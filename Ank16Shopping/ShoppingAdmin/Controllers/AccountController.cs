using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping.Entities.Concrete;
using ShoppingAdmin.Models;

namespace ShoppingAdmin.Controllers
{
    public class AccountController : Controller
    {
        private IValidator<LoginViewModel> _validator;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(IValidator<LoginViewModel> validator, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _validator = validator;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ValidationResult result = _validator.Validate(model);

            if(!result.IsValid)
            {
                ModelState.Clear();

                result.AddToModelState(ModelState);

                return View(model);
            }

            AppUser? user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("UserNotFound", "Kullanıcı adı veya şifre yanlıştır");

                return View(model);
            }

            if(user.UserType != Shopping.Common.UserType.Manager)
            {
                ModelState.AddModelError("UserNotFound", "Kullanıcı adı veya şifre yanlıştır :)");

                return View(model);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if(signInResult.IsLockedOut)
            {
                //return View(nameof(LockedOut));

                ModelState.AddModelError("UserLocked", "Hesabınız kilitlenmiştir");

                return View(model);
            }

            if (signInResult.IsNotAllowed)
            {
                //return View(nameof(LockedOut));

                ModelState.AddModelError("UserLocked", "Hesabınız henüz aktif değildir. Lütfen email adresinizi doğrulayınız.");

                return View(model);
            }

            if (signInResult.RequiresTwoFactor)
            {
                return View(nameof(LoginTwoFactor));
            }

            if(!signInResult.Succeeded)
            {
                ModelState.AddModelError("LoginFail", "Kullanıcı adı veya şifre yanlıştır");

                return View(model);
            }


            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult LockedOut()
        {
            return View();
        }

        public IActionResult LoginTwoFactor()
        {
            return View();
        }
    }
}
