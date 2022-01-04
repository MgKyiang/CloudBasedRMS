
namespace CloudBasedRMS.View.Controllers
{
    using Core;
    using PagedList;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using ViewModel;

    public class OrderController : ControllerAuthorizeBase
    {
        #region instance of all services
        public OrderMasterServices orderMasterServices;
        public OrderItemsServices orderItemsServices;
        public FoodItems_DetailsServices foodItemsDetailServices;
        public TableServices tableServices;
        #endregion

        #region default constructor
        public OrderController()
        {
            orderMasterServices = new OrderMasterServices();
            orderItemsServices = new OrderItemsServices();
            foodItemsDetailServices = new FoodItems_DetailsServices();
            tableServices = new TableServices();
        }
        #endregion

        #region Index
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            var data = orderMasterServices.OrderMaster.GetByAll().Where(x => x.Active == true && x.OrderStatus == null).ToList();
            //Sorting ViewBag
            ViewBag.OrderNoSortby = sortby == "OrderNoAsc" ? "OrderNoDesc" : "OrderNoAsc";
            ViewBag.OrderDateSortby = sortby == "OrderDateAsc" ? "OrderDateDesc" : "OrderDateAsc";
            ViewBag.DescriptionSortby = sortby == "DescriptionAsc" ? "DescriptionDesc" : "DescriptionAsc";
            ViewBag.IsParcelSortBy = sortby == "IsParcelAsc" ? "IsParcelDesc" : "IsParcelAsc";
            ViewBag.TableNoSortby = sortby == "TableNoAsc" ? "TableNoDesc" : "TableNoAsc";

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
                    case "OrderDateAsc":
                        data = data.OrderBy(x => x.OrderDate).ToList();
                        break;
                    case "OrderDateDesc":
                        data = data.OrderByDescending(x => x.OrderDate).ToList();
                        break;
                    case "OrderNoAsc":
                        data = data.OrderBy(x => x.OrderNo).ToList();
                        break;
                    case "OrderNoDesc":
                        data = data.OrderByDescending(x => x.OrderNo).ToList();
                        break;
                    case "DescriptionAsc":
                        data = data.OrderBy(x => x.Description).ToList();
                        break;
                    case "DescriptionDesc":
                        data = data.OrderByDescending(x => x.Description).ToList();
                        break;

                    case "IsParcelAsc":
                        data = data.OrderBy(x => x.IsParcel).ToList();
                        break;
                    case "IsParcelDesc":
                        data = data.OrderByDescending(x => x.IsParcel).ToList();
                        break;
                    case "TableNoAsc":
                        data = data.OrderBy(x => x.Table.TableNo).ToList();
                        break;
                    case "TableNoDesc":
                        data = data.OrderByDescending(x => x.Table.TableNo).ToList();
                        break;
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }

