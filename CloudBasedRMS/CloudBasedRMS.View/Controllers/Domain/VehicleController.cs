namespace CloudBasedRMS.View.Controllers
{
    using System.Data.Entity.Infrastructure;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Services;
    using Core;
    using ViewModel;
    public class VehicleController :ControllerAuthorizeBase
    {
        public VehicleServices vehicleServices;
        public VehicleController()
        {
            vehicleServices = new VehicleServices();
        }
        // GET: Vehicle
        public ActionResult Index()
        {
            var data = vehicleServices.vehicle.GetByAll().Where(x => x.Active == true).ToList();
            return View(data);
        }
        #region Create
        // GET: Vehicle/Create
        public ActionResult Create()
        {
            ViewBag.VehicleType = new SelectList( vehicleServices.vehicleType.GetByAll().Where(x => x.Active == true).ToList(), "VehicleTypeID", "Description");
            return View();
            
        }

        // POST: Vehicle/Create
        [HttpPost]
        public ActionResult Create(VehicleViewModel viewmodel)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                bool checkdata = vehicleServices.vehicle.GetByAll().Any(x => x.RegistrationNo == viewmodel.RegistrationNo && x.Active == true);
                if (!checkdata)
                {
                    Vehicle model = new Vehicle()
                    {
                        VehicleID = Guid.NewGuid().ToString(),
                        RegistrationNo = viewmodel.RegistrationNo,
                        Status = viewmodel.Status,
                        VehicleTypeID = viewmodel.VehicleTypeID,
                         CreatedDate = DateTime.Now,
                       CreatedUserID = CurrentApplicationUser.Id,
                       Active = true
                     };
                    vehicleServices.vehicle.Add(model);
                    vehicleServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.RegistrationNo), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Success(string.Format("<b>{0}</b> was already existed in the system.", viewmodel.RegistrationNo), true);
                    return RedirectToAction("Index");
                }
            }
           
            ViewBag.VehicleType = new SelectList(vehicleServices.vehicleType.GetByAll().Where(x => x.Active == true).ToList(), "VehicleTypeID", "Description");
            return View();
        }
        #endregion
        // GET: Vehicle/Edit/5
        public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                Vehicle model = vehicleServices.vehicle.GetByID(Id);
                VehicleViewModel viewmodel = new VehicleViewModel();
                viewmodel.VehicleID = model.VehicleID;
                viewmodel.Status = model.Status;
                viewmodel.RegistrationNo = model.RegistrationNo;
                ViewBag.VehicleTypeID = new SelectList(vehicleServices.vehicleType.GetByAll().Where(x => x.Active == true).ToList(), "VehicleTypeID", "Description", model.VehicleTypeID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        public ActionResult Edit(VehicleViewModel viewmodel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    Vehicle model = vehicleServices.vehicle.GetByID(viewmodel.VehicleID);
                    model.RegistrationNo = viewmodel.RegistrationNo;
                    model.Status = viewmodel.Status;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedUserID = CurrentApplicationUser.Id;
                    model.VehicleTypeID = viewmodel.VehicleTypeID;
                    vehicleServices.vehicle.Update(model);
                    vehicleServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully updated to the system.", viewmodel.RegistrationNo), true);
                    return RedirectToAction("Index");
                }
             
            }
            catch(RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator");
            }
            return View();
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(string  id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Vehicle model = vehicleServices.vehicle.GetByID(id);
                VehicleViewModel viewmodel = new VehicleViewModel();
                viewmodel.VehicleID = model.VehicleID;
                viewmodel.Status = model.Status;
                viewmodel.RegistrationNo = model.RegistrationNo;
                viewmodel.VehicleType = vehicleServices.vehicleType.GetByID(model.VehicleTypeID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Vehicle/Delete/5
        [HttpPost]
        public ActionResult Delete(VehicleViewModel viewmodel)
        {
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrEmpty(viewmodel.VehicleID))
                {
                    Vehicle model = vehicleServices.vehicle.GetByID(viewmodel.VehicleID);
                    model.Active = false;
                    vehicleServices.vehicle.Update(model);
                    vehicleServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.RegistrationNo), true);
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
    }
}
