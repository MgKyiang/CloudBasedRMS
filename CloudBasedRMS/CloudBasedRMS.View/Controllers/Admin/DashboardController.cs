using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.View.Controllers.Helper;
using CloudBasedRMS.View.Controllers.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using TweetSharp;

namespace CloudBasedRMS.View.Controllers
{
    public class DashboardController : ControllerAuthorizeBase
    {
        #region Create instance/Services
        public OrderMasterServices orderMasterServices;
        public EmployeeServices employeeServices;
        public BillFoodItemsServices billFoodItemsServices;
        public CustomerServices customerServices;
        public SaleBillServices saleBillServices;
        public TableServices tableServices;
        private KOTPickupServices kOTPickupServices;
        CBRMSHelper cBRMSHelper;
        #endregion

        #region Default Constructor
        public DashboardController()
        {
            orderMasterServices = new OrderMasterServices();
            employeeServices = new EmployeeServices();
            billFoodItemsServices = new BillFoodItemsServices();
            customerServices = new CustomerServices();
            saleBillServices = new SaleBillServices();
            tableServices = new TableServices();
            cBRMSHelper = new CBRMSHelper();
            kOTPickupServices = new KOTPickupServices();
        }
        #endregion

        #region getDashboardRecord
        public DashboardViewModel getDashboardRecord()
        {
            DashboardViewModel dashboardviewmodel = new DashboardViewModel();
            //show for popular food item[Table]
            dashboardviewmodel.BillFoods = billFoodItemsServices.BillFoodItems.GetByAll().Where(y => y.Active == true).DistinctBy(x => x.FoodItemID).ToList();
            //====================*******************************************==============================================
            //show for total order count
            ViewBag.orderCount = orderMasterServices.OrderMaster.GetByAll().Where(x => x.Active == true).Count();
            //====================*******************************************==============================================
            //show for order confirm count
            ViewBag.orderConfirm = orderMasterServices.OrderMaster.GetByAll().Where(x => x.OrderStatus == "ConfirmOrder").Count();
            //====================*******************************************==============================================
            //show for total Customer count
            ViewBag.CustomerCount = customerServices.Customer.GetByAll().Where(x => x.Active == true).Count();
            //====================*******************************************==============================================
            //show for total employee count
            ViewBag.EmployeeCount = employeeServices.Employee.GetByAll().Where(x => x.Active == true).Count();
            //====================*******************************************==============================================
            //show for sall bill[sale stages] record by year and net amount[Bar Chart]
            this.SaleBillByMonth();
            #region old code for sale bill by month
            //TwoDimensionalData data = new TwoDimensionalData();
            //var salebillitems= saleBillServices.SaleBill.GetByAll().Where(x => x.Active == true).ToList();
            //var salebills = salebillitems.Select(s => s.OrderMaster.OrderDate.Value.Month).Distinct();
            //foreach (var disitems in salebills)
            //{
            //    decimal netamt = 0;
            //    int month = 0;
            //    foreach (var allitems in salebillitems)
            //    {
            //        if (allitems.OrderMaster.OrderDate.Value.Month == disitems)
            //        {
            //            month = allitems.SaleBillDate.Month;
            //            netamt += allitems.NetAmount;

            //        }
            //    }
            //    data.Data.Add(new int[] { month, (int)netamt});
            //}

            //dashboardviewmodel.TwoDimensionalData = data;
            #endregion
            //====================*******************************************==============================================
            //show for Employee data by rank[Pie Chart]
            #region old code Employee data by rank[Pie Chart]
            //ChartModel model = new ChartModel();
            //model.columns.Add("string", "Rank");
            //model.columns.Add("number", "TotalStaff");

            //DataTable dt = new DataTable();
            //dt.Clear();
            //dt.Columns.Add("Rank");
            //dt.Columns.Add("TotalStaff");
            //dt.Columns["TotalStaff"].DataType = System.Type.GetType("System.Int32");
            //var rankrecordbyemployee = EmployeeServices.Employee.GetByAll().Where(x => x.Active == true).DistinctBy(R => R.RankID).ToList();
            //if (rankrecordbyemployee.Count() > 0)
            //{
            //    foreach (var e in rankrecordbyemployee)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr["Rank"] = e.Rank.Description;
            //        dr["TotalStaff"] =e.Rank.Code.Count();
            //        dt.Rows.Add(dr);
            //    }
            //}
            //else
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["Rank"] = "No Rank ";
            //    dr["TotalStaff"] = 0;
            //    dt.Rows.Add(dr);
            //}

            //model.rows = dt;
            //model.Options.Add("Width", "100%");
            //model.Options.Add("Height", "100%");
            //model.Options.Add("Title", "My pie chart");
            //dashboardviewmodel.ChartModel = model;
            #endregion
            var rankalldata = employeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            ViewBag.EmployeeRecordPoints = JsonConvert.SerializeObject(DataService.GetEmployeeStrength(rankalldata), jsonSetting);
            //====================*******************************************==============================================

            //====================*******************************************==============================================
            dashboardviewmodel.OrderMaster = orderMasterServices.OrderMaster.GetByAll().Where(x => x.Active && x.OrderStatus == null).ToList();
            //====================*******************************************==============================================
            //get Popular food name and its total price
            var pdata = billFoodItemsServices.BillFoodItems.GetByAll().Where(y => y.Active == true).DistinctBy(x => x.FoodItemID).ToList();
            //Below code can be used to include dynamic data in Chart. Check view page and uncomment the line "dataPoints: @Html.Raw(ViewBag.DataPoints)"
            ViewBag.BillFoodItemsDataPoints = JsonConvert.SerializeObject(DataService.GetPopularFoodNameTotalPrice(pdata), jsonSetting);
            //====================*******************************************==============================================
            //get available table list
            dashboardviewmodel.tablelist = tableServices.Table.GetByAll().Where(x => x.IsAvailable == true).Take(ApplicationSettingPagingSize).ToList();
            ViewBag.TotalOrderedtableCount = orderMasterServices.Table.GetByAll().Where(x => x.Active == true && x.IsAvailable == false).Count();

            //get kot pick up data record
            dashboardviewmodel.KOTPickUp = kOTPickupServices.KOTPickUp.GetByAll().Where(x => x.IsReadyPickup = true).ToList();
            //finally return all to view
            return dashboardviewmodel;
        }
        //method for bar chart sale bill by month name
        public void SaleBillByMonth()
        {
            List<string> salebilldate = new List<string>();
            List<decimal> netamtrepo = new List<decimal>();
            var data = saleBillServices.SaleBill.GetByAll().Where(x => x.Active == true).ToList();
            var salebills = data.Select(s => s.OrderMaster.OrderDate.Value.Month).Distinct();
            foreach (var disitems in salebills)
            {
                decimal netamt = 0;
                foreach (var allitems in data)
                {
                    if (allitems.OrderMaster.OrderDate.Value.Month == disitems)
                    {
                        netamt += allitems.NetAmount;
                    }
                }
                netamtrepo.Add(netamt);
            }
            ViewBag.orderDate = cBRMSHelper.ConvertToMonth(salebills);
            ViewBag.saledata = netamtrepo;
        }
        #endregion



