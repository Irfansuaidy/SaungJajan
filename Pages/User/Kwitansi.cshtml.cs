using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAUNGJAJAN.Data;

namespace SAUNGJAJAN.Pages.User
{
    public class KwitansiModel : PageModel
    {
        private readonly AppDbContext _context;

        public KwitansiModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int IdPesanan { get; set; }

        public KwitansiViewModel? Data { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Auth/Login");
            }

            var pembayaran = await _context.TbPembayaran
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.IdPesanan == IdPesanan &&
                    p.IdUser == userId.Value);

            if (pembayaran == null)
            {
                return RedirectToPage("/User/LogPesanan");
            }

            var rows = await (
                from detail in _context.DetailPesanan.AsNoTracking()
                join pesanan in _context.TbPesanan.AsNoTracking()
                    on detail.IdPesanan equals pesanan.IdPesanan
                join produk in _context.TbProduk.AsNoTracking()
                    on detail.IdProduk equals produk.IdProduk
                join toko in _context.TbToko.AsNoTracking()
                    on detail.IdToko equals toko.IdToko
                join user in _context.TbUser.AsNoTracking()
                    on pesanan.IdUser equals user.IdUser
                where pesanan.IdPesanan == IdPesanan
                      && pesanan.IdUser == userId.Value
                select new
                {
                    pembayaran.KodeKwitansi,
                    pembayaran.WaktuDiteruskan,
                    pembayaran.StatusPembayaran,
                    pembayaran.JumlahBayar,
                    user.Nama,
                    toko.NamaToko,
                    produk.NamaMakanan,
                    produk.Harga,
                    detail.Quantity,
                    detail.Subtotal
                }
            ).ToListAsync();

            if (!rows.Any())
            {
                return RedirectToPage("/User/LogPesanan");
            }

            var first = rows.First();

            Data = new KwitansiViewModel
            {
                IdPesanan = IdPesanan,
                KodeKwitansi = first.KodeKwitansi,
                WaktuDiteruskan = first.WaktuDiteruskan,
                StatusPembayaran = first.StatusPembayaran,
                JumlahBayar = first.JumlahBayar,
                NamaPembeli = first.Nama,
                NamaToko = first.NamaToko,
                Items = rows.Select(x => new KwitansiItemViewModel
                {
                    NamaProduk = x.NamaMakanan,
                    Quantity = x.Quantity,
                    Harga = x.Harga,
                    Subtotal = x.Subtotal
                }).ToList()
            };

            return Page();
        }

        public class KwitansiViewModel
        {
            public int IdPesanan { get; set; }

            public string KodeKwitansi { get; set; } = string.Empty;

            public DateTime WaktuDiteruskan { get; set; }

            public string StatusPembayaran { get; set; } = string.Empty;

            public decimal JumlahBayar { get; set; }

            public string NamaPembeli { get; set; } = string.Empty;

            public string NamaToko { get; set; } = string.Empty;

            public List<KwitansiItemViewModel> Items { get; set; } = new();
        }

        public class KwitansiItemViewModel
        {
            public string NamaProduk { get; set; } = string.Empty;

            public int Quantity { get; set; }

            public decimal Harga { get; set; }

            public decimal Subtotal { get; set; }
        }
    }
}