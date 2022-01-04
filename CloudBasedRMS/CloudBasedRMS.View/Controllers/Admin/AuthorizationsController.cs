
using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using PagedList;
using CloudBasedRMS.View.Controllers.ViewModel;

namespace CloudBasedRMS.View.Controllers
{
    public class AuthorizationsController : ControllerAuthorizeBase
    {
        private Authorizationservices _authorizeServices;
        public AuthorizationsController()
        {
            _authorizeServices = new Authorizationservices();
        }
        #region Index
        [HttpGet]
        public ActionResult Index(string searchby1, string search1, string searchby2, string search2, string searchby3, string search3, string searchby4, string search4, string sortby, int? page, string status)
        {
            //Sorting ViewBag
            ViewBag.ControllerSortby = sortby == "ControllerAsc" ? "ControllerDesc" : "ControllerAsc";
            ViewBag.ActionSortby = sortby == "ActionAsc" ? "ActionDesc" : "ActionAsc";
            ViewBag.RoleSortby = sortby == "RoleAsc" ? "RoleDesc" : "RoleAsc";

            //For Showing Alert
            ViewBag.Searchby1 = searchby1;
            ViewBag.Search1 = search1;
            ViewBag.Searchby2 = searchby2;
            ViewBag.Search2 = search2;
            ViewBag.Searchby3 = searchby3;
            ViewBag.Search3 = search3;
            ViewBag.Searchby4 = searchby4;
            ViewBag.Search4 = search4;

            ViewBag.sortby = sortby;
            ViewBag.Status = status;

            //GET Raw Data
            var data = _authorizeServices.Authorizations.GetByAll().Where(x => x.Active == true).ToList();
            //Searching
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
                data = GetSearchingData(searchby4, search4, data);
            }
            //Sorting
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "ControllerAsc":
                        data = data.OrderBy(x => x.ControllerName).ToList();
                        break;
                    case "ControllerDesc":
                        data = data.OrderByDescending(x => x.ControllerName).ToList();
                        break;
                    case "ActionAsc":
                        data = data.OrderBy(x => x.ActionName).ToList();
                        break;
                    case "ActionDesc":
                        data = data.OrderByDescending(x => x.ActionName).ToList();
                        break;
                    case "RoleAsc":
                        data = data.OrderBy(x => x.ApplicationRole.Name).ToList();
                        break;
                    case "RoleDesc":
                        data = data.OrderByDescending(x => x.ApplicationRole.Name).ToList();
                        break;
                }
            }
            return View(data.ToPagedList(page ?? 1, ApplicationSettingPagingSize));
        }
    
        [HttpPost]
        public ActionResult Index(List<Authorizations> dataModel)
        {

            if (dataModel.Count > 0)
            {
                foreach (var i in dataModel)
                {
                    Authorizations entity = _authorizeServices.Authorizations.GetByID(i.AuthorizationsID);
                    entity.IsAllow = i.IsAllow;
                    entity.IsUseLog = i.IsUseLog;
                    entity.UpdatedDate = DateTime.Now;
                    entity.UpdatedUserID = CurrentApplicationUser.Id;

                    _authorizeServices.Authorizations.Update(entity);
                    _authorizeServices.Save();
                }
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.roles = new SelectList(IdentityRoleManager.Roles.ToList(), "Id", "Name");
            ViewBag.controllerName = _authorizeServices.Authorizations.GetControllerAndAction().GroupBy(x => new { x.ControllerName }).Select(y => y.FirstOrDefault());
            ViewBag.actionName = _authorizeServices.Authorizations.GetControllerAndAction().GroupBy(x => new { x.ActionName }).Select(y => y.FirstOrDefault());
            return View();
        }
        [HttpPost]
        public ActionResult Create(AuthorizationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool dataAlreadyExists = _authorizeServices.Authorizations.GetByAll().Any(x => x.ActionName.ToLower() == model.ActionName.ToLower() && x.ControllerName.ToLower() == model.ControllerName.ToLower() && x.Active == true&&x.RoleID==model.RoleID);

                if (!dataAlreadyExists)
                {
                    Authorizations entity = new Authorizations()
                    {
                        AuthorizationsID = Guid.NewGuid().ToString(),
                        ControllerName = model.ControllerName.Replace(" ", string.Empty),
                        ActionName = model.ActionName.Replace(" ", string.Empty),
                        RoleID = model.RoleID,
                        IsAllow = model.IsAllow,
                        IsUseLog = model.UseLog,
                        CreatedUserID = CurrentApplicationUser.Id,
                        CreatedDate = DateTime.Now,
                        Active = true
                    };

                    _authorizeServices.Authorizations.Add(entity);
                    _authorizeServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully added to the system.", model.ActionName), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning(string.Format("<b>{0}</b> was already existed in the system.", model.ActionName), true);
                    return RedirectToAction("Index");
                    
                }

            }
            return View();
        }
        #endregion
        #region Edit
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.roles = new SelectList(IdentityRoleManager.Roles.ToList(), "Id", "Name");

            if (!string.IsNullOrEmpty(id))
            {
                Authorizations entity = _authorizeServices.Authorizations.GetByID(id);

                AuthorizationsViewModel model = new AuthorizationsViewModel()
                {
                    ID = entity.AuthorizationsID,
                    ControllerName = entity.ControllerName,
                    ActionName = entity.ActionName,
                    RoleID = entity.RoleID,
                    IsAllow = entity.IsAllow,
                    UseLog = entity.IsUseLog
                };

                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(AuthorizationsViewModel model)
        {
            ViewBag.roles = new SelectList(IdentityRoleManager.Roles.ToList(), "Id", "Name");

            if (ModelState.IsValid)
            {

                Authorizations entity = _authorizeServices.Authorizations.GetByID(model.ID);
                entity.ActionName = model.ActionName;
                entity.ControllerName = model.ControllerName;
                entity.RoleID = model.RoleID;
                entity.IsAllow = model.IsAllow;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedUserID = CurrentApplicationUser.Id;
                entity.IsUseLog = model.UseLog;

                _authorizeServices.Authorizations.Update(entity);
                _authorizeServices.Save();
                Success(string.Format("<b>{0}</b> was successfully updated to the system.", entity.ActionName), true);
                return RedirectToAction("Index");          
            }

            return View();
        }
        #endregion
        #region Delete
        [HttpGet]
        public ActionResult Delete(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                Authorizations entity = _authorizeServices.Authorizations.GetByID(Id);

                AuthorizationsViewModel model = new AuthorizationsViewModel()
                {
                    ID = entity.AuthorizationsID,
                    ControllerName = entity.ControllerName,
                    ActionName = entity.ActionName,
                    RoleID = entity.RoleID,
                    IsAllow = entity.IsAllow
                };

                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(AuthorizationsViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ID))
            {
                Authorizations entity = _authorizeServices.Authorizations.GetByID(model.ID);
                entity.Active = false;

                _authorizeServices.Authorizations.Update(entity);
                _authorizeServices.Save();
                Success(string.Format("<b>{0}</b> was successfully deleted from the system.", entity.ActionName), true);
                return RedirectToAction("Index");

            }
            return View();
        }
        #endregion
        #region Add for Auto Process
        [HttpGet]
        public ActionResult Add()
        {
            List<ApplicationRole> roles = IdentityRoleManager.Roles.ToList();
            //AuthorizaitonsViewModel List
            List<AuthorizationsViewModel> entities = new List<AuthorizationsViewModel>();
            // getting all DLL Path File
            string assemblyDllFilePath = Server.MapPath("~/bin/CloudBasedRMS.View.DLL");

            //Assembly from dll File
            Assembly controllerDLL = Assembly.LoadFile(assemblyDllFilePath);

            //GET ControllerName, ActionResult =>[ActionResult,JsonResult,PartialViewResult,FileContentResult,ContentResult]
            var controllerTypes = (from i in controllerDLL.ExportedTypes.Where(x => x.BaseType.Name == "ControllerAuthorizeBase")
                                   from j in i.GetMethods().Where(x => x.ReturnType.Name == "ActionResult" || x.ReturnType.Name == "JsonResult" || x.ReturnType.Name == "PartialViewResult" || x.ReturnType.Name == "FileContentResult" ||x.ReturnType.Name== "ContentResult")
                                   orderby i.Name
                                   select new { ControllerName = i.Name.Replace("Controller", string.Empty), ActionName = j.Name }).Distinct().ToList();
            foreach (var r in roles)
            {
                List<Authorizations> existingAuthorizeEntityList = _authorizeServices.Authorizations.GetByAll().Where(x => x.Active == true && x.RoleID == r.Id).ToList();
                if (controllerTypes.Count > 0 || controllerTypes != null)
                {
                    foreach (var item in controllerTypes)
                    {
                        AuthorizationsViewModel entity = new AuthorizationsViewModel();
                        bool checkValue = existingAuthorizeEntityList
                                                       .Any(x => x.ControllerName.Replace(" ", string.Empty).ToLower() == item.ControllerName.Replace(" ", string.Empty).ToLower() &&
                                                              x.ActionName.Replace(" ", string.Empty).ToLower() == item.ActionName.Replace(" ", string.Empty).ToLower());
                        if (checkValue == false)
                        {
                            entity.ControllerName = item.ControllerName;
                            entity.ActionName = item.ActionName;
                            entity.ApplicationRole = IdentityRoleManager.FindById(r.Id);
                            entity.IsAllow = true;
                            entity.UseLog = true;
                            entities.Add(entity);
                        }
                    }
                }
            }
             return View(entities);
        }

        [HttpPost]
        public ActionResult Add(List<AuthorizationsViewModel> entities)
        {
            if (entities != null)
            {
                List<Authorizations> authorizationsentities = new List<Authorizations>();
                    foreach (var i in entities)
                    {
                        Authorizations entity = new Authorizations();
                        entity.AuthorizationsID = Guid.NewGuid().ToString();
                        entity.ControllerName = i.ControllerName;
                        entity.ActionName = i.ActionName;
                        entity.IsAllow = i.IsAllow;
                        entity.IsUseLog = i.UseLog;
                        entity.RoleID =i.ApplicationRole.Id;
                        entity.CreatedUserID = CurrentApplicationUser.Id;
                        entity.CreatedDate = DateTime.Now;
                        entity.Active = true;

                        authorizationsentities.Add(entity);
                }
                _authorizeServices.Authorizations.AddRange(authorizationsentities);
                _authorizeServices.Save();
                Success(string.Format("<b>{0}</b> was successfully added to the system.", authorizationsentities.Count), true);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion
        #region Helper
        public void InsertNewController()
        {
            List<Authorizations> entities = new List<Authorizations>();
            List<Authorizations> existingAuthorizeEntityList = _authorizeServices.Authorizations.GetByAll().Where(x => x.Active == true).ToList();

            var sysAdminRoleID = IdentityRoleManager.FindByName("admin");

            string assemblyDllFilePath = Server.MapPath("~/bin/CloudBasedRMS.View.DLL");

            Assembly controllerDLL = Assembly.LoadFile(assemblyDllFilePath);

            var controllerTypes = (from i in controllerDLL.ExportedTypes.Where(x => x.BaseType.Name == "ControllerAuthorizeBase")
                                   from j in i.GetMethods().Where(x => x.ReturnType.Name == "ActionResult")
                                   orderby i.Name
                                   select new { ControllerName = i.Name.Replace("Controller", string.Empty), ActionName = j.Name }).Distinct().ToList();


            foreach (var item in controllerTypes)
            {
                Authorizations entity = new Authorizations();

                Authorizations checkValue = existingAuthorizeEntityList
                                               .Where(x => x.ControllerName.Replace(" ", string.Empty).ToLower() == item.ControllerName.Replace(" ", string.Empty).ToLower() &&
                                                      x.ActionName.Replace(" ", string.Empty).ToLower() == item.ActionName.Replace(" ", string.Empty).ToLower()).SingleOrDefault();
                if (checkValue == null)
                {
                    entity.AuthorizationsID = Guid.NewGuid().ToString();
                    entity.ControllerName = item.ControllerName;
                    entity.ActionName = item.ActionName;
                    entity.IsAllow = true;
                    //entity.RoleID = sysAdminRoleID.Id;
                    entity.CreatedUserID = CurrentApplicationUser.Id;
                    entity.CreatedDate = DateTime.Now;
                    entity.Active = true;

                    entities.Add(entity);
                }
            }
            _authorizeServices.Authorizations.AddRange(entities);
            _authorizeServices.Save();

        }
        //Searching Helper
        private List<Authorizations> GetSearchingData(string searchby, string search, List<Authorizations> data)
        {
            switch (searchby)
            {
                case "ControllerName":
                    data = data.Where(x => x.ControllerName.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "ActionName":
                    data = data.Where(x => x.ActionName.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "RoleName":
                    data = data.Where(x => x.ApplicationRole.Name.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "AllowOrDeny":
                    bool allowOrDeny;
                    if (bool.TryParse(search, out allowOrDeny))
                    {
                        data = data.Where(x => x.IsAllow == allowOrDeny).ToList();
                    }
                    break;

                case "UseLog":
                    bool useLog;
                    if (bool.TryParse(search, out useLog))
                    {
                        data = data.Where(x => x.IsUseLog == useLog).ToList();
                    }

                    break;

                case "CreatedUserName":
                    data = data.Where(x => x.CreatedUser.UserName.ToLower().Contains(search.ToLower())).ToList();
                    break;

                case "CreatedDate":
                    DateTime createdDate;
                    if (DateTime.TryParse(search, out createdDate))
                    {
                        data = data.Where(x => x.CreatedDate == createdDate).ToList();
                    }
                    break;
            }
            return data;
        }
        #endregion
    }
}
