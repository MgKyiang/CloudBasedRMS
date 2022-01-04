namespace CloudBasedRMS.View.Controllers
{
    using System;
    using System.Linq;
    using Core;
    using Services;
    using System.Web.Mvc;
    using ViewModel;
    using Logger;

    public  class AddressController:ControllerAuthorizeBase
    {
        public AddressServices addressServices;
        
        public AddressController()
        {
            addressServices = new AddressServices();
            
        } 
        public ActionResult Index()
        {
            var data = addressServices.Address.GetByAll().Where(x => x.Active == true).ToList();
            return View(data);
        }
        #region for Address drop down list event
        public JsonResult GetAddress()
        {
            var address = addressServices.Address.GetByAll().Where(x => x.Active == true).ToList();
            return new JsonResult { Data = address, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return Json(address, JsonRequestBehavior.AllowGet);
        }
        #endregion
        // GET: Address/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Address/Create
        [HttpPost]
        public ActionResult Create(AddressViewModel viewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkdata = addressServices.Address.GetByAll().Any(x => x.ZipCode == viewmodel.ZipCode && x.Active == true);
                    if (!checkdata)
                    {
                        Address model = new Address();
                        model.AddressID = Guid.NewGuid().ToString();
                        model.City = viewmodel.City;
                        model.Township = viewmodel.Township;
                        model.Place = viewmodel.Place;
                        model.ZipCode = viewmodel.ZipCode;
                        model.Area = viewmodel.Area;
                        model.Active = true;
                        model.CreatedUserID = CurrentApplicationUser.Id;
                        model.CreatedDate = DateTime.Now;
                        addressServices.Address.Add(model);
                        addressServices.Save();
                        Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.City), true);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Warning(string.Format("<b>{0}</b> was already existed in the system.", viewmodel.City), true);
                        return RedirectToAction("Index");

                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        // GET: Address/Edit/5
        public ActionResult Edit(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                Address model = addressServices.Address.GetByID(id);
                AddressViewModel viewmodel = new AddressViewModel()
                {
                    AddressID = model.AddressID,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    Township = model.Township,
                    Area = model.Area,
                    Place = model.Place
                };
                return View(viewmodel);
            }
            return View();
        }
        // POST: Address/Edit/5
        [HttpPost]
        public ActionResult Edit(AddressViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                Address model = addressServices.Address.GetByID(viewmodel.AddressID);
                model.City = viewmodel.City;
                model.Township = viewmodel.Township;
                model.Place = viewmodel.Place;
                model.ZipCode = viewmodel.ZipCode;
                model.Area = viewmodel.Area;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedUserID = CurrentApplicationUser.Id;
                addressServices.Address.Update(model);
                addressServices.Save();
                Success(string.Format("<b>{0}</b> was successfully updated to the system.", viewmodel.City), true);
                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: Address/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Address model = addressServices.Address.GetByID(id);
                AddressViewModel viewmodel = new AddressViewModel()
                {
                    AddressID = model.AddressID,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    Township = model.Township,
                    Area=model.Area,
                    Place=model.Place
                };
                return View(viewmodel);
            }
            return View();
        }
        // POST: Address/Delete/5
        [HttpPost]
        public ActionResult Delete(AddressViewModel viewmodel)
        {
            try
            {
                if (!string.IsNullOrEmpty(viewmodel.AddressID))
                {
                    Address model = addressServices.Address.GetByID(viewmodel.AddressID);
                    model.Active = false;
                    addressServices.Address.Update(model);
                    addressServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.City), true);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
