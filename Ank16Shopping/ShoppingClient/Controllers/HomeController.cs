using Microsoft.AspNetCore.Mvc;
using Shopping.ViewModel.Category;
using ShoppingClient.Models;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ShoppingClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public async Task<IActionResult> Index()
        //{
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:7035/"); //Api url

        //    HttpResponseMessage responseMessage = await httpClient.GetAsync("/api/Categories");

        //    List<CategoryViewModel> list = new List<CategoryViewModel>();

        //    if (responseMessage != null)
        //    {
        //       list = responseMessage.Content.ReadFromJsonAsync<List<CategoryViewModel>>().Result;
        //    }

        //    if (TempData.ContainsKey("RecordStatus"))
        //    {
        //        ViewBag.RecordStatus = TempData["RecordStatus"];
        //        ViewData["RecordMessage"] = TempData["RecordMessage"];
        //    }

        //    return View(list);
        //}

        public IActionResult Index()
        {
            ViewBag.BasicAuth = HttpContext.Session.GetString("BasicAuth");

            return View();
        }

        public IActionResult Categories()
        {
            ViewBag.BasicAuth = HttpContext.Session.GetString("BasicAuth");

            return View();
        }

        public IActionResult Create()
        {
            CategoryViewModel model = new CategoryViewModel();

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Create(CategoryViewModel model)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:7035/"); //Api url

        //    model.AppUserId = 1;//Login olmu� kullan�c�n�n User bilgisinde alaca��z

        //    string modelJson = JsonSerializer.Serialize(model);

        //    StringContent content = new StringContent(modelJson, Encoding.UTF8, "application/json");

        //    httpClient.PostAsync("/api/Categories", content);

        //    //if (responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
        //    //{
        //    //    // Burada kay�t yap�ld� ve gerekli aksiyon sonras� bir yere y�nlendirme yap�labilri.

        //    //    TempData["RecordMessage"] = "Kay�t yap�lm��t�r.";
        //    //    TempData["RecordStatus"] = true;

        //    //    return RedirectToAction(nameof(Index));
        //    //}


        //    //ViewData["RecordMessage"] = "Kay�t yap�lamam��t�r.";
        //    //ViewData["RecordStatus"] = false;

        //    //return View(model);

        //    TempData["RecordMessage"] = "Kay�t iste�iniz g�nderilmi�tir.";
        //    TempData["RecordStatus"] = true;

        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7035/"); //Api url

            model.AppUserId = 1;//Login olmu� kullan�c�n�n User bilgisinde alaca��z

            string modelJson = JsonSerializer.Serialize(model);

            StringContent content = new StringContent(modelJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await httpClient.PostAsync("/api/Categories", content);

            if (responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                // Burada kay�t yap�ld� ve gerekli aksiyon sonras� bir yere y�nlendirme yap�labilri.

                TempData["RecordMessage"] = "Kay�t yap�lm��t�r.";
                TempData["RecordStatus"] = true;

                return RedirectToAction(nameof(Index));
            }


            ViewData["RecordMessage"] = "Kay�t yap�lamam��t�r.";
            ViewData["RecordStatus"] = false;

            return View(model);
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
