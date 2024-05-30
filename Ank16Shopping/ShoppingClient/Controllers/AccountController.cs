using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Shopping.ViewModel;
using Shopping.ViewModel.Category;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ShoppingClient.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7035/"); //Api url

            SignInViewModel signInViewModel = new SignInViewModel();
            signInViewModel.Email = email;
            signInViewModel.Password = password;


            string modelJson = JsonSerializer.Serialize(signInViewModel);

            StringContent content = new StringContent(modelJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await httpClient.PostAsync("/api/Account/SignIn", content);

            if (responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Burada kayıt yapıldı ve gerekli aksiyon sonrası bir yere yönlendirme yapılabilri.

                List<UserClaimViewModel>  userClaimViewModel = responseMessage.Content.ReadFromJsonAsync<List<UserClaimViewModel>>().Result;

                List<Claim> claims = new List<Claim>();

                foreach(UserClaimViewModel item in userClaimViewModel)
                {
                    Claim claim = new Claim(item.Type, item.Value);
                    claims.Add(claim);
                }


                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index","Home");
            }

            return BadRequest();
        }
    }
}
