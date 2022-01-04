namespace CloudBasedRMS.Core.SeedConfig
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Linq;
    public static class IdentitySeed
    {
        public static void SeedData(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!(context.Users.Any(x => x.UserName == "admin")))
            {
                var adminUser = new ApplicationUser { UserName = "admin", Email = "akog02@gmail.com",FullName="System Administrator",NRCNo="12/Nataka(N)162219",Designation="System Admin",Telephone="09256275319",Fax= "09256275319",PasswordQuestion="whoami" };
                adminUser.Active = true;
                //create the admin user [UserObject,password]
                userManager.Create(adminUser, "akog02");
            }

            if (!(context.Users.Any(x => x.UserName == "employee")))
            {
                var employeeuser = new ApplicationUser { UserName = "employee", Email = "employee@gmail.com", FullName = "employee User", NRCNo = "12/Nataka(N)92929929", Designation = "employee user", Telephone = "09256275319", Fax = "09256275319", PasswordQuestion = "whereareyou" };
                employeeuser.Active = true;
                //create the employee user [userObject,password]
                userManager.Create(employeeuser, "employee");
            }
            if (!(context.Users.Any(x => x.UserName == "customer")))
            {
                var customeruser = new ApplicationUser { UserName = "customer", Email = "customer@gmail.com", FullName = "Customer User", NRCNo = "12/Nataka(N)92929929", Designation = "Customer user", Telephone = "09256275319", Fax = "09256275319", PasswordQuestion = "iamcustomer" };
                customeruser.Active = true;
                //create the user [userObject,password]
                userManager.Create(customeruser, "customer");
            }
            var _adminUser = userManager.FindByName("admin");
            var _employeeuser = userManager.FindByName("employee");
            var _customeruser = userManager.FindByName("customer");
            //Create the Admin Role
            if (!(context.Roles.Any(x => x.Name == "admin")))
            {
                var adminRole = new ApplicationRole();
                adminRole.Name = "admin";
                adminRole.CreatedDate = DateTime.Now;
                adminRole.IsBuildIn = true;

                roleManager.Create(adminRole);
            }
            //Create the Employee Role
            if (!(context.Roles.Any(x => x.Name == "employee")))
            {
                var userRole = new ApplicationRole();
                userRole.Name = "employee";
                userRole.IsBuildIn = true;
                userRole.CreatedDate = DateTime.Now;

                roleManager.Create(userRole);
            }
            //Create the Customer Role
            if (!(context.Roles.Any(x => x.Name == "customer")))
            {
                var customerRole = new ApplicationRole();
                customerRole.Name = "customer";
                customerRole.IsBuildIn = true;
                customerRole.CreatedDate = DateTime.Now;

                roleManager.Create(customerRole);
            }
            //find the roles
            var _adminRole = roleManager.FindByName("admin");
            var _employeeRole = roleManager.FindByName("employee");
            var _customerRole = roleManager.FindByName("customer");
            //Create the AspNetRoles
            userManager.AddToRole(_adminUser.Id, _adminRole.Name);
            userManager.AddToRole(_employeeuser.Id, _employeeRole.Name);
            userManager.AddToRole(_customeruser.Id, _customerRole.Name);

            var adminUserInAdminRole = new UserInRole()
            {
                UserInRoleID = Guid.NewGuid().ToString(),
                UserID = _adminUser.Id,
                RoleID = _adminRole.Id,
                CreatedDate = DateTime.Now,
                CreatedUserID = _adminUser.Id,
                Active = true
            };

            var userInUserRole = new UserInRole()
            {
                UserInRoleID = Guid.NewGuid().ToString(),
                UserID = _employeeuser.Id,
                RoleID = _employeeRole.Id,
                CreatedDate = DateTime.Now,
                CreatedUserID = _adminUser.Id,
                Active = true
            };

            context.UserInRole.Add(adminUserInAdminRole);
            context.UserInRole.Add(userInUserRole);
            context.SaveChanges();
        }
    }
}
