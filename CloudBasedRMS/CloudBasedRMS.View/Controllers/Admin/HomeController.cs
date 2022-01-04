using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.Services.AdminServices;
using CloudBasedRMS.View.Controllers.Helper;
using CloudBasedRMS.View.Controllers.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers
{
    public class HomeController :ControllerBase
    {
        public ContactUsServices contactUsServices;
        public FoodItems_DetailsServices foodItems_DetailsServices;
        public EmployeeServices employeeServices;
        public HomeController()
        {
            contactUsServices = new ContactUsServices();
            foodItems_DetailsServices = new FoodItems_DetailsServices();
            employeeServices = new EmployeeServices();
        }
        public ActionResult Index(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);        
            return View(GetHomeRecord());
        }
        public HomeViewModel GetHomeRecord()
        {
            HomeViewModel homeviewmodel = new HomeViewModel();
           homeviewmodel.foodItems_Details= foodItems_DetailsServices.FoodItems_Details.GetByAll().Where(x => x.Active == true).ToList();
            homeviewmodel.employee = employeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            return homeviewmodel;
        }
        [HttpPost]
        public ActionResult SaveContactUs(string Name,string Email,string Company,string WebSite,string Message)
        {
           if (CheckRecrod( Name,  Email,  Company,  WebSite,  Message))
                {
                    ContactUs model = new ContactUs();
                    model.ContactUsID = System.Guid.NewGuid().ToString();
                    model.Name = Name;
                    model.Email = Email;
                    model.Company = Company;
                    model.WebSite = WebSite;
                    model.Message = Message;
                    contactUsServices.ContactUs.Add(model);
                    contactUsServices.Save();
                    ViewBag.Status = "Thank you for your contact message!";
                    return RedirectToAction("Index", "Home");
                }
            return View("Index", "Home");
        }

        private bool CheckRecrod(string name, string email, string company, string webSite, string message)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(company) || string.IsNullOrEmpty(message))
           {
               return false;
          }
           return true;
        }

        public ActionResult About()
        {
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        #region Helper
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        #endregion      
    }
}