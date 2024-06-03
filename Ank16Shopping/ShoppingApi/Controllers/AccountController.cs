﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shopping.Entities.Concrete;
using Shopping.Services.Mail;
using Shopping.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        private JwtSecurityTokenHandler _jwtTokenHandler;
        private SecurityToken _securityToken;
        private IConfiguration _configuration;

        private string _audience;
        private string _issuer;
        private byte[] _securityKey;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            RoleManager<IdentityRole<int>> roleManager,
            IMailService mailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;

            _jwtTokenHandler = new JwtSecurityTokenHandler();
            _configuration = configuration;

            _audience = _configuration.GetSection("JwtToken:Audience").Value;
            _issuer = _configuration.GetSection("JwtToken:Issuer").Value;
            _securityKey = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtToken:SigningKey").Value);
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

                SignInResponseViewModel response = new SignInResponseViewModel();
                response.Claims = userClaimViewModel;
                response.BasicAuth = BasicAuthGenerate(model.Email, model.Password);

                return Ok(response);
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


        [HttpPost("SignInWithJwt")]
        public IActionResult SignInWithJwt(SignInViewModel model)
        {


            AppUser? user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user == null) return NotFound("Kullanıcı adı veya şifre yanlıştır.");


            Microsoft.AspNetCore.Identity.SignInResult result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true).Result;

            if (result.Succeeded)
            {
                List<Claim> claims = HttpContext.User.Claims.ToList();

                List<UserClaimViewModel> userClaimViewModel = new List<UserClaimViewModel>();

                foreach (Claim claim in claims)
                {
                    userClaimViewModel.Add(new() { Type = claim.Type, Value = claim.Value });
                }

                SignInResponseViewModel response = new SignInResponseViewModel();
                response.Claims = userClaimViewModel;
                response.BasicAuth = BasicAuthGenerate(model.Email, model.Password);
                response.JwtToken = JwtGenerate(claims);

                return Ok(response);
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

        private string JwtGenerate(List<Claim> claims)
        {
            //İmza kısmı

            DateTime expires = DateTime.Now.AddMinutes(5);

            SecurityKey securityKey = new SymmetricSecurityKey(_securityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_issuer, _audience, claims, expires: expires, signingCredentials: signingCredentials);

            return _jwtTokenHandler.WriteToken(token);
        }

        [NonAction]
        private string BasicAuthGenerate(string email, string password)
        {
            string userInfo = email + ":" + password;

            string result = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo)); ;

            return "Basic " + result;
        }
    }
}
