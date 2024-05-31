using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopping.BLL.Managers.Concrete;
using Shopping.Entities.Concrete;
using Shopping.ViewModel.Product;
using ShoppingApi.Filters;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilter]
    public class ProductsController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole<int>> _roleManager;
        private ProductManager _productManager;

        public ProductsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<int>> roleManager, ProductManager productManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _productManager = productManager;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}/Picture")]
        public byte[] GetPicture(int id)
        {
            return _productManager.Get(id).PictureFile;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromForm] ProductViewModel model)
        {
            _productManager.Add(model);
            return Created("", model);
        }

        [HttpPost("FormDataWithJson")]
        public IActionResult FormDataWithJson(IFormFile FormFile, [FromForm] string jsonData)
        {

            //ProductViewModel vm1 = System.Text.Json.JsonSerializer.Deserialize<ProductViewModel>(jsonData);
            ProductViewModel? model = JsonConvert.DeserializeObject<ProductViewModel>(jsonData);

            //model.AppUserId = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);

            string fileName = FormFile.FileName;

            var dosyadakiFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", fileName);

            var konum = dosyadakiFileName;

            //Kaydetmek için bir akış ortamı oluşturalım
            var akisOrtami = new FileStream(konum, FileMode.Create);
            var memory = new MemoryStream();

            //Resmi kaydet
            FormFile.CopyTo(akisOrtami);
            FormFile.CopyTo(memory);

            model.PictureName = fileName;
            model.PictureFile = memory.ToArray();

            akisOrtami.Dispose();
            memory.Dispose();

            _productManager.Add(model);
            return Created("", model);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
