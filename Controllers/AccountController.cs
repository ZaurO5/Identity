using Identity.Entities;
using Identity.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Identity.Utilities.EmailHandler.Models;
using Identity.Utilities.EmailHandler.Abstract;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        #region Register

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

            var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            var url = Url.Action(nameof(ConfirmEmail), "Account", new { token, user.Email }, Request.Scheme);
            _emailService.SendMessage(new Message(new List<string> { user.Email }, "Email Confirmation", url));


            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Login

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

        #endregion

        #region LogOut

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region ForgetPassword

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(AccountForgetPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View(model);
            }

            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            var url = Url.Action(nameof(ResetPassword), "Account", new { token, user.Email }, Request.Scheme);
            _emailService.SendMessage(new Message(new List<string> { user.Email }, "Forget Password?", url));

            ViewBag.NotificationText = "Mail sent successfully";
            return View("Notification");
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(AccountResetPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByNameAsync(model.Email).Result;
            if (user is null)
            {
                ModelState.AddModelError("Password", "Impossible to update the password");
                return View(model);
            }

            var result = _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Subscribe

        [HttpPost]
        public IActionResult Subscribe(string email)
        {
            if (!ModelState.IsValid) return NotFound();

            var user = _userManager.FindByEmailAsync(email).Result;
            if (user is null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }

            var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            var url = Url.Action(nameof(ConfirmSubscription), "Account", new { token, user.Email }, Request.Scheme);
            _emailService.SendMessage(new Message(new List<string> { user.Email }, "Confirm subscription", url));

            ViewBag.NotificationText = "Mail sent successfully";
            return View("Notification");
        }

        #endregion

        #region ConfirmSubscription

        public IActionResult ConfirmSubscription(string email, string token)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user is null) return NotFound();

            user.IsSubscribed = true;

            var updateResult = _userManager.UpdateAsync(user).Result;
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
