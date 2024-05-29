using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping.Entities.Concrete;
using Shopping.Services.Mail;
using Shopping.ViewModel;
using System.Security.Claims;

namespace ShoppingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole<int>> _roleManager;
        private IMailService _mailService;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            RoleManager<IdentityRole<int>> roleManager,
            IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            AppUser user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user != null)
                return BadRequest("Mail adresi daha önceden kayıtlıdır.");

            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();

            user = new AppUser();

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.BirthDate = model.BirthDate;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.UserName = model.UserName;
            user.PasswordHash = hasher.HashPassword(null, model.Password);

            IdentityResult result = _userManager.CreateAsync(user).Result;

            if (result.Succeeded)
            {
                //Mail gönderme

                _mailService.Send(user.Email, user.Name, "Register", "Kayıt için tıklayınız link");
            }

            return Ok();
        }

        [HttpPost("SignIn")]
        public IActionResult SignIn(SignInViewModel model)
        {
            AppUser? user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user == null) return NotFound("Kullanıcı adı veya şifre yanlıştır.");


            Microsoft.AspNetCore.Identity.SignInResult result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true).Result;

            if(result.Succeeded)
            {
                List<Claim> claims = HttpContext.User.Claims.ToList();

                List<UserClaimViewModel> userClaimViewModel = new List<UserClaimViewModel>();

                foreach(Claim claim in claims)
                {
                    userClaimViewModel.Add(new() { Type =  claim.Type, Value = claim.Value });
                }
                
                return Ok(userClaimViewModel);
            }

            if (result.IsNotAllowed)
            {
                return BadRequest("Mail adresiniz doğrulanmamıştır");
            }

            if (result.IsLockedOut)
            {
                return BadRequest("Hesabınız kilitlenmiştir.");
            }


            return NotFound("Kullanıcı adı veya şifre yanlıştır.");
        }
    }
}
