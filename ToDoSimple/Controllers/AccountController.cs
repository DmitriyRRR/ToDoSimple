using Microsoft.AspNetCore.Mvc;
using ToDoSimple.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace ToDoSimple.Controllers
{
    public class AccountController : Controller

    {
        private readonly ToDoContext _context;
        protected int _accountId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        public AccountController(ToDoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogoutAsync(LoginViewModel model)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
