using CloudBasedRMS.View.Controllers.Helper;
using System;
using System.Threading;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = RouteData.Values["culture"] as string;
            // Attempt to read the culture cookie from Request
            if (cultureName == null)
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            if (RouteData.Values["culture"] as string != cultureName)
            {

                // Force a valid culture in the URL
                RouteData.Values["culture"] = cultureName.ToLowerInvariant(); // lower case too

                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;


            return base.BeginExecuteCore(callback, state);
        }
        //protected override void ExecuteCore()
        //{
        //    int culture = 0;
        //    if (this.Session == null || this.Session["CurrentCulture"] == null)
        //    {
        //        int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
        //        this.Session["CurrentCulture"] = culture;
        //    }
        //    else
        //    {
        //        culture = (int)this.Session["CurrentCulture"];
        //    }
        //    // calling CultureHelper class properties for setting
        //    CultureHelper.CurrentCulture = culture;

        //    base.ExecuteCore();
        //}
        //protected override bool DisableAsyncSupport
        //{
        //    get { return true; }
        //}
    }
}