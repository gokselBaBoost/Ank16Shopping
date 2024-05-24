using Microsoft.AspNetCore.Mvc;
using Shopping.ViewModel.Category;
using ShoppingClient.Models;
using System.Diagnostics;

namespace ShoppingClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7035/");

            HttpResponseMessage responseMessage = await httpClient.GetAsync("/api/Categories");

            List<CategoryViewModel> list = new List<CategoryViewModel>();

            if (responseMessage != null)
            {
               list = responseMessage.Content.ReadFromJsonAsync<List<CategoryViewModel>>().Result;
            }

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
