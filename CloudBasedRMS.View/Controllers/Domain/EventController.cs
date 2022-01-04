using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using CloudBasedRMS.View.Controllers.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers
{
    public class EventController : ControllerAuthorizeBase
    {
        public EventServices _EventServices;
        public EventController()
        {
            _EventServices = new EventServices();
        }
        public ActionResult Index()
        {
            var data = _EventServices.Event.GetByAll().Where(x => x.Active == true).ToList();
            return View(data);
        }
        public JsonResult GetEvents()
        {
            var events = _EventServices.Event.GetByAll().Where(x => x.Active == true).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Event()
        {
            return View();
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(EventViewModel viewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkdata = _EventServices.Event.GetByAll().Any(x => x.Title == viewmodel.Title && x.Active == true);
                    if (!checkdata)
                    {
                        Event model = new Event();
                        model.EventID = Guid.NewGuid().ToString();
                        model.Title = viewmodel.Title;                        
                        model.Start = viewmodel.Start;
                        model.End = viewmodel.End;
                        model.ThemeColor = viewmodel.ThemeColor;
                        model.IsFullDay = viewmodel.IsFullDay;
                        model.Active = true;
                        model.CreatedUserID = CurrentApplicationUser.Id;
                        model.CreatedDate = DateTime.Now;
                        _EventServices.Event.Add(model);
                        _EventServices.Save();
                        return RedirectToAction("Index", new { status = "Save Successful!" });
                    }
                    else
                    {
                        return RedirectToAction("Index", new { status = "Data Already Existing!" });
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        // GET: Event/Edit/5
        public ActionResult Edit(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                Event model = _EventServices.Event.GetByID(id);
                EventViewModel viewmodel = new EventViewModel()
                {
                    EventID = model.EventID,
                    Title = model.Title,                   
                    Start = model.Start,
                    End = model.End,
                    IsFullDay = model.IsFullDay,
                    ThemeColor = model.ThemeColor
                };
                return View(viewmodel);
            }
            return View();
        }
        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(EventViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                Event model = _EventServices.Event.GetByID(viewmodel.EventID);
                model.Title = viewmodel.Title;       
                model.Start = viewmodel.Start;
                model.End = viewmodel.End;
                model.ThemeColor = viewmodel.ThemeColor;
                model.IsFullDay = viewmodel.IsFullDay;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedUserID = CurrentApplicationUser.Id;
                _EventServices.Event.Update(model);
                _EventServices.Save();
                return RedirectToAction("Index", new { status = "Update Successful!" });
            }
            return View();
        }
        // GET: Event/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Event model = _EventServices.Event.GetByID(id);
                EventViewModel viewmodel = new EventViewModel()
                {
                    EventID = model.EventID,
                    Title = model.Title,             
                    Start = model.Start,
                    End = model.End,
                    IsFullDay = model.IsFullDay,
                    ThemeColor = model.ThemeColor
                };
                return View(viewmodel);
            }
            return View();
        }
        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(EventViewModel viewmodel)
        {
            try
            {
                if (!string.IsNullOrEmpty(viewmodel.EventID))
                {
                    Event model = _EventServices.Event.GetByID(viewmodel.EventID);
                    model.Active = false;
                    _EventServices.Event.Update(model);
                    _EventServices.Save();
                    return RedirectToAction("Index", new { status = "Delete Successful!" });
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
