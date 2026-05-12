using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAUNGJAJAN.Data;

namespace SAUNGJAJAN.Pages.Auth
{
    public class LoginTokoModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginTokoModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int IdToko { get; set; }

        public List<SelectListItem> TokoOptions { get; set; } = new();

        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadTokoOptionsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (IdToko <= 0)
            {
                ErrorMessage = "Silakan pilih toko terlebih dahulu.";
                await LoadTokoOptionsAsync();
                return Page();
            }

            var toko = await _context.TbToko
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.IdToko == IdToko);

            if (toko == null)
            {
                ErrorMessage = "Toko tidak ditemukan.";
                await LoadTokoOptionsAsync();
                return Page();
            }

            HttpContext.Session.SetInt32("id_toko", toko.IdToko);
            HttpContext.Session.SetString("NamaToko", toko.NamaToko);
            HttpContext.Session.SetString("LoginSebagai", "Toko");

            return RedirectToPage("/User_Toko/Dashboard");
        }

        private async Task LoadTokoOptionsAsync()
        {
            var tokoList = await _context.TbToko
                .AsNoTracking()
                .OrderBy(t => t.NamaToko)
                .ToListAsync();

            TokoOptions = tokoList
                .Select(t => new SelectListItem
                {
                    Value = t.IdToko.ToString(),
                    Text = $"{t.IdToko} - {t.NamaToko}"
                })
                .ToList();
        }
    }
}