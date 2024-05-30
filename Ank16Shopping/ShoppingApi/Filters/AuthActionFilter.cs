using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Shopping.BLL.Managers.Concrete;
using Shopping.Entities.Concrete;
using System.Text;

namespace ShoppingApi.Filters
{
    public class AuthActionFilter : ActionFilterAttribute
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole<int>> _roleManager;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _userManager = context.HttpContext.RequestServices.GetService<UserManager<AppUser>>();
            _signInManager = context.HttpContext.RequestServices.GetService<SignInManager<AppUser>>();
            _roleManager = context.HttpContext.RequestServices.GetService<RoleManager<IdentityRole<int>>>();


            //Authorization için kontrol
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.WriteAsync("Yetkisiz giriş");
                return;
            }
               

            var authorization = context.HttpContext.Request.Headers["Authorization"]; // Base64 Encode olarak değer gelecek

            authorization = authorization.ToString().Split(" ");

            authorization = Encoding.UTF8.GetString(Convert.FromBase64String(authorization[1])); // username:password

            authorization = authorization.ToString().Split(":");

            string email = authorization[0];
            string password = authorization[1];

            //User Var mı?
            AppUser? appUser = _userManager.FindByEmailAsync(email).Result;

            if (appUser == null)
            {
                context.HttpContext.Response.StatusCode = 404;
                context.HttpContext.Response.WriteAsync("Kullanıcı adı veya şifre yanlıştır.");
                return;
            }

            Microsoft.AspNetCore.Identity.SignInResult result = _signInManager.PasswordSignInAsync(appUser, password, false, true).Result;

            if (result.IsNotAllowed)
            {
                context.HttpContext.Response.StatusCode = 460;
                context.HttpContext.Response.WriteAsync("Email adresiniz doğrulanmamıştır. Lütfen doğrulayınız");
                return;
            }

            TimeSpan lockOutEnd = _signInManager.Options.Lockout.DefaultLockoutTimeSpan;

            //double minute = (Convert.ToDateTime(lockOutEnd) -  DateTime.Now).TotalMinutes;

            if (result.IsLockedOut)
            {
                context.HttpContext.Response.StatusCode = 460;
                context.HttpContext.Response.WriteAsync($"Hesabınız kilitlenmiştir. {lockOutEnd.Minutes} dakika sonra giriniz.{DateTime.Now.AddMinutes(lockOutEnd.Minutes)}");
                return;
            }

            if (result.RequiresTwoFactor)
            {
                context.HttpContext.Response.StatusCode = 250;
                context.HttpContext.Response.WriteAsync("İkili doğrulama linki : https://localhost/Auth/TwoFactAuth");
                return;
            }

            if (!result.Succeeded)
            {
                context.HttpContext.Response.StatusCode = 404;
                context.HttpContext.Response.WriteAsync("Kullanıcı adı veya şifre yanlıştır.");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
