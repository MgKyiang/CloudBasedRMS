namespace CloudBasedRMS.Core.SeedConfig
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    public static class Authorizationseed
    {
        public static void SeedData(ApplicationDbContext dbContext)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(dbContext));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            //get the admin role
            var _adminRole = roleManager.FindByName("admin");
            //get [employee role]
            var _employeeRole = roleManager.FindByName("employee");
            //get [Customer  role]
            var _CustomerRole = roleManager.FindByName("customer");
            //get the admin user name
            var _adminUser = userManager.FindByName("admin");
            //get the employee user name
            var _employeeuser = userManager.FindByName("employee");

            //Administration
            //Auhoorize In Controller
            var authorizationslist = new List<Authorizations>()
            {
                new Authorizations { AuthorizationsID =Guid.NewGuid().ToString(),ControllerName ="Authorizations", ActionName="Index", RoleID= _adminRole.Id,  IsAllow = true , CreatedDate = DateTime.Now , CreatedUserID = _adminUser.Id , Active = true  },
                new Authorizations { AuthorizationsID =Guid.NewGuid().ToString(),ControllerName ="Authorizations", ActionName="Create", RoleID= _adminRole.Id,   IsAllow = true , CreatedDate = DateTime.Now , CreatedUserID = _adminUser.Id , Active = true  },
                new Authorizations { AuthorizationsID =Guid.NewGuid().ToString(),ControllerName ="Authorizations", ActionName="Edit", RoleID= _adminRole.Id,   IsAllow = true , CreatedDate = DateTime.Now , CreatedUserID = _adminUser.Id , Active = true  },
                new Authorizations { AuthorizationsID =Guid.NewGuid().ToString(),ControllerName ="Authorizations", ActionName="Delete",RoleID= _adminRole.Id,  IsAllow = true , CreatedDate = DateTime.Now , CreatedUserID = _adminUser.Id , Active = true  },
                new Authorizations { AuthorizationsID =Guid.NewGuid().ToString(),ControllerName ="Authorizations", ActionName="Add",RoleID= _adminRole.Id,  IsAllow = true , CreatedDate = DateTime.Now , CreatedUserID = _adminUser.Id , Active = true  },
                new Authorizations { AuthorizationsID =Guid.NewGuid().ToString(),ControllerName ="Roles", ActionName="Index",RoleID= _adminRole.Id,  IsAllow = true , CreatedDate = DateTime.Now , CreatedUserID = _adminUser.Id , Active = true  },
                new Authorizations { AuthorizationsID =Guid.NewGuid().ToString(),ControllerName ="AppSetting", ActionName="Index",RoleID= _adminRole.Id,  IsAllow = true , CreatedDate = DateTime.Now , CreatedUserID = _adminUser.Id , Active = true,},
                new Authorizations { AuthorizationsID =Guid.NewGuid().ToString(),ControllerName ="Dashboard", ActionName="Index",RoleID= _adminRole.Id,  IsAllow = true , CreatedDate = DateTime.Now , CreatedUserID = _adminUser.Id , Active = true,}
            };
            //Add to ApplicationDbContext
            authorizationslist.ForEach(authori => dbContext.Authorizations.AddOrUpdate(p => p.AuthorizationsID, authori));
            dbContext.SaveChanges();
          
        }
    }
}
