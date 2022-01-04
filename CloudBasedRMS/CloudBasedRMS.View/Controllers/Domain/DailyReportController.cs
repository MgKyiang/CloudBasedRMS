using CloudBasedRMS.Services;
using CloudBasedRMS.View.Controllers.Helper;
using CloudBasedRMS.View.Controllers.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers.Domain
{
    public class DailyReportController : ControllerAuthorizeBase
    {
        #region Create instance of object
        public SaleBillServices saleBillServices;
        public EmployeeServices employeeServices;
        public BillFoodItemsServices billFoodItemsServices;
        public OrderMasterServices orderMasterServices;
        public CustomerServices customerServices;
        CBRMSHelper cBRMSHelper;
        ICharts icharts;
        #endregion

        #region Default Constructor
        public DailyReportController()
        {
            saleBillServices = new SaleBillServices();
            employeeServices = new EmployeeServices();
            icharts = new ChartsConcrete();
            billFoodItemsServices = new BillFoodItemsServices();
            orderMasterServices = new OrderMasterServices();
            cBRMSHelper = new CBRMSHelper();
            customerServices = new CustomerServices();
        }
        #endregion

        #region Index
        // GET: DailyReport
        public ActionResult Index()
        {        
            return View();
        }
        #endregion

        #region SaleBillBySaleDateReport
        /// <summary>
        /// This is used for showing Generic Report(with data and report parameter) in a same window
        /// </summary>
        /// <param name="txtFromDate">This will be passed to Report for showing from Date</param>
        /// <param name="txtToDate">This will be passed to Report for showing to Date</param>
        /// <returns></returns>
        public ActionResult SaleBillBySaleDateReport(SaleBillReportByFromToDateViewModel viewmodel)
        {
            if (ModelState.IsValid||viewmodel.SaleBillFromDate!=DateTime.MinValue||viewmodel.SaleBillToDate!=DateTime.MinValue)
            {
                var data = saleBillServices.SaleBill.GetByAll().Where(x => x.Active == true && (x.SaleBillDate >= viewmodel.SaleBillFromDate && x.SaleBillDate <= viewmodel.SaleBillToDate)).ToList();
                if (data.Count==0) { return RedirectToAction("Index", new { status = "There is No record to show!" }); }
                this.HttpContext.Session["ReportName"] = "SaleBillBySaleDate.rpt";
                this.HttpContext.Session["rptFromDate"] = viewmodel.SaleBillFromDate;
                this.HttpContext.Session["rptToDate"] = viewmodel.SaleBillToDate;
                this.HttpContext.Session["rptSource"] =data.Select(p => new { SaleBillNo = p.SaleBillNo, SaleBillDate = p.SaleBillDate, CasherName = p.Employee.Name, OrderNo = p.OrderMaster.OrderNo, CustomerName = p.Customer.Name, Amount = p.Amount, Tax = p.Tax, Discount = p.Discount, NetAmount = p.NetAmount }).ToList();
                return RedirectToAction("ShowSaleBillBySaleDate", "GenericReportViewer");
            }
            return View("Index");
        }
        #endregion

        #region EmployeeReport
        public ActionResult EmployeeReport()
        {
            ViewBag.Rank = new SelectList(employeeServices.Rank.GetByAll().Where(x => x.Active == true).ToList(), "RankID", "Description");
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeReport(EmployeeReportViewModel viewmodel)
        {           
            if (ModelState.IsValid)
            {
                var data = employeeServices.Employee.GetByAll().Where(x => x.Active == true && x.RankID == viewmodel.RankID).ToList();
                if (data.Count == 0) { return RedirectToAction("EmployeeReport", new { status = "There is No record to show!" }); }
                HttpContext.Session["ReportName"] = "Employee.rpt";
                HttpContext.Session["rptRankID"] = viewmodel.RankID;
                HttpContext.Session["rptSource"] = data.Select(p => new {EmployeeNo=p.EmployeeNo,EmployeeName=p.Name,DOB=p.DOB,Email=p.NRC,PhoneNo=p.PhoneNo,Rank=p.Rank.Description,Address=p.Address.City }).ToList();
                return RedirectToAction("showEmployeeReport", "GenericReportViewer");
            }
            return View();
        }
        #endregion

        #region MIS Reports
        public ActionResult MISReports()
        {
            this.EmployeeStrength();
            this.SaleBillByMonth();
            this.FoodItemsWiseSale();
            return View();
        }
        public void EmployeeStrength()
        {
            var data = employeeServices.Employee.GetByAll().Where(x => x.Active == true).ToList();
            List<int> rankrepo = new List<int>();
            var ranks = data.Select(r => r.Rank.Description).Distinct();
            foreach (var item in ranks)
            {
                rankrepo.Add(data.Count(c => c.Rank.Description == item));
            }
            ViewBag.RankName = ranks;
            ViewBag.RankCount = rankrepo;
        }
        public void SaleBillByMonth()
        {
            List<string> salebilldate = new List<string>();
            List<decimal> netamtrepo = new List<decimal>();
            var data = saleBillServices.SaleBill.GetByAll().Where(x => x.Active == true).ToList();
            var salebills =data.Select(s => s.OrderMaster.OrderDate.Value.Month).Distinct();                  
                foreach(var disitems in salebills)
                {
                        decimal netamt = 0;
                        foreach (var allitems in data)
                        {
                            if (allitems.OrderMaster.OrderDate.Value.Month==disitems)
                            {
                                netamt += allitems.NetAmount;
                            }
                        }
                        netamtrepo.Add(netamt);
               }
            ViewBag.orderDate = cBRMSHelper.ConvertToMonth(salebills) ;
            ViewBag.saledata = netamtrepo;
        }
        public void FoodItemsWiseSale()
        {
            //string tempfooditemscountlist= string.Empty;
            //string tempfooditemsname = string.Empty;
            //icharts.FoodItemsWiseSale(out tempfooditemscountlist, out tempfooditemsname);
            var data = billFoodItemsServices.BillFoodItems.GetByAll().Where(x => x.Active == true).ToList();
            var fooditemsSaleCount = data.Select(x => x.FoodITemsDetails.Description).Distinct();
            List<decimal> fooditemrepo = new List<decimal>();                   
                foreach (var disitem in fooditemsSaleCount)
                {
                decimal q = 0;
                foreach (var allitem in data)
                {
                    if (allitem.FoodITemsDetails.Description== disitem)
                    {
                        q += allitem.Quantity;
                    }
                }
                fooditemrepo.Add(q);
            }
            ViewBag.FoodItemsCount_List = fooditemsSaleCount;
            ViewBag.FoodItemsname_List = fooditemrepo;
        }

        public ContentResult GetOrderData()
        {
            List<MISOrderdata> datalist = new List<MISOrderdata>();
            var data = orderMasterServices.OrderItems.GetByAll().Where(x => x.Active == true).ToList();
            var datadisc = data.Select(d => d.OrderMaster.OrderDate.Value.Month).Distinct();
            foreach(var discitem in datadisc)
            {
                decimal q = 0;
                decimal amt = 0;
                string month = string.Empty;
                MISOrderdata misdata = new MISOrderdata();
                foreach (var allitem in data)
                {                 
                    if (allitem.OrderMaster.OrderDate.Value.Month == discitem)
                    {                     
                        month= cBRMSHelper.ConvertToMonth(allitem.OrderMaster.OrderDate.Value.Month);
                        q += allitem.Quantity;
                        amt += allitem.Amount;
                    }                 
                }
                misdata.Ordermonthname = month;
                misdata.Quantity = q;
                misdata.Amount = amt;
                datalist.Add(misdata);
            }         
            return Content(JsonConvert.SerializeObject(datalist), "application/json");
        }
        #endregion

        [HttpGet]
        public JsonResult GetAllCustomerRegisterRecord()
        {
            string customerValue = customerServices.Customer.GetByAll().Where(x => x.Active == true).Count().ToString();
         
            List<ChartJs> data = new List<ChartJs>();

            ChartJs customer = new ChartJs();
            customer.label = "Customer";
            customer.value = customerValue;
            data.Add(customer);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult animatedpage()
        {
            return View();
        }
    }
}