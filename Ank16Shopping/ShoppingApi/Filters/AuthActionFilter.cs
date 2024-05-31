using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shopping.BLL.Managers.Concrete;
using Shopping.Entities.Concrete;
using System.Text;

namespace ShoppingApi.Filters
{
    public class AuthActionFilter : ActionFilterAttribute
    {
        private UserManager<AppUser>? _userManager;
        private SignInManager<AppUser>? _signInManager;
        private RoleManager<IdentityRole<int>>? _roleManager;
        private ActionExecutingContext? _context;

        public bool UserCheck { get; set; } 

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _context = context;

            _userManager = context.HttpContext.RequestServices.GetService<UserManager<AppUser>>();
            _signInManager = context.HttpContext.RequestServices.GetService<SignInManager<AppUser>>();
            _roleManager = context.HttpContext.RequestServices.GetService<RoleManager<IdentityRole<int>>>();


            if(_userManager is null)
            {
                context.Result = ReturnResult("UserManager servisi yüklenemedi.", 500);
                return;
            }

            /*
             * Bir işlemi kırmak için break
             * Bir işlemi atlamak için continue
             * Bir işlemi tamamen sonuçlandırmak için return
             */


            if (_signInManager is null)
            {
                context.Result = ReturnResult("SignInManager servisi yüklenemedi.", 500);
            }

            if (_roleManager is null)
            {
                context.Result = ReturnResult("RoleManager servisi yüklenemedi.", 500);
            }

            if (UserCheck)
            {
                //True ise bu işlemler
            }
            else
            {
                //False ise bu işlemeler
            }

            //Authorization için kontrol
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                object uaObject = new
                {
                    StatusCode = 401,
                    Message = "Yetkisiz Giriş",
                    Location = "ShoppinApi Product"
                };

                context.Result = ReturnResult(uaObject, 401);

                return;
            }

            //(int, string) a = (5, "Hello World");
            //Tuple<string, int> b = new Tuple<string, int>("MErhaba", 5);

            var checkAuthorization = CheckAuthorization();

            if (checkAuthorization.Check)
            {
                //User Var mı?
                AppUser? appUser = _userManager.FindByEmailAsync(checkAuthorization.Email).Result;

                if (appUser == null)
                {
                    context.Result = NotFoundReturnResult();
                    return;
                }

                Microsoft.AspNetCore.Identity.SignInResult result = _signInManager.PasswordSignInAsync(appUser, checkAuthorization.Password, false, true).Result;

                if (result.IsNotAllowed)
                {
                    context.Result = ReturnResult("Email adresiniz doğrulanmamıştır. Lütfen doğrulayınız", 460);
                    return;
                }

                if (result.IsLockedOut)
                {
                    TimeSpan lockOutEnd = _signInManager.Options.Lockout.DefaultLockoutTimeSpan;

                    context.Result = ReturnResult($"Hesabınız kilitlenmiştir. {lockOutEnd.Minutes} dakika sonra giriniz.{DateTime.Now.AddMinutes(lockOutEnd.Minutes)}", 470);

                    return;
                }

                if (result.RequiresTwoFactor)
                {
                    context.Result = ReturnResult("İkili doğrulama linki : https://localhost/Auth/TwoFactAuth", 250);

                    return;
                }

                if (!result.Succeeded)
                {
                    context.Result = NotFoundReturnResult();
                    return;
                }

            }
            else
            {
                object uaObject = new
                {
                    StatusCode = 500,
                    Message = "Authorization Model wrong!!",
                    Location = "ShoppinApi Product"
                };

                UnauthorizedObjectResult uaObjectResult = new UnauthorizedObjectResult(uaObject);
                uaObjectResult.ContentTypes.Add("application/json");

                context.Result = uaObjectResult;

                return;
            }

            base.OnActionExecuting(context);
        }

        public (bool Check, string Email, string Password) CheckAuthorization()
        {
            var authorization = _context.HttpContext.Request.Headers["Authorization"]; // Base64 Encode olarak değer gelecek

            authorization = authorization.ToString().Split(" ");

            //authorization[0] // JWT

            authorization = Encoding.UTF8.GetString(Convert.FromBase64String(authorization[1])); // username:password

            authorization = authorization.ToString().Split(":");

            string email = "";
            string password = "";
            bool check = true;

            if (authorization.Count == 2)
            {
                email = authorization[0];
                password = authorization[1];
            }
            else
            {
                check = false;
            }

            

            return (check, email, password);
        }

        public IActionResult ReturnResult(object message, int statusCode)
        {
            ObjectResult objectResult = new ObjectResult(message);
            objectResult.StatusCode = statusCode;
            objectResult.ContentTypes.Add("application/json");

            return objectResult;
        }

        public IActionResult NotFoundReturnResult()
        {
            return ReturnResult("Kullanıcı adı veya şifre yanlıştır", 404);
        }
    }
}
