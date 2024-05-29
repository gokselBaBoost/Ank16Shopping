using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping.BLL.Managers.Concrete;
using Shopping.ViewModel.Category;
using Shopping.ViewModel.Product;

namespace ShoppingAdmin.Controllers
{
    public class ProductController : Controller
    {
        private ProductManager _productManager;
        private CategoryManager _categoryManager;

        public ProductController(ProductManager productManager, CategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> list = _productManager.GetAll();

            return View(list);
        }
        // GET: CategoryController/Create
        public ActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();

            List<SelectListItem> list = new List<SelectListItem>();

            List<CategoryViewModel> categories = _categoryManager.GetAll().ToList();

            foreach (CategoryViewModel category in categories)
            {
                list.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }

            ViewBag.CategoryList = list;

            return View(model);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                model.AppUserId = 1;//Login olan User Id  => AspNetUser da ki ID  Identity Login Kısımını yapılması gerekiyor

                if (_productManager.Add(model) > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError("DbError", "Veritabanı ekleme hatası");

                    return View(model);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GeneralException", ex.Message);
                ModelState.AddModelError("GeneralInnerException", ex.InnerException?.Message);
                return View();
            }
        }
    }
}
