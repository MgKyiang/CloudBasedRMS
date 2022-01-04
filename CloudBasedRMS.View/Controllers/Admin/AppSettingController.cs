using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers
{
    public class AppSettingController : ControllerAuthorizeBase
    {
        private ApplicationSettingServices _applicationSettingServices;
        private ApplicationSetting _applicationSettingEntity;

        public AppSettingController()
        {
            _applicationSettingServices = new ApplicationSettingServices();
        }
        [HttpGet]
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string sortby, int? page, string status)
        {
            var data = _applicationSettingServices.ApplicationSettings.GetByAll().Where(x => x.Active == true).ToList();
            //Sorting ViewBag
            ViewBag.KeySortby = sortby == "KeyAsc" ? "KeyDesc" : "KeyAsc";
            ViewBag.ValueSortby = sortby == "ValueAsc" ? "ValueDesc" : "ValueAsc";

            //For Showing Alert
            ViewBag.Searchby1 = searchby1;
            ViewBag.Search1 = search1;

            ViewBag.Searchby2 = searchby2;
            ViewBag.Search2 = search2;

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
            //Sorting
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "KeyAsc":
                        data = data.OrderBy(x => x.Key).ToList();
                        break;
                    case "ValueDesc":
                        data = data.OrderByDescending(x => x.Value).ToList();
                        break;
                    case "KeyDesc":
                        data = data.OrderBy(x => x.Key).ToList();
                        break;
                    case "ValueAsc":
                        data = data.OrderByDescending(x => x.Value).ToList();
                        break;
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }

        [HttpPost]
        public JsonResult Create(ApplicationSetting model)
        {         
                var checkdata = _applicationSettingServices.ApplicationSettings.GetByAll().Any(x => x.Key == model.Key && x.Active == true);

                if (!checkdata)
                {
                    ApplicationSetting appmodel = new ApplicationSetting()
                    {
                        ApplicationSettingID = Guid.NewGuid().ToString(),
                        Key = model.Key,
                        Value = model.Value,
                        Active = true,
                        CreatedUserID = CurrentApplicationUser.Id,
                        CreatedDate = DateTime.Now                     
                    };
                try
                {
                    _applicationSettingServices.ApplicationSettings.Add(appmodel);

                    _applicationSettingServices.BeginTransaction();

                    _applicationSettingServices.Save();

                    // Check that ApplicationSetting entity is not null
                    if (appmodel != null)
                    {
                        // Commit transaction
                        this._applicationSettingServices.Commit();
                    }
                }
                catch (Exception ex)
                {
                    // Check that ApplicationSetting entity is not null
                    if (appmodel != null)
                    {
                        // Commit transaction
                        this._applicationSettingServices.Rollback();
                    }
                    throw;
                }
              
                return Json(true, JsonRequestBehavior.AllowGet);      
                }              
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
        }
            
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                _applicationSettingEntity = new ApplicationSetting();
                _applicationSettingEntity = _applicationSettingServices.ApplicationSettings.SingleOrDefault(x => x.ApplicationSettingID == Id && x.Active == true);

                return View(_applicationSettingEntity);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(ApplicationSetting model)
        {
            if (ModelState.IsValid)
            {
                _applicationSettingEntity = new ApplicationSetting();
                _applicationSettingEntity = _applicationSettingServices.ApplicationSettings.SingleOrDefault(x => x.ApplicationSettingID == model.ApplicationSettingID && x.Active == true);
                _applicationSettingEntity.Key = model.Key;
                _applicationSettingEntity.Value = model.Value;
                _applicationSettingEntity.UpdatedDate = DateTime.Now;
                _applicationSettingEntity.UpdatedUserID = CurrentApplicationUser.Id;

                _applicationSettingServices.ApplicationSettings.Update(_applicationSettingEntity);
                _applicationSettingServices.Save();

                return RedirectToAction("Index");
            }

            return View(model.ApplicationSettingID);
        }
        //Searching Helper
        private List<ApplicationSetting> GetSearchingData(string searchby, string search, List<ApplicationSetting> data)
        {
            switch (searchby)
            {
                case "Key":
                    data = data.Where(x => x.Key.ToLower().Contains(search.ToLower())).ToList();
                    break;
                case "Value":
                    data = data.Where(x => x.Value.ToLower().Contains(search.ToLower())).ToList();
                    break;
            }
            return data;
        }
    }
}
