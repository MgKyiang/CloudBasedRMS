using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
namespace CloudBasedRMS.Core.SeedConfig
{
    public static  class APIAuthorizationSeedData
    {
        public static void SeedData(ApplicationDbContext dbContext)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(dbContext));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            var _adminRole = roleManager.FindByName("admin");
            var _userRole = roleManager.FindByName("employee");
            var _adminUser = userManager.FindByName("admin");
            var _user = userManager.FindByName("employee");
            List<APIAuthorization> entities = new List<APIAuthorization>()
            {
             new APIAuthorization { APIAuthorizationID=Guid.NewGuid().ToString(),ControllerName= "ApiAuthorization",ActionName= "GetByAll",TransactionLog=true,AllowOrDeny=true,Active=true,CreatedDate=DateTime.Now,CreatedUserID=_adminUser.Id, RoleID=_adminRole.Id } ,
             new APIAuthorization { APIAuthorizationID=Guid.NewGuid().ToString(),ControllerName= "ApiAuthorization",ActionName= "GetApiData",TransactionLog=true,AllowOrDeny=true,Active=true,CreatedDate=DateTime.Now,CreatedUserID=_adminUser.Id, RoleID=_adminRole.Id },
             new APIAuthorization { APIAuthorizationID=Guid.NewGuid().ToString(),ControllerName= "ApiAuthorization",ActionName= "PostAutoCreateApi",TransactionLog=true,AllowOrDeny=true,Active=true,CreatedDate=DateTime.Now,CreatedUserID=_adminUser.Id, RoleID=_adminRole.Id },
             new APIAuthorization { APIAuthorizationID=Guid.NewGuid().ToString(),ControllerName= "ApiAuthorization",ActionName= "PostManuallyCreateApi",TransactionLog=true,AllowOrDeny=true,Active=true,CreatedDate=DateTime.Now,CreatedUserID=_adminUser.Id, RoleID=_adminRole.Id },
             new APIAuthorization { APIAuthorizationID=Guid.NewGuid().ToString(),ControllerName= "ApiAuthorization",ActionName= "GetRoles",TransactionLog=true,AllowOrDeny=true,Active=true,CreatedDate=DateTime.Now,CreatedUserID=_adminUser.Id, RoleID=_adminRole.Id },
             new APIAuthorization { APIAuthorizationID=Guid.NewGuid().ToString(),ControllerName= "ApiAuthorization",ActionName= "Post",TransactionLog=true,AllowOrDeny=true,Active=true,CreatedDate=DateTime.Now,CreatedUserID=_adminUser.Id, RoleID=_adminRole.Id },
             new APIAuthorization { APIAuthorizationID=Guid.NewGuid().ToString(),ControllerName= "ApiAuthorization",ActionName= "DeleteById",TransactionLog=true,AllowOrDeny=true,Active=true,CreatedDate=DateTime.Now,CreatedUserID=_adminUser.Id, RoleID=_adminRole.Id }
            };
            dbContext.APIAuthorization.AddRange(entities);
            dbContext.SaveChanges();
        }
    }
}
