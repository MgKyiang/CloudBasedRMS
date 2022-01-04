using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.Services.DomainServices;
using CloudBasedRMS.View.Controllers.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers.Domain
{
    public class KitchenController : ControllerAuthorizeBase
    {
        #region instance and index
        public KitchenServices kitchenServices;
        private KOTPickupServices kOTPickupServices;
        public OrderMasterServices orderMasterServices;
        public KitchenController()
        {
            kitchenServices = new KitchenServices();
            kOTPickupServices = new KOTPickupServices();
            orderMasterServices = new OrderMasterServices();
        }
        // GET: Kitchen
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            //Sorting ViewBag
            ViewBag.KitchenNameSortby = sortby == "KitchenNameAsc" ? "KitchenNameDesc" : "KitchenNameAsc";
            ViewBag.KitchenDescriptionSortby = sortby == "KitchenDescriptionAsc" ? "KitchenDescriptionDesc" : "KitchenDescriptionAsc";
            ViewBag.EmployeeSortby = sortby == "EmployeeAsc" ? "EmployeeDesc" : "EmployeeAsc";

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
            var data = kitchenServices.Kitchen.GetByAll().Where(x => x.Active == true).ToList();
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
                    case "KitchenNameAsc":
                        data = data.OrderBy(x => x.KitchenName).ToList();
                        break;
                    case "KitchenNameDesc":
                        data = data.OrderByDescending(x => x.KitchenName).ToList();
                        break;
                    case "KitchenDescriptionAsc":
                        data = data.OrderBy(x => x.KitchenDescription).ToList();
                        break;
                    case "KitchenDescriptionDesc":
                        data = data.OrderByDescending(x => x.KitchenDescription).ToList();
                        break;
                    case "EmployeeAsc":
                        data = data.OrderBy(x => x.Employee.Name).ToList();
                        break;
                    case "EmployeeDesc":
                        data = data.OrderByDescending(x => x.Employee.Name).ToList();
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
        private List<Kitchen> GetSearchingData(string searchby, string search, List<Kitchen> data)
        {
            switch (searchby)
            {
                case "KitchenName":
                    data = data.Where(x => x.KitchenName.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "KitchenDescription":
                    data = data.Where(x => x.KitchenDescription.ToLower().Contains(search.ToLower())).ToList();
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
        #endregion
        public ActionResult KOT(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            //Sorting ViewBag
            ViewBag.KitchenNameSortby = sortby == "KitchenNameAsc" ? "KitchenNameDesc" : "KitchenNameAsc";
            ViewBag.KitchenDescriptionSortby = sortby == "KitchenDescriptionAsc" ? "KitchenDescriptionDesc" : "KitchenDescriptionAsc";
            ViewBag.EmployeeSortby = sortby == "EmployeeAsc" ? "EmployeeDesc" : "EmployeeAsc";

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
            var kot = (from oi in kitchenServices.OrderItems.GetByAll().Where(x => x.Active == true).ToList()
                       join f in kitchenServices.FoodItems.GetByAll().Where(x => x.Active == true).ToList() on oi.FoodItemID equals f.FoodItemID
                       join k in kitchenServices.Kitchen.GetByAll().Where(x => x.Active == true).ToList() on f.KitchenID equals k.KitchenID
                       join om in kitchenServices.OrderMaster.GetByAll().Where(x => x.Active == true && (x.OrderStatus == "ConfirmOrder" && x.OrderStatus != "ReadyPickup") && x.IsBillPaid == false).ToList() on oi.OrderMasterID equals om.OrderMasterID
                       orderby k.KitchenName
                       select new KOTViewModel
                       {
                           fooditems = f,
                           Kitchen = k,
                           OrderItems = oi
                       }).ToList();
            //Searching
            if (!string.IsNullOrEmpty(searchby1) && !string.IsNullOrEmpty(search1))
            {
                kot = GetSearchingData2(searchby1, search1, kot);
            }
            if (!string.IsNullOrEmpty(searchby2) && !string.IsNullOrEmpty(search2))
            {
                kot = GetSearchingData2(searchby2, search2, kot);
            }
            if (!string.IsNullOrEmpty(searchby3) && !string.IsNullOrEmpty(search3))
            {
                kot = GetSearchingData2(searchby3, search3, kot);
            }
            if (!string.IsNullOrEmpty(searchby4) && !string.IsNullOrEmpty(search4))
            {
                kot = GetSearchingData2(searchby4, search4, kot);
            }
            //Sorting
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "KitchenNameAsc":
                        kot = kot.OrderBy(x => x.Kitchen.KitchenName).ToList();
                        break;
                    case "KitchenNameDesc":
                        kot = kot.OrderByDescending(x => x.Kitchen.KitchenName).ToList();
                        break;
                    case "KitchenDescriptionAsc":
                        kot = kot.OrderBy(x => x.Kitchen.KitchenDescription).ToList();
                        break;
                    case "KitchenDescriptionDesc":
                        kot = kot.OrderByDescending(x => x.Kitchen.KitchenDescription).ToList();
                        break;
                    case "EmployeeAsc":
                        kot = kot.OrderBy(x => x.fooditems.Description).ToList();
                        break;
                    case "EmployeeDesc":
                        kot = kot.OrderByDescending(x => x.fooditems.Description).ToList();
                        break;
                    case "CreatedUserNameAsc":
                        kot = kot.OrderBy(x => x.fooditems.IsTodaySpecial).ToList();
                        break;
                    case "CreatedUserNameDesc":
                        kot = kot.OrderByDescending(x => x.fooditems.IsTodaySpecial).ToList();
                        break;
                }
            }
            return View(kot.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }

        private List<KOTViewModel> GetSearchingData2(string searchby, string search, List<KOTViewModel> kot)
        {
            switch (searchby)
            {
                case "KitchenName":
                    kot = kot.Where(x => x.Kitchen.KitchenName.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "KitchenDescription":
                    kot = kot.Where(x => x.Kitchen.KitchenDescription.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "Employee":
                    kot = kot.Where(x => x.fooditems.Description.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "CreatedUserName":
                    kot = kot.Where(x => x.fooditems.IsTodaySpecial == Convert.ToBoolean(search)).ToList();
                    break;

                case "CreatedDate":
                    DateTime createdDate;
                    if (DateTime.TryParse(search, out createdDate))
                    {
                        kot = kot.Where(x => x.OrderItems.CreatedDate == createdDate).ToList();
                    }
                    break;
            }
            return kot;
        }

        #region create
        // GET: Kitchen/Create
        public ActionResult Create()
        {
            BindEmployee();
            return View();
        }
        public void BindEmployee()
        {
            List<Employee> employeelist = kitchenServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            employeelist.Insert(0, new Employee { EmployeeID = "1", Name = "-Select One-" });
            ViewBag.Employee = new SelectList(employeelist, "EmployeeID", "Name");
        }
        // POST: Kitchen/Create
        [HttpPost]
        public ActionResult Create(KitchenViewModel viewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkdata = kitchenServices.Kitchen.GetByAll().Any(x => x.KitchenName == viewmodel.KitchenName && x.Active == true);
                    if (!checkdata)
                    {
                        Kitchen model = new Kitchen();
                        model.KitchenID = Guid.NewGuid().ToString();
                        model.KitchenName = viewmodel.KitchenName;
                        model.KitchenDescription = viewmodel.KitchenDescription;
                        model.EmployeeID = viewmodel.EmployeeID;
                        model.Active = true;
                        model.CreatedUserID = CurrentApplicationUser.Id;
                        model.CreatedDate = DateTime.Now;
                        kitchenServices.Kitchen.Add(model);
                        kitchenServices.Save();
                        return RedirectToAction("Index", new { status = "Save Successful!" });
                    }
                    else
                    {
                        return RedirectToAction("Index", new { status = "Data Already Existing!" });
                    }
                }
                BindEmployee();
                return View();
            }
            catch (Exception e)
            {
                BindEmployee();
                return View();
            }
        }
        #endregion

        #region edit
        // GET: Kitchen/Edit/5
        public ActionResult Edit(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                Kitchen model = kitchenServices.Kitchen.GetByID(id);
                KitchenViewModel viewmodel = new KitchenViewModel()
                {
                    KitchenID = model.KitchenID,
                    KitchenName = model.KitchenName,
                    KitchenDescription = model.KitchenDescription,
                };
                ViewBag.Employee = new SelectList(kitchenServices.Employee.GetByAll().Where(x => x.Active == true).ToList(), "EmployeeID", "Name", model.EmployeeID);
                return View(viewmodel);
            }
            return View();
        }
        // POST: Kitchen/Edit/5
        [HttpPost]
        public ActionResult Edit(KitchenViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                Kitchen model = kitchenServices.Kitchen.GetByID(viewmodel.KitchenID);
                model.KitchenName = viewmodel.KitchenName;
                model.KitchenDescription = viewmodel.KitchenDescription;
                model.EmployeeID = viewmodel.EmployeeID;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedUserID = CurrentApplicationUser.Id;
                kitchenServices.Kitchen.Update(model);
                kitchenServices.Save();
                return RedirectToAction("Index", new { status = "Update Successful!" });
            }
            BindEmployee();
            return View();
        }
        #endregion

        #region delete
        // GET: Kitchen/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Kitchen model = kitchenServices.Kitchen.GetByID(id);
                KitchenViewModel viewmodel = new KitchenViewModel()
                {
                    KitchenID = model.KitchenID,
                    KitchenName = model.KitchenName,
                    KitchenDescription = model.KitchenDescription,
                };
                viewmodel.Employee = kitchenServices.Employee.GetByID(model.EmployeeID);
                return View(viewmodel);
            }
            return View();
        }
        // POST: Kitchen/Delete/5
        [HttpPost]
        public ActionResult Delete(KitchenViewModel viewmodel)
        {
            try
            {
                if (!string.IsNullOrEmpty(viewmodel.KitchenID))
                {
                    Kitchen model = kitchenServices.Kitchen.GetByID(viewmodel.KitchenID);
                    model.Active = false;
                    kitchenServices.Kitchen.Update(model);
                    kitchenServices.Save();
                    return RedirectToAction("Index", new { status = "Delete Successful!" });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        #endregion

        //[HttpGet]
        //public JsonResult GetAllKitchen()
        //{
        //    ApplicationUser user = IdentityUserManager.Users.Where(x => x.Id == userId && x.Active == true).SingleOrDefault();
        //    List<ApplicationRole> roles = IdentityRoleManager.Roles.ToList();
        //    UserAssignRoleViewModel model = new UserAssignRoleViewModel()
        //    {
        //        ApplicationUser = user,
        //        ApplicationRoles = roles
        //    };
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult SetReadyPickupMsg(FormCollection fc)
        {
            string OrderMasterID = fc["OrderMasterID"]; // Basic Helper
            string OrderItemsID = fc["OrderItemsID"]; // Basic Helper
            string FoodItemID = fc["FoodItemID"]; // Basic Helper
            if (IsValidRecord(OrderMasterID, OrderItemsID, FoodItemID))
            {
                KOTPickUp kotmodel = new KOTPickUp();
                kotmodel.KOTPickUpID = Guid.NewGuid().ToString();
                kotmodel.OrderMasterID = OrderMasterID;
                kotmodel.OrderItemsID = OrderItemsID;
                kotmodel.IsReadyPickup = true;
                OrderMaster ordermodel = orderMasterServices.OrderMaster.GetByID(OrderMasterID);
                ordermodel.OrderStatus = "ReadyPickup";
                if (kOTPickupServices.KOTPickUp.Add(kotmodel))
                {
                    orderMasterServices.OrderMaster.Update(ordermodel);
                    kOTPickupServices.Save();
                    orderMasterServices.Save();
                    Success(string.Format("<b>{0}</b> is ready to pick up.", ordermodel.Table.TableNo), true);
                }
            }
            return RedirectToAction("KOT", "Kitchen");
        }

        private bool IsValidRecord(string orderMasterID, string orderItemsID, string foodItemID)
        {
            if (string.IsNullOrEmpty(orderItemsID) || string.IsNullOrEmpty(orderItemsID) || string.IsNullOrEmpty(foodItemID)) { return false; }
            return true;

        }
    }
}