using CloudBasedRMS.Core;
using CloudBasedRMS.Services.AdminServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CloudBasedRMS.WebAPI.Common
{
    public class APIAuthorizeBase : AuthorizeAttribute
    {
        private APIAuthorizationServices apiAuthorizationServices;

        private ApplicationRoleManager _applicationRoleManager;
        private ApplicationUserManager _applicationUserManager;

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _applicationRoleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _applicationRoleManager = value;
            }
        }
        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _applicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {

                _applicationUserManager = value;
            }
        }

        /// <summary>
        /// 1.Checking by Request.Principal.Identity.IsAuthenticat is true or false and then false is retrun false
        /// 2.Get Current ApplicationUser by Identity.Name and then User is Null return false
        /// 3.Get Role Name by UserId and then roleName is string empty or null return false
        /// 4.Get Applicaton Role by roleName and then Application Role is null return false
        /// 5.Get ApiAuthorization Data by current request controllerName and actionName and then apiauthorization data is not existed return false
        /// 6.Get DenyApi by request controllerName and actionName and roleId and allowOrDeny is false and active is true and then denyapi is true return false
        /// 7.Get AllowApi by request controllername and ationName and roleId and allowOrDeny is true and active is true and then allow is true return true
        /// </summary>
        /// <param name="actionContext">HttpActionContext</param>
        /// <returns>bool</returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //create instance
            apiAuthorizationServices = new APIAuthorizationServices();

            List<APIAuthorization> apiAuthorizations =apiAuthorizationServices.APIAuthorizations.GetByAll().Where(x => x.Active == true).ToList();

            //checking Authenticate ture or false
            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated == false)
            {
                return false;
            }

            //get application user by identity name
            ApplicationUser appUser =AppUserManager.Users.SingleOrDefault(x => x.UserName.Replace(" ", string.Empty).ToLower() ==actionContext.RequestContext.Principal.Identity.Name.Replace(" ", string.Empty).ToLower());

            // checking application user
            if (appUser == null)
            {
                return false;
            }

            //get role name by current user
            string roleName = AppUserManager.GetRoles(appUser.Id).SingleOrDefault();

            //checking user is in role or not
            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }

            //get application role by role name for current user
            ApplicationRole appRole = AppRoleManager.Roles.SingleOrDefault(x => x.Name.Replace(" ", string.Empty).ToLower() ==
                                                                             roleName.Replace(" ", string.Empty).ToLower());

            //checking role
            if (appRole == null)
            {
                return false;
            }

            //get current api controller name and action name
            string controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionName = actionContext.ActionDescriptor.ActionName.ToLower();

            //data existed or not
            bool registerApi = apiAuthorizations
                        .Any(x => x.ControllerName.Replace(" ", string.Empty).ToLower() == controllerName &&
                                  x.ActionName.Replace(" ", string.Empty).ToLower() == actionName &&
                                  x.ApplicationRole.Id == appRole.Id &&
                                  x.Active == true);
            //if not existed data => return false
            if (!registerApi)
            {
                return false;
            }

            //deny for current controller and action api and current role by user
            bool denyApi = apiAuthorizations
                        .Any(x => x.ControllerName.Replace(" ", string.Empty).ToLower() == controllerName &&
                                  x.ActionName.Replace(" ", string.Empty).ToLower() == actionName &&
                                  x.ApplicationRole.Id == appRole.Id &&
                                  x.AllowOrDeny == false && x.Active == true);

            //checking deny api
            if (denyApi)
            {
                return false;
            }

            //allow for current controller and action api and current role by user
            bool allowApi = apiAuthorizations
                        .Any(x => x.ControllerName.Replace(" ", string.Empty).ToLower() == controllerName &&
                                  x.ActionName.Replace(" ", string.Empty).ToLower() == actionName &&
                                  x.ApplicationRole.Id == appRole.Id &&
                                  x.AllowOrDeny == true && x.Active == true);


            //checking allow api
            if (allowApi)
            {
                return true;
            }

            return base.IsAuthorized(actionContext);
        }
    }
}