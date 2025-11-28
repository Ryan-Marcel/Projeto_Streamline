using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Projeto_Dotnet8.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ---------------- LOGIN ----------------
        [HttpGet]
        public IActionResult Login()
        {
            // The application uses the login UI under Views/Principal/Login.cshtml
            // Redirecting to that view ensures a single login UI is kept in one place
            return View("~/Views/Principal/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var result = await _signInManager.PasswordSignInAsync(email, senha, false, false);

            if (!result.Succeeded)
            {
                // Keep the user on the same UI page, and display the error
                ViewBag.Error = "Email ou senha incorretos!";
                return View("~/Views/Principal/Login.cshtml");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // If the user doesn't exist (unexpected because sign-in succeeded), redirect to the index.
                return RedirectToAction("Index", "Principal");
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return RedirectToAction("Dashboard", "Principal");

            return RedirectToAction("Index", "Principal");
        }

        // ---------------- REGISTER ----------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string senha)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, senha);

            if (!result.Succeeded)
            {
                ViewBag.Error = string.Join(", ", result.Errors.Select(e => e.Description));
                return View();
            }

            await _userManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Login");
        }

        // ---------------- LOGOUT ----------------
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        // ---------------- ACCESS DENIED ----------------
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
