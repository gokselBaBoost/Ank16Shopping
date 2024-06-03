using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoppingApiWithJWT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingApiWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private JwtSecurityTokenHandler _jwtTokenHandler;
        private SecurityToken _securityToken;
        private IConfiguration _configuration;

        private string _audience;
        private string _issuer;
        private byte[] _securityKey;

        public AuthController(IConfiguration configuration)
        {
            _jwtTokenHandler = new JwtSecurityTokenHandler();
            _configuration = configuration;

            _audience = _configuration.GetSection("JwtToken:Audience").Value;
            _issuer = _configuration.GetSection("JwtToken:Issuer").Value;
            _securityKey = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtToken:SigningKey").Value);
            

        }

        [HttpPost("Token")]
        public IActionResult Token(UserLoginModel userLoginModel)
        {
            //Model Validation

            //User Check -> db or Identity or başka bir yer

            //Token Oluşturuacağız.
            string token = TokenGenerate();
            // 1. Head
            // JWT Kütüphanesi oluşturacak
            // 2. Payload
            // Benim hazırladığım bir data olacak
            // 3. Signture
            // JWT Kütüphanesine ayar olarak verilecek


            return Ok(token);
        }

        [HttpGet("TokenValidate")]
        public IActionResult TokenValidate(string token)
        {
            try
            {
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = _audience,
                    ValidIssuer = _issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(_securityKey)
                };

                ClaimsPrincipal claimsPrincipal = _jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out _securityToken);

                List<UserInfo> userInfos = new List<UserInfo>();

                foreach (Claim claim in claimsPrincipal.Claims)
                {
                    userInfos.Add(new UserInfo() { Type = claim.Type, Value = claim.Value });
                }


                return Ok(userInfos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }

        private string TokenGenerate()
        {
            //İmza kısmı

            DateTime expires = DateTime.Now.AddMinutes(5);

            SecurityKey securityKey = new SymmetricSecurityKey(_securityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier,"1"));
            claims.Add(new Claim(ClaimTypes.Name,"Göksel"));


            var token = new JwtSecurityToken(_issuer, _audience, claims, expires: expires,signingCredentials: signingCredentials);

            return _jwtTokenHandler.WriteToken(token);

        }
    }
}
