

namespace CloudBasedRMS.View.Controllers
{
    using CloudBasedRMS.Core;
    using CloudBasedRMS.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class LogsController : ControllerAuthorizeBase
    {
        private TransactionLogServices logServices;

        public LogsController()
        {
            logServices = new TransactionLogServices();
        }

        // GET: Admin/Logs
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string status)
        {
            ViewBag.Status = status;

            var data = logServices.Logs.GetByAll();

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
                data = GetSearchingData(searchby4, search1, data);
            }

            return View(data.ToList());
        }

        //Searching Helper
        public IEnumerable<TransactionLog> GetSearchingData(string searchby, string search, IEnumerable<TransactionLog> data)
        {
            switch (searchby)
            {
                case "Url":
                    data = data.Where(x => x.Url.Contains(search)).ToList();
                    break;
                case "HttpRequestType":
                    data = data.Where(x => x.HttpRequestType.Contains(search)).ToList();
                    break;
                case "ControllerName":
                    data = data.Where(x => x.ControllerName.Contains(search)).ToList();
                    break;
                case "ActionName":
                    data = data.Where(x => x.ActionName.Contains(search)).ToList();
                    break;
                case "CreatedUserName":
                    data = data.Where(x => x.CreatedUser.UserName.Contains(search)).ToList();
                    break;
                case "CreatedDate":

                    DateTime createdDate;

                    if (DateTime.TryParse(search, out createdDate))
                    {
                        data = data.Where(x => x.CreatedDate.ToShortDateString() == createdDate.ToShortDateString()).ToList();
                    }
                    break;
            }

            return data;
        }
    }
}