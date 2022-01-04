using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.View.Controllers.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers.Domain
{
    public class SupplierController : ControllerAuthorizeBase
    {
        public SupplierServices supplierServices;
        public SupplierController()
        {
            supplierServices = new SupplierServices();
        }
        // GET: Supplier
        public ActionResult Index()
        {
            var data = supplierServices.Supplier.GetByAll().Where(x => x.Active == true).ToList();
            return View(data);
        }
        #region Create
        // GET: Supplier/Create
        public ActionResult Create()
        {
            ddlDatabind();
            return View();
        }
        private void ddlDatabind()
        {
            ViewBag.AddressID = new SelectList(supplierServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City");
        }
        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(SupplierViewModel viewmodel)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                bool checkdata = supplierServices.Supplier.GetByAll().Any(x => x.SupplierName == viewmodel.SupplierName && x.Active == true);
                if (!checkdata)
                {
                    Supplier model = new Supplier()
                    {
                        SupplierID = Guid.NewGuid().ToString(),
                        SupplierName = viewmodel.SupplierName,
                        Phone = viewmodel.Phone,
                        AddressID = viewmodel.AddressID,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id,
                        Active = true
                    };
                    supplierServices.Supplier.Add(model);
                    supplierServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.SupplierName), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Information(string.Format("<b>{0}</b> was  already existing in the system.", viewmodel.SupplierName), false);
                    return RedirectToAction("Index");
                }
            }
            Danger("can't save your data!", false);
            ddlDatabind();
            return View();
        }
        #endregion
        // GET: Supplier/Edit/5
        public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                Supplier model = supplierServices.Supplier.GetByID(Id);
                SupplierViewModel viewmodel = new SupplierViewModel();
                viewmodel.SupplierID = model.SupplierID;
                viewmodel.SupplierName = model.SupplierName;
                viewmodel.Phone = model.Phone;
                ViewBag.AddressID = new SelectList(supplierServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City", model.AddressID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(SupplierViewModel viewmodel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    Supplier model = supplierServices.Supplier.GetByID(viewmodel.SupplierID);
                    model.SupplierName = viewmodel.SupplierName;
                    model.Phone = viewmodel.Phone;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedUserID = CurrentApplicationUser.Id;
                    model.AddressID = viewmodel.AddressID;
                    supplierServices.Supplier.Update(model);
                    supplierServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully updated to the system.", viewmodel.SupplierName), true);
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

        // GET: Supplier/Delete/5
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Supplier model = supplierServices.Supplier.GetByID(id);
                SupplierViewModel viewmodel = new SupplierViewModel();
                viewmodel.SupplierID = model.SupplierID;
                viewmodel.SupplierName = model.SupplierName;
                viewmodel.Phone = model.Phone;
                viewmodel.Address =supplierServices.Address.GetByID(model.AddressID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(SupplierViewModel viewmodel)
        {
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrEmpty(viewmodel.SupplierID))
                {
                    Supplier model = supplierServices.Supplier.GetByID(viewmodel.SupplierID);
                    model.Active = false;
                    supplierServices.Supplier.Update(model);
                    supplierServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.SupplierName), true);
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