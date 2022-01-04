using CloudBasedRMS.View.Controllers.Helper;
using System.Web.Mvc;
using System.Web.Routing;

namespace CloudBasedRMS.View
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional,language="en-US" }
            );
        }
    }
}
