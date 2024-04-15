using Microsoft.AspNetCore.Mvc;
using ToDoSimple.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ToDoSimple.Models;


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

        public async Task<IActionResult> LoginAsync([Bind(Prefix = "l")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = model
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);

            if (user is null)
            {
                ViewBag.Error = "Login data is incorrect";
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = model
                });
            }
            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        private async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> RegisterAsync([Bind(Prefix = "r")] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel
                {
                    RegisterViewModel = model
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);

            if (user != null)
            {
                ViewBag.Error = "Pls login";
                return View("Index", new AccountViewModel
                {
                    RegisterViewModel = model
                });
            }
            user = new User(model.Login, model.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogoutAsync(LoginViewModel model)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
