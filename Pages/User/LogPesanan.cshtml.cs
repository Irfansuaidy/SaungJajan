using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAUNGJAJAN.Data;

namespace SAUNGJAJAN.Pages.User
{
    public class LogPesananModel : PageModel
    {
        private readonly AppDbContext _context;

        public LogPesananModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? Bulan { get; set; }

        public string UserNama { get; set; } = string.Empty;

        public string PeriodeText { get; set; } = string.Empty;

        public List<LogPesananViewModel> LogPesananList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Auth/Login");
            }

            DateTime awalBulan;

            if (!string.IsNullOrWhiteSpace(Bulan) &&
                DateTime.TryParseExact(Bulan, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var hasilParse))
            {
                awalBulan = new DateTime(hasilParse.Year, hasilParse.Month, 1);
            }
            else
            {
                var sekarang = DateTime.Now;
                awalBulan = new DateTime(sekarang.Year, sekarang.Month, 1);
                Bulan = awalBulan.ToString("yyyy-MM");
            }

            var akhirBulan = awalBulan.AddMonths(1);

            var user = await _context.TbUser
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUser == userId.Value);

            UserNama = user?.Nama ?? "-";
            PeriodeText = awalBulan.ToString("MMMM yyyy", new CultureInfo("id-ID"));

            var data = await (
                from detail in _context.DetailPesanan.AsNoTracking()
                join pesanan in _context.TbPesanan.AsNoTracking()
                    on detail.IdPesanan equals pesanan.IdPesanan
                join produk in _context.TbProduk.AsNoTracking()
                    on detail.IdProduk equals produk.IdProduk
                join toko in _context.TbToko.AsNoTracking()
                    on detail.IdToko equals toko.IdToko
                where pesanan.IdUser == userId.Value
                      && pesanan.WaktuPesan >= awalBulan
                      && pesanan.WaktuPesan < akhirBulan
                orderby pesanan.WaktuPesan descending
                select new
                {
                    detail.IdPesanan,
                    detail.Quantity,
                    detail.Subtotal,
                    produk.NamaMakanan,
                    produk.Harga,
                    toko.NamaToko,
                    pesanan.Status,
                    pesanan.WaktuPesan,
                    pesanan.TotalHarga
                }
            ).ToListAsync();

            LogPesananList = data
                .GroupBy(x => x.IdPesanan)
                .Select(g =>
                {
                    var first = g.First();

                    return new LogPesananViewModel
                    {
                        IdPesanan = first.IdPesanan,
                        NamaToko = first.NamaToko,
                        Status = first.Status,
                        WaktuPesan = first.WaktuPesan,
                        TotalHarga = first.TotalHarga,
                        Detail = g.Select(x => new LogDetailViewModel
                        {
                            NamaProduk = x.NamaMakanan,
                            Quantity = x.Quantity,
                            Harga = x.Harga,
                            Subtotal = x.Subtotal
                        }).ToList()
                    };
                })
                .OrderByDescending(x => x.WaktuPesan)
                .ToList();

            return Page();
        }

        public class LogPesananViewModel
        {
            public int IdPesanan { get; set; }

            public string NamaToko { get; set; } = string.Empty;

            public string Status { get; set; } = string.Empty;

            public DateTime WaktuPesan { get; set; }

            public decimal TotalHarga { get; set; }

            public List<LogDetailViewModel> Detail { get; set; } = new();
        }

        public class LogDetailViewModel
        {
            public string NamaProduk { get; set; } = string.Empty;

            public int Quantity { get; set; }

            public decimal Harga { get; set; }

            public decimal Subtotal { get; set; }
        }
    }
}