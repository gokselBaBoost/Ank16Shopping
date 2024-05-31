using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.ViewModel;
using System.Text;

namespace ShoppingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginViewModel model)
        {
            LoginResponseModel loginResponseModel = new LoginResponseModel();

            //if(!ModelState.IsValid)
            //    return BadRequest("Model is not valid");

            if (model.UserName != "admin" && model.Password != "1234567")
            {
                loginResponseModel.StatusCode = 400;
                loginResponseModel.Message = "Kullanıcı adı veya şifre yanlıştır.";

                return BadRequest(loginResponseModel);
            }

            string userInfo = model.UserName + "@" + model.Password;

            loginResponseModel.StatusCode = 200;
            loginResponseModel.Message = "Giriş başarılıdır.";
            loginResponseModel.AuthToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));

            return Ok(loginResponseModel);
        }

        [HttpGet("{token}")] 
        public IActionResult Login(string token)
        {
            var userInfo = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split("@");

            if (userInfo[0] == "admin" && userInfo[1] == "1234567")
            {
                return Ok("Kullanıcı doğru");
            }

            return BadRequest("Token fail");
        }
    }
}
