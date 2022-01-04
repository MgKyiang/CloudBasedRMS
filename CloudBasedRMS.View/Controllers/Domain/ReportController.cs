using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.View.Controllers.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers.Domain
{
    public class ReportController : Controller
    {
        EmployeeServices eS;

        public ReportController()
        {
            eS = new EmployeeServices();
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StudentDetailReport()
        {
            return View(new EmployeeViewModel());
        }
        //[HttpPost]
        //public ActionResult StudentDetailReport(EmployeeViewModel filterinput)
        //{
        //    IEnumerable<Employee> data = null;
        //    if (string.IsNullOrEmpty(filterinput.EmployeeNo) && string.IsNullOrEmpty(filterinput.AddressID))
        //    {
        //        data = eS.Employee.GetByAll();
        //    }
        //    else { data = eS.Employee.GetByAll().Where(x => x.EmployeeNo == filterinput.EmployeeNo ||x.AddressID == filterinput.AddressID).ToList(); }

        //    List<StudentviewModel> item = new List<StudentviewModel>();
        //    foreach (var i in data)
        //    {
        //        StudentviewModel obj = new StudentviewModel();
        //        obj.StudentName = i.StudentName;
        //        obj.Email = i.Email;
        //        //getting the image url path from the database and set rdlc image with the AbsoluteUri
        //        obj.ImagePath = new Uri(Server.MapPath(i.ImagePath)).AbsoluteUri;
        //        obj.TownshipName = i.Township.TownshipDescription;
        //        item.Add(obj);
        //    }
        //    Session["ReportData"] = item;
        //    return View();
        //}

    }
}