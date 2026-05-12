using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAUNGJAJAN.Data;
using SAUNGJAJAN.Models;

namespace SAUNGJAJAN.Pages.User
{
    public class MenuModel : PageModel
    {
        private readonly AppDbContext _context;

        public MenuModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? SelectedTokoId { get; set; }

        public List<SelectListItem> TokoOptions { get; set; } = new();

        public List<ProdukViewModel> ProdukList { get; set; } = new();

        public string UserNama { get; set; } = string.Empty;

        public decimal UserSaldo { get; set; }

        public string SelectedNamaToko { get; set; } = string.Empty;

        [TempData]
        public string? SuccessMessage { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Auth/Login");
            }

            await LoadPageDataAsync(userId.Value);

            return Page();
        }

        public async Task<IActionResult> OnPostBuyAsync(int selectedTokoId, int productId, int quantity)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Auth/Login");
            }

            if (selectedTokoId <= 0)
            {
                ErrorMessage = "Warung belum dipilih.";
                return RedirectToPage();
            }

            if (quantity <= 0)
            {
                ErrorMessage = "Jumlah pembelian harus lebih dari 0.";
                return RedirectToPage(new { selectedTokoId });
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = await _context.TbUser
                    .FirstOrDefaultAsync(u => u.IdUser == userId.Value);

                if (user == null)
                {
                    ErrorMessage = "User tidak ditemukan.";
                    return RedirectToPage(new { selectedTokoId });
                }

                var toko = await _context.TbToko
                    .FirstOrDefaultAsync(t => t.IdToko == selectedTokoId);

                if (toko == null)
                {
                    ErrorMessage = "Warung tidak ditemukan.";
                    return RedirectToPage();
                }

                var produk = await _context.TbProduk
                    .FirstOrDefaultAsync(p =>
                        p.IdProduk == productId &&
                        p.IdToko == selectedTokoId);

                if (produk == null)
                {
                    ErrorMessage = "Produk tidak ditemukan pada warung yang dipilih.";
                    return RedirectToPage(new { selectedTokoId });
                }

                if (produk.Stok < quantity)
                {
                    ErrorMessage = "Stok produk tidak mencukupi.";
                    return RedirectToPage(new { selectedTokoId });
                }

                decimal totalHarga = produk.Harga * quantity;

                if (user.Saldo < totalHarga)
                {
                    ErrorMessage = "Saldo user tidak mencukupi.";
                    return RedirectToPage(new { selectedTokoId });
                }

                var pesananBaru = new TbPesanan
                {
                    IdUser = user.IdUser,
                    Status = "Diproses",
                    WaktuPesan = DateTime.Now,
                    TotalHarga = totalHarga
                };

                _context.TbPesanan.Add(pesananBaru);
                await _context.SaveChangesAsync();

                var detailPesananBaru = new DetailPesanan
                {
                    IdPesanan = pesananBaru.IdPesanan,
                    IdProduk = produk.IdProduk,
                    IdToko = selectedTokoId,
                    Quantity = quantity,
                    Subtotal = totalHarga
                };

                _context.DetailPesanan.Add(detailPesananBaru);

                produk.Stok -= quantity;
                user.Saldo -= totalHarga;
                toko.Pemasukan += totalHarga;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                HttpContext.Session.SetString(
                    "UserSaldo",
                    user.Saldo.ToString(CultureInfo.InvariantCulture)
                );

                SuccessMessage = $"Pesanan berhasil dibuat. ID Pesanan: {pesananBaru.IdPesanan}";

                return RedirectToPage(new { selectedTokoId });
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
            
                ErrorMessage = "Gagal membuat pesanan: " + ex.Message;
            
                if (ex.InnerException != null)
                {
                    ErrorMessage += " | Detail: " + ex.InnerException.Message;
                }
            
                return RedirectToPage(new { selectedTokoId });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            
                ErrorMessage = "Terjadi kesalahan saat memproses pemesanan: " + ex.Message;
            
                return RedirectToPage(new { selectedTokoId });
            }
        }

        private async Task LoadPageDataAsync(int userId)
        {
            UserNama = HttpContext.Session.GetString("UserNama") ?? string.Empty;

            var user = await _context.TbUser
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUser == userId);

            if (user != null)
            {
                UserNama = user.Nama;
                UserSaldo = user.Saldo;
            }
            else
            {
                var saldoString = HttpContext.Session.GetString("UserSaldo");

                if (!string.IsNullOrWhiteSpace(saldoString))
                {
                    decimal.TryParse(
                        saldoString,
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out var saldo
                    );

                    UserSaldo = saldo;
                }
            }

            var tokoList = await _context.TbToko
                .AsNoTracking()
                .OrderBy(t => t.NamaToko)
                .ToListAsync();

            TokoOptions = tokoList
                .Select(t => new SelectListItem
                {
                    Value = t.IdToko.ToString(),
                    Text = t.NamaToko
                })
                .ToList();

            if (SelectedTokoId == null)
            {
                return;
            }

            var tokoTerpilih = tokoList
                .FirstOrDefault(t => t.IdToko == SelectedTokoId.Value);

            if (tokoTerpilih == null)
            {
                ErrorMessage = "Warung yang dipilih tidak ditemukan.";
                SelectedTokoId = null;
                return;
            }

            SelectedNamaToko = tokoTerpilih.NamaToko;

            ProdukList = await _context.TbProduk
                .AsNoTracking()
                .Where(p => p.IdToko == SelectedTokoId.Value)
                .OrderBy(p => p.NamaMakanan)
                .Select(p => new ProdukViewModel
                {
                    IdProduk = p.IdProduk,
                    IdToko = p.IdToko,
                    NamaMakanan = p.NamaMakanan,
                    Jenis = p.Jenis,
                    Harga = p.Harga,
                    Stok = p.Stok
                })
                .ToListAsync();
        }

        public class ProdukViewModel
        {
            public int IdProduk { get; set; }

            public int IdToko { get; set; }

            public string NamaMakanan { get; set; } = string.Empty;

            public string Jenis { get; set; } = string.Empty;

            public decimal Harga { get; set; }

            public int Stok { get; set; }
        }
    }
}