using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using ShoppingAdmin.Filters;
using Shopping.Common;
using Shopping.Entities.Concrete;

namespace ShoppingAdmin.Controllers
{
    [Route("[controller]")]
    public class ManagementController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;
        //private List<(bool IsActive, string Title, string Link)> _breadcrumbs;

        public ManagementController(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager, IHtmlHelper htmlHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            //_breadcrumbs = new List<(bool IsActive, string Title, string Link)>();

            //_breadcrumbs.Add((false, "Yönetim", "/Management"));
        }

        // GET: ManagementController
        [Route("")]
        [Route("[action]")]
        [BreadCrumbActionFilter(Title = "Yönetim")]
        public ActionResult Index()
        {
            List<AppUser> users = _userManager.Users.ToList();
            List<IdentityRole<int>> roles = _roleManager.Roles.ToList();

            ViewBag.Users = users;
            ViewBag.Roles = roles;
            //ViewBag.BreadCrumbs = _breadcrumbs;

            return View();
        }

        //// GET: ManagementController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //GET: ManagementController/CreateUser
        [HttpGet("[action]")]
        [BreadCrumbActionFilter(Title = "Yönetim@Index|Yeni Kullanıcı")]
        public ActionResult CreateUser()
        {
            return View();
        }

        //GET: ManagementController/CreateRole
        [HttpGet("[action]")]
        [BreadCrumbActionFilter(Title = "Yönetim@Index|Yeni Role")]
        public ActionResult CreateRole()
        {
            return View();
        }

        //// POST: ManagementController/Create
        //[HttpPost("{type}")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(ManagementType type, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ManagementController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ManagementController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ManagementController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ManagementController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
