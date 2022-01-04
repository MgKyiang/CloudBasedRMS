namespace CloudBasedRMS.View.Controllers
{
    using Core;
    using CrystalDecisions.CrystalReports.Engine;
    using PagedList;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModel;

    public class SaleBillController : ControllerAuthorizeBase
    {
        #region    //create instance of services
        public SaleBillServices SaleBillServices;
        public OrderMasterServices OrderMasterServices;
        public TableServices tableServices;
        public RestaurantProfileServices restaurantProfileServices;
        public SaleBillController()
        {
            SaleBillServices = new SaleBillServices();
            OrderMasterServices = new OrderMasterServices();
            tableServices = new TableServices();
            restaurantProfileServices = new RestaurantProfileServices();
        }
        #endregion


        #region get salebill by rodermaster id json method
        [HttpGet]
        public JsonResult GettSaleBillbyOrderOrderMasterID(string OrderMasterID)
        {
            decimal result = CalcBillByOrderMasterID(OrderMasterID);
            if (result == 0)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public decimal CalcBillByOrderMasterID(string OrderMasterID)
        {
            List<OrderItems> orderitems = new List<OrderItems>();
            orderitems = OrderMasterServices.OrderItems.GetOrderItemsByOrderMasterID(OrderMasterID).ToList();
            if (orderitems == null)
            {
                return 0;
            }
            var data = orderitems.Select(x => x.Amount);
            decimal totalamount = 0;
            foreach (var amt in data)
            {
                totalamount += amt;
            }
            return totalamount;
        }

        #endregion

        #region GET: SaleBill
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            var data = SaleBillServices.SaleBill.GetByAll().Where(x => x.Active == true).OrderByDescending(x => x.CreatedDate).ToList();
            //Sorting ViewBag
            ViewBag.SaleBillDateSortby = sortby == "SaleBillDateAsc" ? "SaleBillDateDesc" : "SaleBillDateAsc";
            ViewBag.SaleBillNoSortby = sortby == "SaleBillNoAsc" ? "SaleBillNoDesc" : "SaleBillNoAsc";
            ViewBag.OrderMasterSortby = sortby == "OrderMasterAsc" ? "OrderMasterDesc" : "OrderMasterAsc";
            ViewBag.NetAmountSortBy = sortby == "NetAmountAsc" ? "NetAmountDesc" : "NetAmountAsc";
            ViewBag.CustomerSortBy = sortby == "CustomerAsc" ? "CustomerDesc" : "CustomerAsc";
            ViewBag.CasherSortBy = sortby == "EmployeeAsc" ? "EmployeeDesc" : "EmployeeAsc";
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
                    case "SaleBillDateAsc":
                        data = data.OrderBy(x => x.SaleBillDate).ToList();
                        break;
                    case "SaleBillDateDesc":
                        data = data.OrderByDescending(x => x.SaleBillDate).ToList();
                        break;
                    case "SaleBillNoAsc":
                        data = data.OrderBy(x => x.SaleBillNo).ToList();
                        break;
                    case "SaleBillNoDesc":
                        data = data.OrderByDescending(x => x.SaleBillNo).ToList();
                        break;
                    case "OrderMasterAsc":
                        data = data.OrderBy(x => x.OrderMaster.OrderNo).ToList();
                        break;
                    case "OrderMasterDesc":
                        data = data.OrderByDescending(x => x.OrderMaster.OrderNo).ToList();
                        break;
                    case "NetAmountAsc":
                        data = data.OrderBy(x => x.NetAmount).ToList();
                        break;
                    case "NetAmountDesc":
                        data = data.OrderByDescending(x => x.NetAmount).ToList();
                        break;
                    case "CustomerAsc":
                        data = data.OrderBy(x => x.Customer.Name).ToList();
                        break;
                    case "CustomerDesc":
                        data = data.OrderByDescending(x => x.Customer.Name).ToList();
                        break;
                    case "EmployeeAsc":
                        data = data.OrderBy(x => x.Employee.Name).ToList();
                        break;
                    case "EmployeeDesc":
                        data = data.OrderByDescending(x => x.Employee.Name).ToList();
                        break;
                }
            }
            ViewBag.SaleBillbyTable = SaleBillServices.OrderMaster.GetByAll().Where(x => x.Active == true && x.OrderStatus == "ReadyPickup" && x.IsBillPaid == false).ToList();
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }

        private List<SaleBill> GetSearchingData(string searchby, string search, List<SaleBill> data)
        {
            switch (searchby)
            {
                case "SaleBillNo":
                    data = data.Where(x => x.SaleBillNo.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "OrderNo":
                    data = data.Where(x => x.OrderMaster.OrderNo.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "NetAmount":
                    data = data.Where(x => x.NetAmount == Convert.ToDecimal(search)).ToList();
                    break;
                case "SaleBillDate":
                    DateTime OrderDate;
                    if (DateTime.TryParse(search, out OrderDate))
                    {
                        data = data.Where(x => x.SaleBillDate == OrderDate).ToList();
                    }
                    break;
                case "Customer":
                    data = data.Where(x => x.Customer.Name.ToLower().Contains(search.ToLower())).ToList();
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
                case "Employee":
                    data = data.Where(x => x.Employee.Name.ToLower().Contains(search.ToLower())).ToList();
                    break;

            }
            return data;
        }
        #endregion

        #region Export Sale Bill
        public ActionResult ExportSaleBill()
        {
            var data = SaleBillServices.SaleBill.GetByAll().Where(x => x.Active == true).ToList();
            if (data.Count == 0)
            {
                TempData["Msg"] = "There is no record to export.";
                return View("Index", data);
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "SaleBill.rpt"));
            rd.SetDataSource(data.Select(p => new { SaleBillNo = p.SaleBillNo, SaleBillDate = p.SaleBillDate, CasherName = p.Employee.Name, OrderNo = p.OrderMaster.OrderNo, CustomerName = p.Customer.Name, Amount = p.Amount, Tax = p.Tax, Discount = p.Discount, NetAmount = p.NetAmount }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "SaleBill.pdf");
        }
        #endregion

        #region Export ExportSaleBillGroupByCasher
        public ActionResult ExportSaleBillGroupByCasher()
        {
            var data = from s in SaleBillServices.SaleBill.GetByAll().Where(x => x.Active == true).ToList()
                       join r in restaurantProfileServices.RestaurantProfile.GetByAll().Where(x => x.Active == true).ToList()
                       on s.Active equals r.Active
                       select new
                       {
                           s.SaleBillNo,
                           s.SaleBillDate,
                           s.Employee,
                           s.OrderMaster,
                           s.Customer,
                           s.Amount,
                           s.Tax,
                           s.Discount,
                           s.NetAmount,
                           r.RestaurantName,
                           r.ContactAddress,
                           r.EmailAddress,
                           r.FacebookAddress,
                           r.Phone
                       };
            if (data.Equals(0))
            {
                TempData["Msg"] = "There is no record to export.";
                return View("Index", data);
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "SaleBillGroupbyCasher.rpt"));
            rd.SetDataSource(data.Select(p => new
            {
                SaleBillNo = p.SaleBillNo,
                SaleBillDate = p.SaleBillDate.ToString("dd/MM/yyyy"),
                CasherName = p.Employee.Name,
                OrderNo = p.OrderMaster.OrderNo,
                CustomerName = p.Customer.Name,
                Amount = p.Amount,
                Tax = p.Tax,
                Discount = p.Discount,
                NetAmount = p.NetAmount,
                CasherID = p.Employee.Name,
                Name = p.RestaurantName,
                Address = p.ContactAddress,
                gmail = p.EmailAddress,
                facebook = p.FacebookAddress,
                Phone = p.Phone
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "SaleBillGroupbyCashier.pdf");
        }
        #endregion

        #region Create
        // GET: SaleBill/Create
        public ActionResult Create()
        {
            SaleBillViewModel viewmodel = new SaleBillViewModel();
            List<Employee> employeelist = SaleBillServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            employeelist.Insert(0, new Employee { EmployeeID = "1", Name = "-Select One-" });
            ViewBag.Employee = new SelectList(employeelist, "EmployeeID", "Name");
            List<Customer> customerlist = SaleBillServices.Customer.GetByAll().Where(x => x.Active == true).ToList();
            customerlist.Insert(0, new Customer { CustomerID = "1", Name = "-Select One-" });
            ViewBag.Customer = new SelectList(customerlist, "CustomerID", "Name");
            List<OrderMaster> ordermasterlist = SaleBillServices.OrderMaster.GetByAll().Where(x => x.Active == true && x.OrderStatus == "ConfirmOrder" && x.IsBillPaid == false).ToList();
            ordermasterlist.Insert(0, new OrderMaster { OrderMasterID = "1", OrderNo = "-Select One-" });
            ViewBag.OrderMaster = new SelectList(ordermasterlist, "OrderMasterID", "OrderNo");
            string generateCode = "S" + DateTime.Now.ToString("HH:mm:ss:fffffff").Replace(":", "");//+ DateTime.Now.ToString("dd/MM/yy") + "_"
            viewmodel.SaleBillNo = generateCode;
            viewmodel.SaleBillDate = DateTime.Now;

            return View(viewmodel);

        }
        public ActionResult ConfirmBill(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "SaleBill");
            }
            SaleBillViewModel viewmodel = new SaleBillViewModel();
            List<Employee> employeelist = SaleBillServices.Employee.GetByAll().Where(x => x.CreatedUserID == CurrentApplicationUser.Id).ToList();
            ViewBag.Employee = new SelectList(employeelist, "EmployeeID", "Name");
            List<Customer> customerlist = SaleBillServices.Customer.GetByAll().Where(x => x.Active == true).ToList();
            customerlist.Insert(0, new Customer { CustomerID = "1", Name = "-Select One-" });
            ViewBag.Customer = new SelectList(customerlist, "CustomerID", "Name");
            List<OrderMaster> ordermasterlist = SaleBillServices.OrderMaster.GetByAll().Where(x => x.OrderMasterID == id).ToList();
            ViewBag.OrderMaster = new SelectList(ordermasterlist, "OrderMasterID", "OrderNo");
            string generateCode = "S" + DateTime.Now.ToString("HH:mm:ss:fffffff").Replace(":", "");//+ DateTime.Now.ToString("dd/MM/yy") + "_"
            viewmodel.SaleBillNo = generateCode;
            viewmodel.SaleBillDate = DateTime.Now;
            return View("Create", viewmodel);
        }
        // POST: SaleBill/Create
        [HttpPost]
        public JsonResult Create(SaleBillViewModel viewmodel)
        {
            // TODO: Add insert logic here
            string redirectUrl = string.Empty;
            if (CheckRecord(viewmodel))
            {
                bool checkdata = SaleBillServices.SaleBill.GetByAll().Any(x => x.SaleBillNo == viewmodel.SaleBillNo && x.Active == true);
                if (!checkdata)
                {
                    //get sale bill record
                    SaleBill model = new SaleBill()
                    {
                        SaleBillID = Guid.NewGuid().ToString(),
                        SaleBillNo = viewmodel.SaleBillNo,
                        SaleBillDate = viewmodel.SaleBillDate,
                        EmployeeID = viewmodel.EmployeeID,
                        CustomerID = viewmodel.CustomerID,
                        OrderMasterID = viewmodel.OrderMasterID,
                        Amount = viewmodel.Amount,
                        Tax = viewmodel.Tax,
                        Discount = viewmodel.Discount,
                        NetAmount = viewmodel.NetAmount,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id,
                        Active = true
                    };
                    List<BillFoodItems> billfootitemlist = new List<BillFoodItems>();
                    //get order item list by order master id
                    List<OrderItems> orderitemslist = SaleBillServices.OrderItems.GetByAll().Where(x => x.OrderMasterID == model.OrderMasterID).ToList();
                    foreach (var item in orderitemslist)
                    {
                        BillFoodItems billFoodItems = new BillFoodItems();
                        billFoodItems.BillFoodItemsID = Guid.NewGuid().ToString();
                        billFoodItems.SaleBillID = model.SaleBillID;
                        billFoodItems.FoodItemID = item.FoodItemID;
                        billFoodItems.Quantity = item.Quantity;
                        billFoodItems.RatePerItems = item.RatePerItems;
                        billFoodItems.Amount = item.Amount;
                        billFoodItems.CreatedDate = DateTime.Now;
                        billFoodItems.CreatedUserID = CurrentApplicationUser.Id;
                        billFoodItems.Active = true;
                        billfootitemlist.Add(billFoodItems);
                    }
                    OrderMaster ordermodel = OrderMasterServices.OrderMaster.GetByID(viewmodel.OrderMasterID);
                    ordermodel.IsBillPaid = true;
                    OrderMasterServices.OrderMaster.Update(ordermodel);
                    OrderMasterServices.Save();
                    //keep sale bill record
                    SaleBillServices.SaleBill.Add(model);
                    //keep bill food item record
                    if (SaleBillServices.BillFoosItems.AddRange(billfootitemlist))
                    {
                        SaleBillServices.Save();
                        OrderMaster ordermaster = OrderMasterServices.OrderMaster.GetByID(viewmodel.OrderMasterID);
                        Core.Tables tablemodel = tableServices.Table.GetByID(ordermaster.TableID);
                        tablemodel.IsAvailable = true;
                        tablemodel.Status = "This table is checked out at " + DateTime.Now + ".";
                        tableServices.Table.Update(tablemodel);
                        tableServices.Save();
                    }
                    Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.SaleBillNo), true);
                    return Json(new { redirectUrl = Url.Action("Index", "SaleBill"), isRedirect = true });
                }
                else
                {
                    Success(string.Format("<b>{0}</b> was already existed in the system.", viewmodel.SaleBillNo), true);
                    return Json(new { redirectUrl = Url.Action("Create", "SaleBill"), isRedirect = true });

                }
            }
            ViewBag.Employee = new SelectList(SaleBillServices.Employee.GetByAll().Where(x => x.Active == true).ToList(), "EmployeeID", "Name");
            ViewBag.Customer = new SelectList(SaleBillServices.Customer.GetByAll().Where(x => x.Active == true).ToList(), "CustomerID", "Name");
            ViewBag.OrderMaster = new SelectList(SaleBillServices.OrderMaster.GetByAll().Where(x => x.Active == true).ToList(), "OrderMasterID", "OrderNo");
            return Json(new { redirectUrl = Url.Action("Index", "SaleBill"), isRedirect = true });

        }

        private bool CheckRecord(SaleBillViewModel viewmodel)
        {
            bool result = true;
            if (string.IsNullOrEmpty(viewmodel.SaleBillNo) ||
                viewmodel.SaleBillDate == DateTime.MinValue ||
                string.IsNullOrEmpty(viewmodel.OrderMasterID) ||
                string.IsNullOrEmpty(viewmodel.CustomerID) ||
                viewmodel.NetAmount == 0 ||
                string.IsNullOrEmpty(viewmodel.EmployeeID) ||
                viewmodel.Amount == 0)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Edit
        // GET: SaleBill/Edit/5
        public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                SaleBill model = SaleBillServices.SaleBill.GetByID(Id);
                SaleBillViewModel viewmodel = new SaleBillViewModel();
                viewmodel.SaleBillID = model.SaleBillID;
                viewmodel.SaleBillNo = model.SaleBillNo;
                viewmodel.SaleBillDate = model.SaleBillDate;
                viewmodel.Discount = model.Discount;
                viewmodel.Amount = model.Amount;
                viewmodel.Tax = model.Tax;
                viewmodel.NetAmount = model.NetAmount;
                ViewBag.Employee = new SelectList(SaleBillServices.Employee.GetByAll().Where(x => x.Active == true).ToList(), "EmployeeID", "Name", model.EmployeeID);
                ViewBag.Customer = new SelectList(SaleBillServices.Customer.GetByAll().Where(x => x.Active == true).ToList(), "CustomerID", "Name", model.CustomerID);
                ViewBag.OrderMaster = new SelectList(SaleBillServices.OrderMaster.GetByAll().Where(x => x.Active == true).ToList(), "OrderMasterID", "OrderNo", model.OrderMasterID);
                return View(viewmodel);
            }
            return View();
        }
        // POST: SaleBill/Edit/5
        [HttpPost]
        public ActionResult Edit(SaleBillViewModel viewmodel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    SaleBill model = SaleBillServices.SaleBill.GetByID(viewmodel.SaleBillID);
                    model.SaleBillNo = viewmodel.SaleBillNo;
                    model.SaleBillDate = viewmodel.SaleBillDate;
                    model.EmployeeID = viewmodel.EmployeeID;
                    model.CustomerID = viewmodel.CustomerID;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedUserID = CurrentApplicationUser.Id;
                    model.OrderMasterID = viewmodel.OrderMasterID;
                    model.Tax = viewmodel.Tax;
                    model.Amount = viewmodel.Amount;
                    model.NetAmount = viewmodel.NetAmount;
                    model.Discount = viewmodel.Discount;
                    //for bill food item record update
                    IEnumerable<BillFoodItems> billfooditems = SaleBillServices.BillFoosItems.GetBySaleBillID(viewmodel.SaleBillID);
                    List<OrderItems> orderitemslist = SaleBillServices.OrderItems.GetByAll().Where(x => x.OrderMasterID == model.OrderMasterID).ToList();
                    foreach (var orderitem in orderitemslist)
                    {
                        foreach (var item in billfooditems)
                        {
                            item.FoodItemID = orderitem.FoodItemID;
                            item.Quantity = orderitem.Quantity;
                            item.RatePerItems = orderitem.RatePerItems;
                            item.Amount = orderitem.Amount;
                            SaleBillServices.BillFoosItems.Update(item);
                        }
                    }
                    SaleBillServices.SaleBill.Update(model);
                    SaleBillServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully updated to the system.", viewmodel.SaleBillNo), true);
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
        #endregion

        #region Delete
        // GET: SaleBill/Delete/5
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                SaleBill model = SaleBillServices.SaleBill.GetByID(id);
                SaleBillViewModel viewmodel = new SaleBillViewModel();
                viewmodel.SaleBillID = model.SaleBillID;
                viewmodel.SaleBillNo = model.SaleBillNo;
                viewmodel.SaleBillDate = model.SaleBillDate;
                viewmodel.Discount = model.Discount;
                viewmodel.Amount = model.Amount;
                viewmodel.Tax = model.Tax;
                viewmodel.NetAmount = model.NetAmount;
                viewmodel.Employee = SaleBillServices.Employee.GetByID(model.EmployeeID);
                viewmodel.Customer = SaleBillServices.Customer.GetByID(model.CustomerID);
                viewmodel.OrderMaster = SaleBillServices.OrderMaster.GetByID(model.OrderMasterID);
                return View(viewmodel);
            }
            return View();
        }
        // POST: SaleBill/Delete/5
        [HttpPost]
        public ActionResult Delete(SaleBillViewModel viewmodel)
        {
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrEmpty(viewmodel.SaleBillID))
                {
                    SaleBill model = SaleBillServices.SaleBill.GetByID(viewmodel.SaleBillID);
                    model.Active = false;
                    SaleBillServices.SaleBill.Update(model);
                    IEnumerable<BillFoodItems> billfooditems = SaleBillServices.BillFoosItems.GetBySaleBillID(viewmodel.SaleBillID);
                    foreach (var item in billfooditems)
                    {
                        item.Active = false;
                        SaleBillServices.BillFoosItems.Update(item);
                    }
                    SaleBillServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted to the system.", viewmodel.SaleBillNo), true);
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
        #endregion
        public ActionResult SaleRecordDetail()
        {
            var data = SaleBillServices.SaleBill.GetByAll().Where(x => x.Active == true).ToList();
            return View(data);
        }
    }
}