        //Searching Helper
        private List<OrderMaster> GetSearchingData(string searchby, string search, List<OrderMaster> data)
        {
            switch (searchby)
            {
                case "OrderNo":
                    data = data.Where(x => x.OrderNo.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "OrderStatus":
                    data = data.Where(x => x.OrderStatus.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "IsParcel":
                    data = data.Where(x => x.IsParcel == Convert.ToBoolean(search)).ToList();
                    break;
                case "Capacity":
                    DateTime OrderDate;
                    if (DateTime.TryParse(search, out OrderDate))
                    {
                        data = data.Where(x => x.OrderDate == OrderDate).ToList();
                    }
                    break;
                case "Table":
                    data = data.Where(x => x.Table.TableNo.ToLower().Contains(search.ToLower())).ToList();
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

        private List<OrderListViewModel> GetSearchingData(string searchby, string search, List<OrderListViewModel> data)
        {
            switch (searchby)
            {
                case "OrderNo":
                    data = data.Where(x => x.order.OrderNo.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "Description":
                    data = data.Where(x => x.order.Description.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "IsParcel":
                    data = data.Where(x => x.order.IsParcel == Convert.ToBoolean(search)).ToList();
                    break;
                case "OrderDate":
                    DateTime OrderDate;
                    if (DateTime.TryParse(search, out OrderDate))
                    {
                        data = data.Where(x => x.order.OrderDate == OrderDate).ToList();
                    }
                    break;
                case "TableNo":
                    data = data.Where(x => x.order.Table.TableNo.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "CreatedUserName":
                    data = data.Where(x => x.order.CreatedUser.UserName.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "CreatedDate":
                    DateTime createdDate;
                    if (DateTime.TryParse(search, out createdDate))
                    {
                        data = data.Where(x => x.order.CreatedDate == createdDate).ToList();
                    }
                    break;
            }
            return data;
        }
        #endregion

        #region cascading drop down list event[getFoodItems_Details]
        [HttpGet]
        public JsonResult getFoodItems_Details(string CategoryID = "")
        {
            List<FoodItems_Details> foodItems = orderItemsServices.FoodItems_Details.GetByCategoryID(CategoryID).OrderBy(x => x.Code).ToList();
            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = foodItems,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Not valid request",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        #endregion

        #region getFoodItemRecord
        [HttpGet]
        public JsonResult getFoodItemRecord(string FoodItemID)
        {
            FoodItems_Details foodItems = foodItemsDetailServices.FoodItems_Details.GetByID(FoodItemID);
            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = foodItems,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Not valid request",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Status = "Create";
            OrderMasterViewModel orderviewmodel = new OrderMasterViewModel();
            ddlDataBind();
            //auto generate order no
            string generateCode = "O" + DateTime.Now.ToString("HH:mm:ss:fffffff").Replace(":", "");// + DateTime.Now.ToString("dd/MM/yy") + "_" 
            orderviewmodel.OrderNo = generateCode;
            return View(orderviewmodel);
        }
        [HttpPost]
        public ActionResult Create(OrderMasterViewModel ordermasterviewmodel, string OrderItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var checkdata = orderMasterServices.OrderMaster.GetByOrderNo(ordermasterviewmodel.OrderNo);
                    if (checkdata.Count > 0 || OrderItem == "[]" || OrderItem == null)
                    {
                        //Warning("Duplicate Order No or Required Items data please check your records.", false);
                        //ddlDataBind();
                        return RedirectToAction("Index");
                    }
                    //else if (OrderItem == "[]" || OrderItem == null)
                    // {
                    //     Warning("Required Item data.", false);
                    //     ddlDataBind();
                    //     return View(ordermasterviewmodel);                   
                    // }
                    else
                    {
                        //bind order master data from view
                        OrderMaster ordermastermodel = new OrderMaster
                        {
                            OrderMasterID = Guid.NewGuid().ToString(),
                            OrderNo = ordermasterviewmodel.OrderNo,
                            OrderDate = ordermasterviewmodel.OrderDate,
                            IsParcel = ordermasterviewmodel.IsParcel,
                            Description = ordermasterviewmodel.Description,
                            TableID = ordermasterviewmodel.TableID,
                            CreatedDate = DateTime.Now,
                            CreatedUserID = CurrentApplicationUser.Id,
                            Active = true
                        };
                        //Deserialization json fromat into list view
                        List<OrderItems> orderitems = new JavaScriptSerializer().Deserialize<List<OrderItems>>(OrderItem);
                        foreach (OrderItems item in orderitems)
                        {
                            item.OrderItemsID = Guid.NewGuid().ToString();
                            item.OrderMasterID = ordermastermodel.OrderMasterID;
                            item.CreatedUserID = CurrentApplicationUser.Id;
                            item.CreatedDate = DateTime.Now;
                            item.Active = true;
                        }
                        if (orderMasterServices.Insert(ordermastermodel, orderitems))
                        {
                            Tables tablemodel = tableServices.Table.GetByID(ordermasterviewmodel.TableID);
                            tablemodel.IsAvailable = false;
                            tablemodel.Status = "This table is using by" + ordermasterviewmodel.OrderNo;
                            tableServices.Table.Update(tablemodel);
                            tableServices.Save();
                        }
                        Success(string.Format("<b>{0}</b> was successfully saved to the system.", ordermastermodel.OrderNo), true);
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    return Json(new { Errors = ex.Message });
                }

            }//end of Model.IsValid()
            else
            {
                string errorMessage = string.Empty;
                foreach (var key in ModelState.Keys)
                {
                    var error = ModelState[key].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        errorMessage += "<li class=\"field-validation-error\">"
                         + error.ErrorMessage + "</li>";
                    }
                }
                errorMessage += "</ul>";
                Danger(string.Format("<b>{0}</b> error occur in your saving record.", errorMessage), true);
                this.ddlDataBind();
                return RedirectToAction("Create");
            }
        }
        #endregion

        #region Bind Method
        private void ddlDataBind()
        {
            //bind tables data to the dropdown list
            List<Tables> tables = orderMasterServices.Table.GetByAll().Where(x => x.Active == true).ToList();
            tables.Insert(0, new Tables { TableID = "1", TableNo = "-Select One-" });
            ViewBag.tables = new SelectList(tables, "TableID", "TableNo");
            //bind category data to the dropdown list
            List<Category> categories = orderItemsServices.Category.GetByAll().OrderBy(c => c.Code).ToList();
            categories.Insert(0, new Category { CategoryID = "1", Description = "-Select One-" });
            ViewBag.categories = new SelectList(categories, "CategoryID", "Description");
            //bind food items data to the dropdown list
            List<FoodItems_Details> foodItems = new List<FoodItems_Details>();
            foodItems.Insert(0, new FoodItems_Details { FoodItemID = "1", Code = "-Select One-" });
            ViewBag.foodItems = new SelectList(foodItems, "FoodItemID", "Description");
        }
        private void ddlDataBindForEdit()
        {
            //bind category data to the dropdown list
            List<Category> categories = orderItemsServices.Category.GetByAll().OrderBy(c => c.Code).ToList();
            categories.Insert(0, new Category { CategoryID = "1", Description = "-Select One-" });
            ViewBag.categories = new SelectList(categories, "CategoryID", "Description");
            //bind food items data to the dropdown list
            List<FoodItems_Details> foodItems = new List<FoodItems_Details>();
            foodItems.Insert(0, new FoodItems_Details { FoodItemID = "1", Code = "-Select One-" });
            ViewBag.foodItems = new SelectList(foodItems, "FoodItemID", "Description");
        }
        #endregion

        #region Edit Action
        public ActionResult Edit(string Id)
        {
            ViewBag.Status = "Edit";
            OrderMasterViewModel ordermasterviewmodel = new OrderMasterViewModel();
            List<OrderItemsViewModel> OrderItemvmList = new List<OrderItemsViewModel>();
            //get selected order mater record by order master id
            OrderMaster ordermastermodel = orderMasterServices.OrderMaster.GetByID(Id);
            //binding order master data to the viewmodel
            ordermasterviewmodel.OrderMasterID = ordermastermodel.OrderMasterID;
            ordermasterviewmodel.OrderNo = ordermastermodel.OrderNo;
            ordermasterviewmodel.IsParcel = ordermastermodel.IsParcel;
            ordermasterviewmodel.OrderDate = ordermastermodel.OrderDate;
            ordermasterviewmodel.Description = ordermastermodel.Description;
            List<Tables> tables = orderMasterServices.Table.GetByAll().Where(t => t.TableID == ordermastermodel.TableID).ToList();
            ViewBag.tables = new SelectList(tables, "TableID", "TableNo");
            ordermasterviewmodel.OrderItems = ordermastermodel.OrderItems.Where(x => x.Active == true && x.OrderMasterID == Id).ToList().Select((x, index)
                     => new OrderItemsViewModel
                     {
                         Index = index,
                         OrderItemsID = x.OrderItemsID,
                         FoodItemID = x.FoodItemID,
                         Quantity = x.Quantity,
                         RatePerItems = x.RatePerItems,
                         Amount = x.Amount,
                         FoodItems = x.FoodItems_Details == null ? string.Empty : x.FoodItems_Details.Code
                     }).ToList();
            ddlDataBindForEdit();
            return View("Create", ordermasterviewmodel);
        }
        [HttpPost]
        public ActionResult Edit(OrderMasterViewModel ordermasterviewmodel, string OrderItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var checkdata = orderMasterServices.OrderMaster.GetByOrderNo(ordermasterviewmodel.OrderNo).Where(x => x.OrderMasterID != ordermasterviewmodel.OrderMasterID).ToList();
                    if (checkdata.Count > 0)
                    {

                        Warning("Duplicate Order No please check your records.", false);
                        this.ddlDataBind();
                        return View("Create", ordermasterviewmodel);
                    }
                    if (OrderItem == "[]")
                    {
                        Warning("Required Items data please check your records.", false);
                        this.ddlDataBind();
                        return View("Create", ordermasterviewmodel);
                    }
                    OrderMaster ordermastermodel = new OrderMaster
                    {
                        OrderMasterID = ordermasterviewmodel.OrderMasterID,
                        OrderNo = ordermasterviewmodel.OrderNo,
                        OrderDate = ordermasterviewmodel.OrderDate,
                        TableID = ordermasterviewmodel.TableID,
                        IsParcel = ordermasterviewmodel.IsParcel,
                        Description = ordermasterviewmodel.Description,
                        UpdatedDate = DateTime.Now,
                        UpdatedUserID = CurrentApplicationUser.Id,
                        Active = true
                    };
                    //Deserialize json array to object model
                    List<OrderItems> orderitemsmodel = new JavaScriptSerializer().Deserialize<List<OrderItems>>(OrderItem);
                    foreach (OrderItems items in orderitemsmodel)
                    {
                        items.OrderItemsID = Guid.NewGuid().ToString();
                        items.OrderMasterID = ordermastermodel.OrderMasterID;
                        items.CreatedUserID = CurrentApplicationUser.Id;
                        items.CreatedDate = DateTime.Now;
                        items.Active = true;
                    }
                    orderMasterServices.OrderItems.AddRange(orderitemsmodel);
                    orderMasterServices.OrderMaster.Update(ordermastermodel);
                    orderMasterServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully updated to the system.", ordermasterviewmodel.OrderNo), true);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Danger("Error occur when updating process>it is " + ex.Message);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string errorMessage = string.Empty;
                foreach (var key in ModelState.Keys)
                {
                    var error = ModelState[key].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        errorMessage += "<li class=\"field-validation-error\">"
                         + error.ErrorMessage + "</li>";
                    }
                }
                errorMessage += "</ul>";
                Danger("Error occur when updating process> it is " + errorMessage);
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete Action
        public ActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                OrderMaster ordermastermodel = orderMasterServices.OrderMaster.GetByID(id);
                ordermastermodel.Active = false;
                orderMasterServices.Save();
                return Json(new { result = "Delete Successful." });
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.Message });
            }
        }
        #endregion

        #region Order Master-Detail view List
        public ActionResult List(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            List<OrderListViewModel> data = new List<OrderListViewModel>();
            var ordermaster = orderMasterServices.OrderMaster.GetByAll().Where(x => x.Active == true).OrderByDescending(a => a.CreatedDate);
            foreach (var i in ordermaster)
            {
                var orderitems = orderItemsServices.OrderItems.GetByAll().Where(a => a.OrderMasterID.Equals(i.OrderMasterID)).ToList();
                data.Add(new OrderListViewModel { order = i, orderDetails = orderitems });
            }
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
                    case "OrderDateAsc":
                        data = data.OrderBy(x => x.order.OrderDate).ToList();
                        break;
                    case "OrderDateDesc":
                        data = data.OrderByDescending(x => x.order.OrderDate).ToList();
                        break;
                    case "OrderNoAsc":
                        data = data.OrderBy(x => x.order.OrderNo).ToList();
                        break;
                    case "OrderNoDesc":
                        data = data.OrderByDescending(x => x.order.OrderNo).ToList();
                        break;
                    case "OrderStatusAsc":
                        data = data.OrderBy(x => x.order.OrderStatus).ToList();
                        break;
                    case "OrderStatusDesc":
                        data = data.OrderByDescending(x => x.order.OrderStatus).ToList();
                        break;

                    case "IsParcelAsc":
                        data = data.OrderBy(x => x.order.IsParcel).ToList();
                        break;
                    case "IsParcelDesc":
                        data = data.OrderByDescending(x => x.order.IsParcel).ToList();
                        break;
                    case "TableAsc":
                        data = data.OrderBy(x => x.order.Table.TableNo).ToList();
                        break;
                    case "TableDesc":
                        data = data.OrderByDescending(x => x.order.Table.TableNo).ToList();
                        break;
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }
        #endregion
    }//end of Order Controller
}//end of Namespace
