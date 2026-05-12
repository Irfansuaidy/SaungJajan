using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAUNGJAJAN.Data;

namespace SAUNGJAJAN.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                return RedirectToPage("/Register");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.TbUser
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.Email == Input.Email &&
                    u.Password == Input.Password);

            if (user == null)
            {
                ErrorMessage = "Email atau password salah.";
                return Page();
            }

            HttpContext.Session.SetInt32("UserId", user.IdUser);
            HttpContext.Session.SetString("UserNama", user.Nama ?? "");
            HttpContext.Session.SetString("UserEmail", user.Email ?? "");
            HttpContext.Session.SetString("UserSaldo", user.Saldo.ToString(CultureInfo.InvariantCulture));

            return RedirectToPage("/User/Menu");
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Email wajib diisi")]
            [EmailAddress(ErrorMessage = "Format email tidak valid")]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password wajib diisi")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;
        }
    }
}
