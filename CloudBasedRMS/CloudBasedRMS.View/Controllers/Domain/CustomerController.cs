namespace CloudBasedRMS.View.Controllers
{
    using Core;
    using Services;
    using ViewModel;
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Web.Mvc;
    public   class CustomerController: ControllerAuthorizeBase
    {
        public CustomerServices CustomerServices;
        public CustomerController()
        {
            CustomerServices = new CustomerServices();
        }
        // GET: Customer
        public ActionResult Index()
        {
            var data = CustomerServices.Customer.GetByAll().Where(x => x.Active == true).ToList();
            return View(data);
        }
        #region GET Create
        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.Address = new SelectList(CustomerServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City");
            return View();

        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(CustomerViewModel viewmodel)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                bool checkdata = CustomerServices.Customer.GetByAll().Any(x => x.Email == viewmodel.Email && x.Active == true);
                if (!checkdata)
                {
                    Customer model = new Customer()
                    {
                        CustomerID = Guid.NewGuid().ToString(),
                        Name = viewmodel.Name,
                        PhoneNo = viewmodel.PhoneNo,
                        Email = viewmodel.Email,
                        MobileNo = viewmodel.MobileNo,
                        AddressID=viewmodel.AddressID,
                        Note=viewmodel.Note,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = CurrentApplicationUser.Id,
                        Active = true
                    };
                    CustomerServices.Customer.Add(model);
                    CustomerServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.Name), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Success(string.Format("<b>{0}</b> was already existed to the system.", viewmodel.Name), true);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Address = new SelectList(CustomerServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City");
            return View();
        }
        #endregion
        // GET: Customer/Edit/5
        public ActionResult Edit(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                Customer model = CustomerServices.Customer.GetByID(Id);
                CustomerViewModel viewmodel = new CustomerViewModel();
                viewmodel.Name = model.Name;
                viewmodel.Email = model.Email;
                viewmodel.CustomerID = model.CustomerID;
                viewmodel.PhoneNo = model.PhoneNo;
                viewmodel.MobileNo = model.MobileNo;
                viewmodel.Note = model.Note;
                ViewBag.Address = new SelectList(CustomerServices.Address.GetByAll().Where(x => x.Active == true).ToList(), "AddressID", "City", model.AddressID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(CustomerViewModel viewmodel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    Customer model = CustomerServices.Customer.GetByID(viewmodel.CustomerID);
                    model.Name = viewmodel.Name;
                    model.PhoneNo = viewmodel.PhoneNo;
                    model.Email = viewmodel.Email;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedUserID = CurrentApplicationUser.Id;
                    model.AddressID = viewmodel.AddressID;
                    model.MobileNo = viewmodel.MobileNo;
                    model.Note = viewmodel.Note;
                    CustomerServices.Customer.Update(model);
                    CustomerServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully updated to the system.", viewmodel.Name), true);
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

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Customer model = CustomerServices.Customer.GetByID(id);
                CustomerViewModel viewmodel = new CustomerViewModel();
                viewmodel.Name = model.Name;
                viewmodel.Email = model.Email;
                viewmodel.CustomerID = model.CustomerID;
                viewmodel.PhoneNo = model.PhoneNo;
                viewmodel.MobileNo = model.MobileNo;
                viewmodel.Note = model.Note;
                viewmodel.Address = CustomerServices.Address.GetByID(model.AddressID);
                return View(viewmodel);
            }
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(CustomerViewModel viewmodel)
        {
            try
            {
                // TODO: Add delete logic here
                if (!string.IsNullOrEmpty(viewmodel.CustomerID))
                {
                    Customer model = CustomerServices.Customer.GetByID(viewmodel.CustomerID);
                    model.Active = false;
                    CustomerServices.Customer.Update(model);
                    CustomerServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.Name), true);
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
