using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudBasedRMS.WebAPI.Controllers
{
    public class UsersController : Controller
    {
        //User Account Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}