using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping.BLL.Managers.Concrete;
using Shopping.DTO;
using Shopping.ViewModel.Category;
using Shopping.ViewModel.Product;
using System.Security.Claims;

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
            var User = HttpContext.User;

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
                ModelState.Remove("PictureName");
                ModelState.Remove("PictureFile");

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                model.AppUserId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);

                string fileName = model.FormFile.FileName;

                var dosyadakiFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", fileName);

                var konum = dosyadakiFileName;

                //Kaydetmek için bir akış ortamı oluşturalım
                var akisOrtami = new FileStream(konum, FileMode.Create);
                var memory = new MemoryStream();

                //Resmi kaydet
                model.FormFile.CopyTo(akisOrtami);
                model.FormFile.CopyTo(memory);

                model.PictureName = fileName;
                model.PictureFile = memory.ToArray();

                akisOrtami.Dispose();
                memory.Dispose();

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
