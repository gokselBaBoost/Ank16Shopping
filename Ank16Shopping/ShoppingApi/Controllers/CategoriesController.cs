using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shopping.BLL.Managers.Concrete;
using Shopping.ViewModel.Category;
using ShoppingApi.Filters;
using System.Net;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [AuthActionFilter]
    public class CategoriesController : ControllerBase
    {
        private CategoryManager _categoryManager;

        public CategoriesController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        //public IEnumerable<CategoryViewModel> Get()
        public IActionResult Get()
        {
            //return _categoryManager.GetAll();
            return Ok(_categoryManager.GetAll());
        }

        // GET: api/<CategoriesController>
        [HttpPost("Post")]
        //public IEnumerable<CategoryViewModel> Get()
        public IActionResult Post()
        {
            //return _categoryManager.GetAll();
            return Ok(_categoryManager.GetAll());
        }

        // GET: api/<CategoriesController>
        [HttpPut("Put")]
        //public IEnumerable<CategoryViewModel> Get()
        public IActionResult Put()
        {
            //return _categoryManager.GetAll();
            return Ok(_categoryManager.GetAll());
        }

        // GET: api/<CategoriesController>
        [HttpDelete("Delete")]
        //public IEnumerable<CategoryViewModel> Get()
        public IActionResult Delete()
        {
            //return _categoryManager.GetAll();
            return Ok(_categoryManager.GetAll());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_categoryManager.Get(id));
        }

        // GET api/<CategoriesController>/5/Products
        [HttpGet("{id}/Products")]
        public IActionResult GetProducts(int id)
        {
            return Ok(_categoryManager.Get(id));
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] CategoryViewModel model)
        {
            //if(HttpContext.Request.Headers.ContainsKey("userName") && 
            //    HttpContext.Request.Headers.ContainsKey("password"))
            //{
            //    string username = HttpContext.Request.Headers["userName"];
            //    string password = HttpContext.Request.Headers["password"];

            //    if(username == "admin" && password == "1234567")
            //    {
            //        Thread.Sleep(2000);
            //        _categoryManager.Add(model);
            //        return Created("", model);
            //    }
            //    else
            //    {
            //        return BadRequest("Kullanıcı adı veya şifre yanlış");
            //    }
            //}

            if (HttpContext.Request.Headers.ContainsKey("yetkiKodu"))
            {
                string token = HttpContext.Request.Headers["yetkiKodu"];

                var userInfo = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split("@");

                if (userInfo[0] == "admin" && userInfo[1] == "1234567")
                {
                    Thread.Sleep(2000);
                    _categoryManager.Add(model);
                    return Created("", model);
                }
                else
                {
                    return BadRequest("Kullanıcı adı veya şifre yanlış");
                }
            }

            return Unauthorized("Yetkisiz giriş");
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryViewModel model)
        {
            CategoryViewModel? category = _categoryManager.Get(id);

            if (category == null)
                return NotFound();

            category.Name = model.Name;
            category.Description = model.Description;

            _categoryManager.Update(category);

            return StatusCode(210,"Güncelleme yapıldı.");
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CategoryViewModel? category = _categoryManager.Get(id);

            if (category == null)
                return NotFound();

            _categoryManager.Delete(category);

            return StatusCode(220, "Silme yapıldı.");
        }
    }
}
