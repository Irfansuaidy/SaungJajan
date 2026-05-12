using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAUNGJAJAN.Data;

namespace SAUNGJAJAN.Pages.User_Toko
{
    public class LaporanPenjualanModel : PageModel
    {
        private readonly AppDbContext _context;

        public LaporanPenjualanModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? Bulan { get; set; }

        public string NamaToko { get; set; } = string.Empty;

        public string PeriodeText { get; set; } = string.Empty;

        public decimal TotalPendapatan { get; set; }

        public List<LaporanTransaksiViewModel> LaporanList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var idToko = GetCurrentTokoId();

            if (idToko == null)
            {
                return RedirectToPage("/Auth/LoginToko");
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

            var toko = await _context.TbToko
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.IdToko == idToko.Value);

            if (toko == null)
            {
                return RedirectToPage("/Auth/LoginToko");
            }

            NamaToko = toko.NamaToko;
            PeriodeText = awalBulan.ToString("MMMM yyyy", new CultureInfo("id-ID"));

            var data = await (
                from detail in _context.DetailPesanan.AsNoTracking()
                join pesanan in _context.TbPesanan.AsNoTracking()
                    on detail.IdPesanan equals pesanan.IdPesanan
                join user in _context.TbUser.AsNoTracking()
                    on pesanan.IdUser equals user.IdUser
                where detail.IdToko == idToko.Value
                      && pesanan.WaktuPesan >= awalBulan
                      && pesanan.WaktuPesan < akhirBulan
                orderby pesanan.WaktuPesan descending
                select new
                {
                    detail.IdPesanan,
                    detail.Subtotal,
                    detail.Quantity,
                    pesanan.WaktuPesan,
                    pesanan.Status,
                    NamaPembeli = user.Nama
                }
            ).ToListAsync();

            LaporanList = data
                .GroupBy(x => x.IdPesanan)
                .Select(g =>
                {
                    var first = g.First();

                    return new LaporanTransaksiViewModel
                    {
                        IdPesanan = first.IdPesanan,
                        NamaPembeli = first.NamaPembeli,
                        WaktuPesan = first.WaktuPesan,
                        Status = first.Status,
                        Total = g.Sum(x => x.Subtotal)
                    };
                })
                .OrderByDescending(x => x.WaktuPesan)
                .ToList();

            TotalPendapatan = LaporanList.Sum(x => x.Total);

            return Page();
        }

        private int? GetCurrentTokoId()
        {
            int? sessionIdToko = HttpContext.Session.GetInt32("id_toko");

            if (sessionIdToko.HasValue)
            {
                return sessionIdToko.Value;
            }

            int? sessionTokoId = HttpContext.Session.GetInt32("TokoId");

            if (sessionTokoId.HasValue)
            {
                return sessionTokoId.Value;
            }

            return null;
        }

        public class LaporanTransaksiViewModel
        {
            public int IdPesanan { get; set; }

            public string NamaPembeli { get; set; } = string.Empty;

            public DateTime WaktuPesan { get; set; }

            public decimal Total { get; set; }

            public string Status { get; set; } = string.Empty;
        }
    }
}