using CloudBasedRMS.Core;
using CloudBasedRMS.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CloudBasedRMS.WebAPI.Common
{
    [APIAuthorizeBase]
    public  class APIControllerBase : ApiController
    {

        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;
        private ApplicationSignInManager _applicationSignInManager;
        private ApplicationSettingServices _applicationSettingServices;

        protected ApplicationSettingServices ApplicationSettings
        {
            get
            {
                return _applicationSettingServices = new ApplicationSettingServices();
            }
            private set
            {
                _applicationSettingServices = value;
            }
        }
        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return _applicationSignInManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _applicationSignInManager = value;
            }
        }
        protected ApplicationUserManager UserManager
        {
            get
            {
                return _applicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set { _applicationUserManager = value; }

        }
        protected ApplicationRoleManager RoleManager
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
        protected ApplicationUser CurrentUser
        {
            get
            {
                var userName = User.Identity.Name;

                return UserManager.FindByName(userName);
            }
        }
    }
}