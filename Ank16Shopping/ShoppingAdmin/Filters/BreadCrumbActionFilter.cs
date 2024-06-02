using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace ShoppingAdmin.Filters
{
    public class BreadCrumbActionFilter : ActionFilterAttribute
    {
        private List<(bool IsActive, string Title, string Link)> _breadcrumbs;
        private string _controllerName;

        public string Title { get; set; }
        public BreadCrumbActionFilter()
        {
            _breadcrumbs = new List<(bool IsActive, string Title, string Link)>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _breadcrumbs.Clear();

            Controller controller = context.Controller as Controller;

            if (controller != null)
            {
                string controllerName = controller.RouteData.Values["controller"].ToString();

                string[] breadCrumbs = Title.Split('|');

                foreach(string breadCrumb in breadCrumbs)
                {
                    string[] titleLink = breadCrumb.Split('@');

                    string link = "";

                    if (titleLink.Length > 1)
                        link = controller.Url.ActionLink(titleLink[1], controllerName);

                    if (titleLink.Length == 2)
                        _breadcrumbs.Add((true, titleLink[0], link));
                    else
                        _breadcrumbs.Add((false, titleLink[0], ""));
                }

                controller.ViewBag.BreadCrumbs = _breadcrumbs;
            }
        }
    }
}
