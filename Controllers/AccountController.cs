using Identity.Entities;
using Identity.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AccountRegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                City = model.City,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Wrong Email or Password");
                return View(model);
            }

            var result = _signInManager.PasswordSignInAsync(user, model.Password, false, false).Result;
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Wrong Email or Password");
                return View(model);
            }
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("index", "home");
        }

        //[HttpGet]
        //public IActionResult AddAdmin()
        //{
        //    var role = new IdentityRole
        //    {
        //        Name = "Admin"
        //    };

        //    var roleResult = _roleManager.CreateAsync(role).Result;
        //    if (!roleResult.Succeeded)
        //        return NotFound("Cannot add Admin");


        //    var user = new User
        //    {
        //        UserName = "admin@app.com",
        //        Email = "admin@app.com",
        //        City = "Baku",
        //        Country = "Azerbaijan"
        //    };

        //    var result = _userManager.CreateAsync(user, "Admin123!").Result;
        //    if (!result.Succeeded)
        //        return NotFound("Cannot add Admin");

        //    var addToRoleResult = _userManager.AddToRoleAsync(user, role.Name).Result;
        //    if (!addToRoleResult.Succeeded)
        //        return NotFound("Cannot give Admin status to user");

        //    return Ok(user);
        //}
    }
}
