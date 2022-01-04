using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers
{
    public class AuthorizationsBase : AuthorizeAttribute
    {
        //Create Instance
        private Authorizationservices authorizationServices;
        private TransactionLogServices logServices;
        private TransactionLog logEntity;
        public AuthorizationsBase()
        {
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
        /// <summary>
        /// Authorizations Checking
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //Checking User is In Authenticate
            if (httpContext.User.Identity.IsAuthenticated == false)
            {
                return false;
            }
            authorizationServices = new Authorizationservices();
            //Create User/Role Manager
            var userManager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = httpContext.GetOwinContext().Get<ApplicationRoleManager>();
            List<Authorizations> authorizationsEntities = authorizationServices.Authorizations.GetByAll().Where(x => x.Active == true).ToList();
            if (authorizationsEntities.Count == 0 || authorizationsEntities == null)
            {
                return false;
            }
            //Get Current RouteDate like ConrollerName , ActionName 
            var routeDate = httpContext.Request.RequestContext.RouteData;
            string currentAction = routeDate.GetRequiredString("action");
            string currentController = routeDate.GetRequiredString("controller");
            //Get Current UserName
            string currentUserName = httpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(currentUserName))
            {
                return false;
            }
            //Get Current ApplicationUser
            ApplicationUser currentApplicationUser = userManager.FindByName(currentUserName);

            if (currentApplicationUser == null)
            {
                return false;
            }

            //Get Current RoleName By Current ApplicationUser
            string roleName = userManager.GetRoles(currentApplicationUser.Id).SingleOrDefault();

            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }

            //Get Current ApplicaitonRole By Current RoleName
            ApplicationRole applicaitonRole = roleManager.FindByName(roleName);

            if (applicaitonRole == null)
            {
                return false;
            }
            bool uselog = authorizationsEntities.Any(a => a.ControllerName.Replace(" ", string.Empty).ToLower() == currentController.Replace(" ", string.Empty).ToLower() &&
                                                                                 a.ActionName.Replace(" ", string.Empty).ToLower() == currentAction.Replace(" ", string.Empty).ToLower() &&
                                                                                 a.IsAllow == true &&
                                                                                 a.IsUseLog == true &&
                                                                                 a.RoleID == applicaitonRole.Id &&
                                                                                 a.Active == true);

            bool allow = authorizationsEntities.Any(a => a.ControllerName.Replace(" ", string.Empty).ToLower() == currentController.Replace(" ", string.Empty).ToLower() &&
                                                                               a.ActionName.Replace(" ", string.Empty).ToLower() == currentAction.Replace(" ", string.Empty).ToLower() &&
                                                                               a.IsAllow == true &&
                                                                               a.RoleID == applicaitonRole.Id &&
                                                                               a.Active == true);

            bool deny = authorizationsEntities.Any(a => a.ControllerName.Replace(" ", string.Empty).ToLower() == currentController.Replace(" ", string.Empty).ToLower() &&
                                                                               a.ActionName.Replace(" ", string.Empty).ToLower() == currentAction.Replace(" ", string.Empty).ToLower() &&
                                                                               a.IsAllow == false &&
                                                                               a.RoleID == applicaitonRole.Id &&
                                                                               a.Active == true);

            //For Log
            if (uselog)
            {
                //Saving Log
                Log(httpContext);
            }

            //First
            if (deny)
            {
                return false;
            }

            //Second
            if (allow)
            {
                return true;
            }
            else
            {
                return false;
            }
            //return base.AuthorizeCore(httpContext);
        }

        //Use Log Helper Method
        private void Log(HttpContextBase httpContext)
        {
            logServices = new TransactionLogServices();
            logEntity = new Core.TransactionLog();
            var userManager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentApplicationUser = userManager.FindByName(httpContext.User.Identity.Name);
            //create instance HTTPRequest
            var _httpRequest = httpContext.Request;
            //For Saving Passing Data => Name & Value
            StringBuilder rawDataStringBuilder = new StringBuilder();
            //Get ALl Request Key
            string[] getAllKeys = httpContext.Request.Form.AllKeys;
            //Get Value For Get Key
            foreach (string key in getAllKeys)
            {
                if (key != "__RequestVerificationToken")
                {
                    rawDataStringBuilder.Append("[ " + key + " : " + _httpRequest.Params[key].ToString() + " ]" + "\n");
                }
            }
            logEntity.LogID = System.Guid.NewGuid().ToString();
            logEntity.Url = _httpRequest.Url.AbsoluteUri;
            logEntity.ServerName = _httpRequest.Url.DnsSafeHost;
            logEntity.HostName = _httpRequest.Url.Host;
            logEntity.HttpRequestType = _httpRequest.RequestType;
            logEntity.Port = _httpRequest.Url.Port.ToString();
            logEntity.ControllerName = _httpRequest.RequestContext.RouteData.GetRequiredString("controller");
            logEntity.ActionName = _httpRequest.RequestContext.RouteData.GetRequiredString("action");
            logEntity.RawData = rawDataStringBuilder.ToString();
            logEntity.CreatedUserID = currentApplicationUser.Id;
            logEntity.CreatedDate = DateTime.Now;
            logEntity.Active = true;
            logServices.Logs.Add(logEntity);
            logServices.Save();
        }
    }
}
