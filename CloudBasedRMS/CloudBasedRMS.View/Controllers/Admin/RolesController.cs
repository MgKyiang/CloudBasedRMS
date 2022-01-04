
using CloudBasedRMS.Core;
using CloudBasedRMS.View.Controllers.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers
{
    public class RolesController : ControllerAuthorizeBase
    {
        // GET: Admin/Roles
        public ActionResult Index(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                ViewBag.Status = status;
            }

            var data = IdentityRoleManager.Roles.ToList();

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        //Remote Validation in MVC:that is grate in mvc.
        public ActionResult CheckRoleNameExists(RoleViewModel model)
        {
            bool RoleExists = false;
            try
            {
                bool checkRolename = IdentityRoleManager.Roles.Any(x => x.Name.Replace(" ", string.Empty).ToLower() == model.RoleName.Replace(" ", string.Empty).ToLower());
                if (!checkRolename)
                {
                    RoleExists = false;
                }
                else
                {
                    RoleExists = true;
                }
                return Json(!RoleExists, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool checkRolename = IdentityRoleManager.Roles.Any(x => x.Name.Replace(" ", string.Empty).ToLower() == model.RoleName.Replace(" ", string.Empty).ToLower());

                if (!checkRolename)
                {
                    ApplicationRole role = new ApplicationRole()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = model.RoleName,
                        CreatedDate = DateTime.Now,
                        IsBuildIn = false
                    };

                    IdentityResult result = IdentityRoleManager.Create(role);

                    if (result.Succeeded)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult Edit(string Id)
        {

            if (!string.IsNullOrEmpty(Id))
            {

                ApplicationRole role = IdentityRoleManager.Roles.Where(x => x.Id == Id).SingleOrDefault();

                RoleViewModel model = new RoleViewModel() { Id = role.Id, RoleName = role.Name };

                return Json(model, JsonRequestBehavior.AllowGet);

            }

            return Json(string.Empty, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                ApplicationRole role = IdentityRoleManager.Roles.Where(x => x.Id == model.Id).SingleOrDefault();

                bool checkRoleName = IdentityRoleManager.Roles.Any(x =>
                x.Name.Replace(" ", string.Empty).ToLower() != role.Name.Replace(" ", string.Empty).ToLower() &&
                x.Name.Replace(" ", string.Empty).ToLower() == model.RoleName.Replace(" ", string.Empty).ToLower());

                if (!checkRoleName)
                {
                    role.Name = model.RoleName;
                    role.UpdatedDate = DateTime.Now;

                    IdentityResult result = IdentityRoleManager.Update(role);

                    if (result.Succeeded)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Delete(RoleViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                bool checkBuildIn = IdentityRoleManager.Roles.Any(x => x.Id == model.Id && x.IsBuildIn == true);

                if (!checkBuildIn)
                {
                    ApplicationRole role = IdentityRoleManager.Roles.Where(x => x.Id == model.Id && x.IsBuildIn == false).SingleOrDefault();


                    IdentityResult result = IdentityRoleManager.Delete(role);

                    if (result.Succeeded)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }




    }
}