using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System;
using System.Threading;
using System.Globalization;
using System.Web.Routing;
using CloudBasedRMS.View.Controllers.Helper;
using System.Collections.Generic;

namespace CloudBasedRMS.View.Controllers
{
    //With Custom Authorize Attribute For Authorize Controller
    [AuthorizationsBase]
    public class ControllerAuthorizeBase : System.Web.Mvc.Controller
    {
        private CBRMSHelper cbrmsHelper;
        private ErrorLogServices errorLogService;
        private ErrorLog errorLogEntity;
        //Application Setting Services Here
        private ApplicationSettingServices applicationSettingServices = new ApplicationSettingServices();

        private ApplicationRoleManager applicationRoleManager;

        private ApplicationSignInManager applicationSignInManager;

        private ApplicationUserManager applicationUserManager;

        public CBRMSHelper DateHelper
        {
            get
            {
                cbrmsHelper = new CBRMSHelper();
                return cbrmsHelper;
            }
        }
        /// <summary>
        /// Current Identity ApplicationUser
        /// </summary>
        public ApplicationUser CurrentApplicationUser
        {
            get
            {
                var currentIdentityId = User.Identity.GetUserId();
                return this.IdentityUserManager.FindById(currentIdentityId);
            }
        }
        #region CultureSetUp
        private string CurrentLanguageCode { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["language"] != null && requestContext.RouteData.Values["language"] as string != "null")
            {
                CurrentLanguageCode = (string)requestContext.RouteData.Values["language"];
                if (CurrentLanguageCode != null)
                {
                    try
                    {
                        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguageCode);
                    }
                    catch (Exception)
                    {
                        throw new NotSupportedException($"Invalid language code '{CurrentLanguageCode}'.");
                    }
                }
            }
            base.Initialize(requestContext);
        }



        #endregion  CultureSetUp
        /// <summary>
        /// Asp.NET Identity RoleManager
        /// Role Class Name => ApplicationRole
        /// </summary>
        public ApplicationRoleManager IdentityRoleManager
        {
            get
            {
                return applicationRoleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set
            {
                applicationRoleManager = value;
            }
        }
        /// <summary>
        ///get Application Setting  Here
        /// </summary>
        public int ApplicationSettingPagingSize
        {
            get {return applicationSettingServices.PagingSize;}
        }
        public string ApplicationSettingApplicationVersion
        {
            get { return applicationSettingServices.ApplicationVersion; }
        }

        public string ApplicationSettingDefaultUserPassword
        {
            get { return applicationSettingServices.DefaultUserPassword; }
        }
        public string ApplicationSettingApplicationName
        {
            get { return applicationSettingServices.ApplicationName; }
        }
        public string ApplicationSettingFooterTradeMark
        {
            get { return applicationSettingServices.FooterTradeMark; }
        }
        
        /// <summary>
        /// Asp.NET Identity SignInManager
        /// </summary>
        public ApplicationSignInManager IdentitySignInManager
        {
            get
            {
                return applicationSignInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                applicationSignInManager = value;
            }
        }

        /// <summary>
        /// Asp.NET Identity UserManager
        /// User Class Name : ApplicationUser
        /// </summary>
        public ApplicationUserManager IdentityUserManager
        {
            get
            {
                return applicationUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                applicationUserManager = value;
            }
        }
        #region Alert
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }
        #endregion
        /// <summary>
        /// ErrorLog if on exception
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)

        {

            //Create New Instance
            errorLogService = new ErrorLogServices();
            errorLogEntity = new ErrorLog();

            errorLogEntity.ErrorLogID = System.Guid.NewGuid().ToString();
            errorLogEntity.ControllerName = filterContext.RouteData.GetRequiredString("controller");
            errorLogEntity.ActionName = filterContext.RouteData.GetRequiredString("action");
            errorLogEntity.ErrorMessage = filterContext.Exception.Message;
            errorLogEntity.Url = filterContext.HttpContext.Request.Url.AbsoluteUri;
            errorLogEntity.IdentityName = filterContext.HttpContext.User.Identity.Name;
            errorLogEntity.MachineName = filterContext.HttpContext.Server.MachineName;
            errorLogEntity.CreatedDate = System.DateTime.Now;

            //save error log
            errorLogService.ErrorLogs.Add(errorLogEntity);
            errorLogService.Save();

            base.OnException(filterContext);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (applicationUserManager != null)
                {
                    applicationUserManager.Dispose();
                    applicationUserManager = null;
                }

                if (applicationSignInManager != null)
                {
                    applicationSignInManager.Dispose();
                    applicationSignInManager = null;
                }

                if (applicationRoleManager != null)
                {
                    applicationRoleManager.Dispose();
                    applicationRoleManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }

    //Without Custom Authorize For Controller 
    public class ControllerBase : System.Web.Mvc.Controller
    {
        private ApplicationRoleManager applicationRoleManager;
        private ApplicationSettingServices applicationSettingServices = new ApplicationSettingServices();
        private ApplicationSignInManager applicationSignInManager;
        private ApplicationUserManager applicationUserManager;
        public ApplicationUser CurrentApplicationUser
        {
            get
            {
                var currentIdentityId = User.Identity.GetUserId();
                ApplicationUser currentUser = IdentityUserManager.FindById(currentIdentityId);
                return currentUser;
            }
        }

        /// <summary>
        /// Asp.NET Identity RoleManager
        /// Role Class Name => ApplicationRole
        /// </summary>
        public ApplicationRoleManager IdentityRoleManager
        {
            get
            {
                return applicationRoleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set
            {
                applicationRoleManager = value;
            }
        }

        /// <summary>
        /// Asp.NET Identity SignInManager
        /// </summary>
        public ApplicationSignInManager IdentitySignInManager
        {
            get
            {
                return applicationSignInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                applicationSignInManager = value;
            }
        }

        /// <summary>
        /// Asp.NET Identity UserManager
        /// User Class Name : ApplicationUser
        /// </summary>
        public ApplicationUserManager IdentityUserManager
        {
            get
            {
                return applicationUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                applicationUserManager = value;
            }
        }
        /// <summary>
        ///get Application Setting  Here
        /// </summary>
        public int ApplicationSettingPagingSize
        {
            get { return applicationSettingServices.PagingSize; }
        }
        public string ApplicationSettingApplicationVersion
        {
            get { return applicationSettingServices.ApplicationVersion; }
        }

        public string ApplicationSettingDefaultUserPassword
        {
            get { return applicationSettingServices.DefaultUserPassword; }
        }
        public string ApplicationSettingApplicationName
        {
            get { return applicationSettingServices.ApplicationName; }
        }
        public string ApplicationSettingFooterTradeMark
        {
            get { return applicationSettingServices.FooterTradeMark; }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (applicationUserManager != null)
                {
                    applicationUserManager.Dispose();
                    applicationUserManager = null;
                }

                if (applicationSignInManager != null)
                {
                    applicationSignInManager.Dispose();
                    applicationSignInManager = null;
                }

                if (applicationRoleManager != null)
                {
                    applicationRoleManager.Dispose();
                    applicationRoleManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
