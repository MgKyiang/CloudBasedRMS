using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers.Admin
{
    public class FAQController : ControllerBase
    {
        // GET: FAQ
        public ActionResult Index()
        {
            return View();
        }
    }
}