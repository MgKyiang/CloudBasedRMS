namespace CloudBasedRMS.View.Controllers
{
    using ViewModel;
    using Core;
    using Services;
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Web.Mvc;
    using PagedList;
    using System.Collections.Generic;
    using System.Web;
    using System.Data.OleDb;
    using System.Data;
    using LinqToExcel;
    using System.Data.Entity.Validation;
    using Microsoft.Reporting.WebForms;
    using System.Web.UI.WebControls;
    public  class TableController:ControllerAuthorizeBase
    {
        public TableServices tableServices;
        public EmployeeServices employeeServices;
        public TableController()
        {
            tableServices = new TableServices();
            employeeServices = new EmployeeServices();
        }
        // GET: Table
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            var data = tableServices.Table.GetByAll().Where(x => x.Active == true).ToList();
            //Sorting ViewBag
            ViewBag.TableNoSortby = sortby == "TableNoAsc" ? "TableNoDesc" : "TableNoAsc";
            ViewBag.CapacitySortby = sortby == "CapacityAsc" ? "CapacityDesc" : "CapacityAsc";
            ViewBag.IsAvailableSortby = sortby == "IsAvailableAsc" ? "IsAvailableDesc" : "IsAvailableAsc";
            ViewBag.StatusSortBy = sortby == "StatusAsc" ? "StatusDesc" : "StatusAsc";
            ViewBag.EmployeeSortBy = sortby == "EmployeeAsc" ? "EmployeeDesc" : "EmployeeAsc";
            
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
                    case "TableNoAsc":
                        data = data.OrderBy(x => x.TableNo).ToList();
                        break;
                    case "TableNoDesc":
                        data = data.OrderByDescending(x => x.TableNo).ToList();
                        break;
                    case "CapacityAsc":
                        data = data.OrderBy(x => x.Capacity).ToList();
                        break;
                    case "CapacityDesc":
                        data = data.OrderByDescending(x => x.Capacity).ToList();
                        break;
                    case "IsAvailableAsc":
                        data = data.OrderBy(x => x.IsAvailable).ToList();
                        break;
                    case "IsAvailableDesc":
                        data = data.OrderByDescending(x => x.IsAvailable).ToList();
                        break;

                    case "StatusAsc":
                        data = data.OrderBy(x => x.Status).ToList();
                        break;
                    case "StatusDesc":
                        data = data.OrderByDescending(x => x.Status).ToList();
                        break;
                    case "EmployeeAsc":
                        data = data.OrderBy(x => x.Employee.Name).ToList();
                        break;
                    case "EmployeeDesc":
                        data = data.OrderByDescending(x => x.Employee.Name).ToList();
                        break;                  
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }
        //Searching Helper
        private List<Tables> GetSearchingData(string searchby, string search, List<Tables> data)
        {
            switch (searchby)
            {
                case "TableNo":
                    data = data.Where(x => x.TableNo.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "Status":
                    data = data.Where(x => x.Status.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "IsAvailable":
                    data = data.Where(x => x.IsAvailable == Convert.ToBoolean(search)).ToList();
                    break;
                case "Capacity":
                    data = data.Where(x => x.Capacity == Convert.ToDecimal(search)).ToList();
                    break;
                case "Employee":
                    data = data.Where(x => x.Employee.Name.ToLower().Contains(search.ToLower())).ToList();
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
        #region Create
        // GET: Table/Create
        public ActionResult Create()
        {
            ViewBag.Employee = new SelectList(tableServices.Employee.GetByAll().Where(x => x.Active == true).ToList(), "EmployeeID", "Name");
            return View();
        }

        // POST: Table/Create
        [HttpPost]
        public ActionResult Create(TableViewModel viewmodel)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                bool checkdata = tableServices.Table.GetByAll().Any(x => x.TableNo == viewmodel.TableNo && x.Active == true);
                if (!checkdata)
                {
                    Tables model = new Tables()
                    {
                        TableID = Guid.NewGuid().ToString(),
                        TableNo = viewmodel.TableNo,
                        Capacity = viewmodel.Capacity,
                        Status = viewmodel.Status,
                        EmployeeID = viewmodel.EmployeeID,
                        IsAvailable= viewmodel.IsAvailable,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id,
                        Active = true
                    };
                    tableServices.Table.Add(model);
                    tableServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.TableNo), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning(string.Format("<b>{0}</b> was already existsted in the system.", viewmodel.TableNo), true);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Employee = new SelectList(tableServices.Employee.GetByAll().Where(x => x.Active == true).ToList(), "EmployeeID", "Name");
            return View();
        }
        #endregion
        // GET: Table/Edit/5
        public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                Tables model = tableServices.Table.GetByID(Id);
                TableViewModel viewmodel = new TableViewModel();
                viewmodel.TableID = model.TableID;
                viewmodel.TableNo = model.TableNo;
                viewmodel.Capacity = model.Capacity;
                viewmodel.IsAvailable = model.IsAvailable;
                viewmodel.Status = model.Status;
                ViewBag.Employee = new SelectList(tableServices.Employee.GetByAll().Where(x => x.Active == true).ToList(), "EmployeeID", "Name", model.EmployeeID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Table/Edit/5
        [HttpPost]
        public ActionResult Edit(TableViewModel viewmodel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Tables model = tableServices.Table.GetByID(viewmodel.TableID);
                    model.TableNo = viewmodel.TableNo;
                    model.Capacity = viewmodel.Capacity;
                    model.Status = viewmodel.Status;
                    model.IsAvailable = viewmodel.IsAvailable;
                    model.EmployeeID = viewmodel.EmployeeID;
                    tableServices.Table.Update(model);
                    tableServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully Updated to the system.", viewmodel.TableNo), true);
                    return RedirectToAction("Index");
                }

            }
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                Danger("Unable to save changes. Try again, and if the problem persists, see your system administrator");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator");
            }
            return View();
        }

        // GET: Table/Delete/5
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Tables model = tableServices.Table.GetByID(id);
                TableViewModel viewmodel = new TableViewModel();
                viewmodel.TableID = model.TableID;
                viewmodel.TableNo = model.TableNo;
                viewmodel.Capacity = model.Capacity;
                viewmodel.IsAvailable = model.IsAvailable;
                viewmodel.Status = model.Status;
                viewmodel.Employee = tableServices.Employee.GetByID(model.EmployeeID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Table/Delete/5
        [HttpPost]
        public ActionResult Delete(TableViewModel viewmodel)
        {
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrEmpty(viewmodel.TableID))
                {
                    Tables model = tableServices.Table.GetByID(viewmodel.TableID);
                    model.Active = false;
                    tableServices.Table.Update(model);
                    tableServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.TableNo), true);
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


        #region ImportExcel
        public ActionResult ImportExcel()
        {         
            return View();
        }
        /// <summary>  
        /// This function is used to download excel format.  
        /// </summary>  
        /// <param name="Path"></param>  
        /// <returns>file</returns>  
        public FileResult DownloadExcel()
        {
            string path = "/Doc/table_Template.xlsx";
            return File(path, "application/vnd.ms-excel", "table_Template.xlsx");
        }
        [HttpPost]
        public JsonResult UploadExcel(Tables table, HttpPostedFileBase FileUpload)
        {
            List<Tables> tableviewmodel = new List<Tables>();
            Employee employeemodel = new Employee();
            List<string> data = new List<string>();
            if (FileUpload != null && FileUpload.ContentLength > 0)
            {               
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    try
                    {
                        if (filename.EndsWith(".xls"))
                        {
                            connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                        }
                        else if (filename.EndsWith(".xlsx"))
                        {
                            connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                        }
                        var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                        var ds = new DataSet();

                        adapter.Fill(ds, "ExcelTable");

                        DataTable dtable = ds.Tables["ExcelTable"];
                    }
                    catch (Exception ex)
                    {
                        //alert message for invalid file format  
                        data.Add("<ul>");
                        data.Add("<li>The Microsoft.ACE.OLEDB.12.0 provider is not registered on the local machine.Please install it.</li>");
                        data.Add("</ul>");
                        data.ToArray();
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var tablelist = from t in excelFile.Worksheet<Tables>(sheetName) select t;
                    tableviewmodel= tablelist.ToList();
                    foreach (var item in tablelist)
                    {
                        try
                        {
                            if (item.TableNo != "" && item.Status != "" && item.EmployeeID != "" && item.Capacity!=0)
                            {
                                employeemodel = employeeServices.Employee.GetEmployeeByEmployeeName(item.EmployeeID);
                                if (employeemodel != null) {
                                    Core.Tables model = new Core.Tables();
                                    model.TableID = Guid.NewGuid().ToString();
                                    model.TableNo = item.TableNo;
                                    model.Status = item.Status;
                                    model.IsAvailable = item.IsAvailable;
                                    model.Capacity = item.Capacity;
                                    model.EmployeeID = employeemodel.EmployeeID;
                                    model.Active = true;
                                    model.CreatedUserID = CurrentApplicationUser.Id;
                                    model.CreatedDate = DateTime.Now;
                                    tableServices.Table.Add(model);
                                    tableServices.Save();
                                }else {
                                    data.Add("<ul>");
                                     data.Add("<li> Employee ID is invalid!</li>");
                                    data.Add("</ul>");
                                    data.ToArray();
                                    return Json(data, JsonRequestBehavior.AllowGet);
                                }
                                
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (item.TableNo == "" || item.TableNo == null) data.Add("<li> TableNo is required</li>");
                                if (item.Status == "" || item.Status == null) data.Add("<li> Status is required</li>");
                                if (item.EmployeeID == "" || item.EmployeeID == null) data.Add("<li>EmployeeID is required</li>");

                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }

                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        public ActionResult Report()
        {
            ReportViewer rptViewer = new ReportViewer();

            // ProcessingMode will be Either Remote or Local  
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(99);
            rptViewer.Height = Unit.Pixel(1000);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://localhost/ReportServer/");
            //rptViewer.ServerReport.ReportPath = this.SetReportPath();
            ViewBag.ReportViewer = rptViewer;
            return View();
        }

        #region Create Tables Record
        [HttpPost]
        public JsonResult CreateTable(TableViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool checkaddress = tableServices.Table.GetByAll().Any(x => x.TableNo == viewmodel.TableNo && x.Active == true);
                if (!checkaddress)
                {
                    Tables model = new Tables()
                    {
                        TableID = Guid.NewGuid().ToString(),
                        TableNo = viewmodel. TableNo,
                       Status = viewmodel.Status,
                       IsAvailable = viewmodel.IsAvailable,
                       Capacity = viewmodel.Capacity,
                       EmployeeID = viewmodel.EmployeeID,

                       Active = true,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id
                    };
                    if (tableServices.Table.Add(model))
                    {
                        tableServices.Save();
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
