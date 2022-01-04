namespace CloudBasedRMS.View.Controllers
{
    using System;
    using System.Linq;
    using CloudBasedRMS.Core;
    using System.Web.Mvc;
    using Services;
    using ViewModel;

    public  class VehicleTypeController: ControllerAuthorizeBase
    {
        public VehicleTypeServices _vehicleTypeServices;
        public VehicleTypeController()
        {
            _vehicleTypeServices = new VehicleTypeServices();
        }
        // GET: VehicleType
        public ActionResult Index()
        {
            var data = _vehicleTypeServices.VehicleType.GetByAll().Where(x => x.Active == true).ToList();
            return View(data);
        }
        // GET: VehicleType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleType/Create
        [HttpPost]
        public ActionResult Create(VehicleTypeViewModel viewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkdata = _vehicleTypeServices.VehicleType.GetByAll().Any(x => x.Code == viewmodel.Code && x.Active == true);
                    if (!checkdata)
                    {
                        VehicleType model = new VehicleType();
                        model.VehicleTypeID = Guid.NewGuid().ToString();
                        model.Code = viewmodel.Code;
                        model.Description = viewmodel.Description;
                        model.Active = true;
                        model.CreatedUserID = CurrentApplicationUser.Id;
                        model.CreatedDate = DateTime.Now;
                        _vehicleTypeServices.VehicleType.Add(model);
                        _vehicleTypeServices.Save();
                        Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.Description), true);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Success(string.Format("<b>{0}</b> was already existed in the system.", viewmodel.Description), true);
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

        // GET: VehicleType/Edit/5
        public ActionResult Edit(string id)
        {
            
                if (!string.IsNullOrEmpty(id))
                {
                    VehicleType model = _vehicleTypeServices.VehicleType.GetByID(id);
                    VehicleTypeViewModel viewmodel = new VehicleTypeViewModel()
                    {
                        VehicleTypeID=model.VehicleTypeID,
                        Code = model.Code,
                        Description = model.Description
                    };
                    return View(viewmodel);
                }
            return View();
        }

        // POST: VehicleType/Edit/5
        [HttpPost]
        public ActionResult Edit(VehicleTypeViewModel viewmodel)
        {
                if (ModelState.IsValid)
                {
                    VehicleType model = _vehicleTypeServices.VehicleType.GetByID(viewmodel.VehicleTypeID);
                    model.Code =viewmodel.Code;
                    model.Description = viewmodel.Description;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedUserID = CurrentApplicationUser.Id;
                    _vehicleTypeServices.VehicleType.Update(model);
                    _vehicleTypeServices.Save();
                Success(string.Format("<b>{0}</b> was successfully updated to the system.", viewmodel.Description), true);
                return RedirectToAction("Index");
            }
                return View();
        }

        // GET: VehicleType/Delete/5
        [HttpGet]
        public ActionResult Delete(string  id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                VehicleType model = _vehicleTypeServices.VehicleType.GetByID(id);
                VehicleTypeViewModel viewmodel = new VehicleTypeViewModel()
                {
                    VehicleTypeID =model.VehicleTypeID,
                    Code =model.Code,
                    Description =model.Description
                };
                return View(viewmodel);
            }
            return View();
        }

        // POST: VehicleType/Delete/5
        [HttpPost]
        public ActionResult Delete(VehicleTypeViewModel viewmodel)
        {
            try
            {
                if (!string.IsNullOrEmpty(viewmodel.VehicleTypeID))
                {
                    VehicleType model = _vehicleTypeServices.VehicleType.GetByID(viewmodel.VehicleTypeID);
                     model.Active = false;
                    _vehicleTypeServices.VehicleType.Update(model);
                    _vehicleTypeServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.Description), true);
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