        #region Index
        public ActionResult Index()
        {
            //System.Threading.Thread.Sleep(100);
            return View(getDashboardRecord());
        }
        #endregion

        #region Confirm/Cancel/Delete order
        public ActionResult ConfirmOrder(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            OrderMaster model = orderMasterServices.OrderMaster.GetByID(id);
            model.OrderStatus = "ConfirmOrder";
            orderMasterServices.OrderMaster.Update(model);
            orderMasterServices.Save();
            Success(string.Format("<b>{0}</b> was successfully Confirm Order to the system.", model.Description), true);
            return RedirectToAction("Index", "Dashboard");
        }
        public ActionResult CancelOrder(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            OrderMaster model = orderMasterServices.OrderMaster.GetByID(id);
            model.OrderStatus = "CancelOrder";
            orderMasterServices.OrderMaster.Update(model);
            orderMasterServices.Save();
            Success(string.Format("<b>{0}</b> was successfully cancel Order to the system.", model.Description), true);
            return RedirectToAction("Index", "Dashboard");
        }
        public ActionResult DeleteOrder(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            OrderMaster model = orderMasterServices.OrderMaster.GetByID(id);
            model.OrderStatus = "DeleteOrder";
            model.Active = false;
            orderMasterServices.OrderMaster.Update(model);
            orderMasterServices.Save();
            Success(string.Format("<b>{0}</b> was successfully deleted order from the system.", model.Description), true);
            return RedirectToAction("Index", "Dashboard");
        }
        #endregion

        public JsonResult Chartd()
        {
            var data = employeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalOrder()
        {
            var data = orderMasterServices.OrderItems.GetByAll().Where(x => x.Active == true).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBillFoodItems()
        {
            var data = billFoodItemsServices.BillFoodItems.GetByAll().Where(y => y.Active == true).DistinctBy(x => x.FoodItemID).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region TwitterCallBackLogin
        public ActionResult TwitterCallback(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            string Key = "gBdUrfCDekapXGJIn9ExzblUp";
            string Secret = "RypLIlXhfHjUHQVa5STQsMqrEonm2L4J2JMrNZ14HwfTdxaBrs";

            try
            {
                TwitterService service = new TwitterService(Key, Secret);

                OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

                service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);

                VerifyCredentialsOptions option = new VerifyCredentialsOptions();

                TwitterUser user = service.VerifyCredentials(option);
                TempData["Name"] = user.Name;
                TempData["Userpic"] = user.ProfileImageUrl;

                return View();
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region Helper
        JsonSerializerSettings jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        #endregion

    }
}