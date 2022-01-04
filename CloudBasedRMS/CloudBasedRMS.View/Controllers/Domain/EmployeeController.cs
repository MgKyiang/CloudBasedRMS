namespace CloudBasedRMS.View.Controllers
{
    using Core;
    using Microsoft.AspNet.Identity;
    using PagedList;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ViewModel;
    public class EmployeeController : ControllerAuthorizeBase
    {
        public EmployeeServices EmployeeServices;
        public UsersMemberServices UsersMemberServices;
        public RankServices rankServices;
        public AddressServices addressServices;
        public EmployeeController()
        {
            EmployeeServices = new EmployeeServices();
            UsersMemberServices = new UsersMemberServices();
            rankServices = new RankServices();
            addressServices = new AddressServices();
        }
        #region GET
        // GET: Employee
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            //Sorting ViewBag
            ViewBag.EmployeeNoSortby = sortby == "EmployeeNoAsc" ? "EmployeeNoDesc" : "EmployeeNoAsc";
            ViewBag.NameSortby = sortby == "NameAsc" ? "NameDesc" : "NameAsc";
            ViewBag.RankSortby = sortby == "RankAsc" ? "RankDesc" : "RankAsc";

            //For Showing Alert
            ViewBag.Searchby1 = searchby1;
            ViewBag.Search1 = search1;
            ViewBag.Searchby2 = searchby2;
            ViewBag.Search2 = search2;
            ViewBag.Searchby3 = searchby3;
            ViewBag.Search3 = search3;
            ViewBag.Searchby4 = searchby4;
            ViewBag.Search4 = search4;

            ViewBag.sortby = sortby;
            ViewBag.Status = status;
            var data = EmployeeServices.Employee.GetByAll().Where(x => x.Active == true).OrderBy(x => x.EmployeeNo).ToList();
            //Searching
            if (!string.IsNullOrEmpty(searchby1) && !string.IsNullOrEmpty(search1))
            {
                data = GetSearchingData(searchby1, search1, data);
            }
            if (!string.IsNullOrEmpty(searchby2) && !string.IsNullOrEmpty(search2))
            {
                data = GetSearchingData(searchby2, search2, data);
            }
            if (!string.IsNullOrEmpty(searchby3) && !string.IsNullOrEmpty(search3))
            {
                data = GetSearchingData(searchby3, search3, data);
            }
            if (!string.IsNullOrEmpty(searchby4) && !string.IsNullOrEmpty(search4))
            {
                data = GetSearchingData(searchby4, search4, data);
            }
            //Sorting
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "EmployeeNoAsc":
                        data = data.OrderBy(x => x.EmployeeNo).ToList();
                        break;
                    case "EmployeeNoDesc":
                        data = data.OrderByDescending(x => x.EmployeeNo).ToList();
                        break;
                    case "NameAsc":
                        data = data.OrderBy(x => x.Name).ToList();
                        break;
                    case "NameDesc":
                        data = data.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "RankAsc":
                        data = data.OrderBy(x => x.Rank.Description).ToList();
                        break;
                    case "RankDesc":
                        data = data.OrderByDescending(x => x.Rank.Description).ToList();
                        break;
                    case "CreatedUserNameAsc":
                        data = data.OrderBy(x => x.CreatedUser).ToList();
                        break;
                    case "CreatedUserNameDesc":
                        data = data.OrderByDescending(x => x.CreatedUser.UserName).ToList();
                        break;
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }
        //Searching Helper
        private List<Employee> GetSearchingData(string searchby, string search, List<Employee> data)
        {
            switch (searchby)
            {
                case "EmployeeNo":
                    data = data.Where(x => x.EmployeeNo.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "Name":
                    data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "Rank":
                    data = data.Where(x => x.Rank.Description.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "CreatedUserName":
                    data = data.Where(x => x.CreatedUser.UserName.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "CreatedDate":
                    DateTime createdDate;
                    if (DateTime.TryParse(search, out createdDate))
                    {
                        data = data.Where(x => x.CreatedDate == createdDate).ToList();
                    }
                    break;
            }
            return data;
        }
        #endregion
        public JsonResult GetAllEmployee()
        {
            var data = EmployeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetEmployees()
        {

            var emp = EmployeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            return Json(emp, JsonRequestBehavior.AllowGet);
        }
        #region for Address drop down list event
        public JsonResult GetAddress()
        {
            var address = EmployeeServices.Address.GetByAll().Where(x => x.Active == true).ToList();
            // return new JsonResult { Data = address, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return Json(address.AsEnumerable().ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create


        // GET: Employee/Create
        public ActionResult Create()
        {
            ddlDatabind();
            return View();
        }



        //Remote Validation in MVC:that is grate in mvc.
        public ActionResult CheckEmployeeNoExists(EmployeeViewModel viewmodel)
        {
            bool EmployeeNoExists = false;
            try
            {
                bool checkemployee = EmployeeServices.Employee.GetByAll().Any(x => x.EmployeeNo == viewmodel.EmployeeNo && x.Active == true);
                if (!checkemployee)
                {
                    EmployeeNoExists = false;
                }
                else
                {
                    EmployeeNoExists = true;
                }
                return Json(!EmployeeNoExists, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeViewModel viewmodel, HttpPostedFileBase file)
        {
            if (string.IsNullOrEmpty(viewmodel.EmployeeNo)
                || string.IsNullOrEmpty(viewmodel.Name)
                || string.IsNullOrEmpty(viewmodel.Sex)
                || string.IsNullOrEmpty(viewmodel.AddressID)
                || string.IsNullOrEmpty(viewmodel.WorkType)
                || string.IsNullOrEmpty(viewmodel.Sex)
                || string.IsNullOrEmpty(viewmodel.RankID)
                || file == null
                || viewmodel.MobileNo.Equals(string.Empty))
            {
                ddlDatabind();
                ModelState.AddModelError("File", "Please Upload Your file.");
                return View(viewmodel);
            }
            bool checkemployee = EmployeeServices.Employee.GetByAll().Any(x => x.EmployeeNo == viewmodel.EmployeeNo && x.Active == true);
            bool usercheck = IdentityUserManager.Users.Any(x => x.Active == true && x.UserName.Replace(" ", string.Empty) == viewmodel.Name.Replace(" ", string.Empty));
            if ((!checkemployee && file.ContentLength > 0) && !usercheck)
            {
                int MaxContentLength = 1024 * 1024 * 3;
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)
                var extension = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)
                if (file.ContentLength > MaxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    ddlDatabind();
                    return View(viewmodel);
                }
                if (allowedExtensions.Contains(extension)) //check what type of extension
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension
                    string myfile = name + "_" + DateTime.Now.ToString("yymmssff") + extension; //appending the name with current date time
                    var path = Path.Combine(Server.MapPath("~/Images/"), myfile);    // store the file inside ~/project folder(Img)
                    var imageurl = "~/Images/" + myfile;
                    Employee model = new Employee()
                    {
                        EmployeeID = Guid.NewGuid().ToString(),
                        Name = viewmodel.Name,
                        EmployeeNo = viewmodel.EmployeeNo,
                        ImagePath = imageurl, //getting complete url
                        RankID = viewmodel.RankID,
                        WorkType = viewmodel.WorkType,
                        AddressID = viewmodel.AddressID,
                        PhoneNo = viewmodel.PhoneNo,
                        MobileNo = viewmodel.MobileNo,
                        BasicPay = viewmodel.BasicPay,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id,
                        Sex = viewmodel.Sex,
                        DOB = viewmodel.DOB,
                        JoinDate = viewmodel.JoinDate,
                        NRC = viewmodel.NRC,
                        Active = true
                    };
                    //First >>Create Application User[User Acc]
                    var appUser = new ApplicationUser();
                    appUser.Id = Guid.NewGuid().ToString();
                    appUser.UserName = model.Name;
                    appUser.Email = viewmodel.Name + "@gmail.com";
                    appUser.PhoneNumber = Convert.ToString(viewmodel.PhoneNo);
                    appUser.FullName = model.Name;
                    appUser.Designation = "Employee";
                    appUser.Fax = Convert.ToString(viewmodel.MobileNo);
                    appUser.Active = true;
                    appUser.CreatedDate = DateTime.Now;
                    //save the Employee Record
                    EmployeeServices.Employee.Add(model);
                    EmployeeServices.Save();
                    file.SaveAs(path);
                    //Create the User with specify user name and password
                    IdentityResult result = IdentityUserManager.Create(appUser, "cbrms");
                    if (result.Succeeded)
                    {
                        //Second Create Role for Created Application User[User Acc]
                        var _employeerole = IdentityRoleManager.FindByName("employee");
                        //Create the Role Record
                        IdentityUserManager.AddToRole(appUser.Id, _employeerole.Name);
                        UsersMember usersMember = new UsersMember()
                        {
                            UsersMemberID = Guid.NewGuid().ToString(),
                            UserID = appUser.Id,
                            UserInMemberID = model.EmployeeID,
                            CreatedDate = DateTime.Now,
                            CreatedUserID = appUser.Id,
                            MemberStatus = "e",
                            Active = true
                        };
                        //Create the Users Member Record!
                        UsersMemberServices.UsersMember.Add(usersMember);
                        UsersMemberServices.Save();
                    }
                    Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.Name), true);
                    return RedirectToAction("Index");
                }// end of check what type of extension
                else
                {
                    ddlDatabind();
                    ModelState.AddModelError("Files", "Only jpeg, png, jpg, Jpg format are allowed.Please select a valid logo picture.");
                }
            }//end of check data exist
            else
            {
                ddlDatabind();
                Warning(string.Format("<b>{0}</b> was already existed in the system.", viewmodel.Name), false);
                return RedirectToAction("Index");
            }
            return View();
        }

        private void ddlDatabind()
        {
            ViewBag.Address = new SelectList(EmployeeServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City");
            ViewBag.Rank = new SelectList(EmployeeServices.Rank.GetByAll().Where(x => x.Active == true).ToList(), "RankID", "Description");
        }
        #endregion

        // GET: Employee/Edit/5
        public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                Employee model = EmployeeServices.Employee.GetByID(Id);
                EmployeeViewModel viewmodel = new EmployeeViewModel();
                viewmodel.EmployeeID = model.EmployeeID;
                viewmodel.Name = model.Name;
                viewmodel.ImagePath = model.ImagePath;
                viewmodel.EmployeeNo = model.EmployeeNo;
                viewmodel.Sex = model.Sex;
                viewmodel.NRC = model.NRC;
                viewmodel.JoinDate = model.JoinDate;
                viewmodel.DOB = model.DOB;
                viewmodel.NRC = model.NRC;
                viewmodel.WorkType = model.WorkType;
                viewmodel.PhoneNo = model.PhoneNo;
                viewmodel.MobileNo = model.MobileNo;
                viewmodel.BasicPay = model.BasicPay;
                ViewBag.Address = new SelectList(EmployeeServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City", model.AddressID);
                ViewBag.Rank = new SelectList(EmployeeServices.Rank.GetByAll().Where(x => x.Active == true).ToList(), "RankID", "Description", model.RankID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel viewmodel, HttpPostedFileBase file)
        {
            try
            {
                if (string.IsNullOrEmpty(viewmodel.EmployeeNo)
                 || string.IsNullOrEmpty(viewmodel.Name)
                 || string.IsNullOrEmpty(viewmodel.Designation)
                 || string.IsNullOrEmpty(viewmodel.AddressID)
                 || string.IsNullOrEmpty(viewmodel.WorkType)
                 || file == null
                 || viewmodel.MobileNo.Equals(string.Empty))
                {
                    ViewBag.Address = new SelectList(EmployeeServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City");
                    ModelState.AddModelError("File", "Please Upload Your file");
                    return View(viewmodel);
                }
                if (file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3;
                    var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)
                    var extension = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)
                    if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        ViewBag.Address = new SelectList(EmployeeServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City");
                        return View(viewmodel);
                    }
                    if (allowedExtensions.Contains(extension)) //check what type of extension
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension
                        string myfile = name + "_" + DateTime.Now.ToString("yymmssff") + extension; //appending the name with current date time
                        var path = Path.Combine(Server.MapPath("~/Images/"), myfile);    // store the file inside ~/project folder(Img)
                        var imageurl = "~/Images/" + myfile;
                        Employee model = EmployeeServices.Employee.GetByID(viewmodel.EmployeeID);
                        model.Name = viewmodel.Name;
                        model.EmployeeNo = viewmodel.EmployeeNo;
                        model.Sex = viewmodel.Sex;
                        model.JoinDate = viewmodel.JoinDate;
                        model.DOB = viewmodel.DOB;
                        model.NRC = viewmodel.NRC;
                        model.ImagePath = imageurl; //getting complete url
                        model.WorkType = viewmodel.WorkType;
                        model.PhoneNo = viewmodel.PhoneNo;
                        model.MobileNo = viewmodel.MobileNo;
                        model.BasicPay = viewmodel.BasicPay;
                        model.AddressID = viewmodel.AddressID;
                        model.RankID = viewmodel.RankID;
                        EmployeeServices.Employee.Update(model);
                        EmployeeServices.Save();
                        file.SaveAs(path);
                        Success(string.Format("<b>{0}</b> was successfully Updated to the system.", viewmodel.Name), true);
                        return RedirectToAction("Index");

                    }//end of allow file extenstion
                }//end of file.content.length>0
            }//end of try
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator");
            }
            return View();
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Employee model = EmployeeServices.Employee.GetByID(id);
                EmployeeViewModel viewmodel = new EmployeeViewModel();
                viewmodel.EmployeeID = model.EmployeeID;
                viewmodel.Name = model.Name;
                viewmodel.ImagePath = model.ImagePath;
                viewmodel.EmployeeNo = model.EmployeeNo;
                viewmodel.Sex = model.Sex;
                viewmodel.NRC = model.NRC;
                viewmodel.JoinDate = model.JoinDate;
                viewmodel.DOB = model.DOB;
                viewmodel.NRC = model.NRC;
                viewmodel.WorkType = model.WorkType;
                viewmodel.PhoneNo = model.PhoneNo;
                viewmodel.MobileNo = model.MobileNo;
                viewmodel.BasicPay = model.BasicPay;
                viewmodel.Address = EmployeeServices.Address.GetByID(model.AddressID);
                viewmodel.Rank = EmployeeServices.Rank.GetByID(model.RankID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(EmployeeViewModel viewmodel)
        {
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrEmpty(viewmodel.EmployeeID))
                {
                    Employee model = EmployeeServices.Employee.GetByID(viewmodel.EmployeeID);
                    model.Active = false;
                    EmployeeServices.Employee.Update(model);
                    EmployeeServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.Name), true);
                    return RedirectToAction("Index");

                }
            }
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator");
            }
            return View();
        }
        public ActionResult ExportToExcel()
        {
            List<EmployeeViewModel> emplist = new List<EmployeeViewModel>();
            var data = EmployeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            if (data.Count > 0)
            {
                foreach (var emp in data)
                {
                    EmployeeViewModel viewmodel = new EmployeeViewModel();
                    viewmodel.EmployeeNo = emp.EmployeeNo;
                    viewmodel.Name = emp.Name;
                    viewmodel.PhoneNo = emp.PhoneNo;
                    viewmodel.MobileNo = emp.MobileNo;
                    viewmodel.BasicPay = emp.BasicPay;
                    viewmodel.Sex = emp.Sex;
                    viewmodel.NRC = emp.NRC;
                    viewmodel.JoinDate = emp.JoinDate;
                    viewmodel.DOB = emp.DOB;
                    viewmodel.Rank = EmployeeServices.Rank.GetByID(emp.RankID);
                    viewmodel.WorkType = emp.WorkType;
                    viewmodel.Address = EmployeeServices.Address.GetByID(emp.AddressID);
                    viewmodel.ImagePath = emp.ImagePath;
                    emplist.Add(viewmodel);

                }
                GridView grid = new GridView();
                //assign data to gridview
                grid.DataSource = emplist;
                //bind data
                grid.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                //Adding name to excel file
                Response.AddHeader("content-disposition", "attachment;filename=sampleExcel.xls");
                //specify content type of file
                //Here i specified "ms-excel" format
                //you can also specify it "ms-word" to get word document
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htwriter = new HtmlTextWriter(sw))
                    {
                        grid.RenderControl(htwriter);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.Close();
                    }
                }
            }
            Response.Redirect("~/AspNetForms/aspnetgeneric.aspx");
            return RedirectToAction("Index");
        }
        //public ActionResult CreatePdf()
        //{
        //    var emp = EmployeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
        //    WebGrid grid = new WebGrid(source: emp, canPage: false, canSort: false);
        //    string gridHtml = grid.GetHtml(
        //           columns: grid.Columns(
        //                    grid.Column("Id", "Id"),
        //                    grid.Column("Name", "Name"),
        //                    grid.Column("Phone", "Phone"),
        //                    grid.Column("Salary", "Salary"),
        //                    grid.Column("Department", "Department"),
        //                     grid.Column("EmailId", "Email")
        //                   )
        //            ).ToString();
        //    string exportData = String.Format("{0}{1}", "", gridHtml);
        //    var bytes = System.Text.Encoding.UTF8.GetBytes(exportData);
        //    using (var input = new MemoryStream(bytes))
        //    {
        //        var output = new MemoryStream();
        //        var document = new Document(PageSize.A4, 50, 50, 50, 50);
        //        var writer = PdfWriter.GetInstance(document, output);
        //        writer.CloseStream = false;
        //        document.Open();
        //        var xmlWorker = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
        //        xmlWorker.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);
        //        document.Close();
        //        output.Position = 0;
        //        return File(output, "application/pdf", "EmployeePDF.pdf");
        //    }
        //}
        public FileContentResult ExportToCSV()
        {
            var data = EmployeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"EmployeeNo\",\"Employee Name\",\"sex\",\"BasicPay\"");
            foreach (var emp in data)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                                            emp.EmployeeNo,
                                           emp.Name,
                                           emp.Sex,
                                           emp.BasicPay));
            }
            var fileName = "EmployeeList" + DateTime.Now.ToString() + ".csv";
            return File(new System.Text.UTF8Encoding().GetBytes(sw.ToString()), "text/csv", fileName);

        }

        #region Create Rank record
        [HttpPost]
        public JsonResult CreateRank(RankViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool checkrank = rankServices.Rank.GetByAll().Any(x => x.Code == viewmodel.Code && x.Active == true);
                if (!checkrank)
                {
                    Rank model = new Rank()
                    {
                        RankID = Guid.NewGuid().ToString(),
                        Code = viewmodel.Code,
                        Description = viewmodel.Description,
                        Active = true,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id
                    };
                    if (rankServices.Rank.Add(model))
                    {
                        rankServices.Save();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create Address Record
        [HttpPost]
        public JsonResult CreateAddress(AddressViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool checkaddress = addressServices.Address.GetByAll().Any(x => x.City == viewmodel.City && x.Active == true);
                if (!checkaddress)
                {
                    Address model = new Address()
                    {
                        AddressID = Guid.NewGuid().ToString(),
                        City = viewmodel.City,
                        Township = viewmodel.Township,
                        Place = viewmodel.Place,
                        ZipCode = viewmodel.ZipCode,
                        Area = viewmodel.Area,
                        Active = true,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id
                    };
                    if (addressServices.Address.Add(model))
                    {
                        addressServices.Save();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}
