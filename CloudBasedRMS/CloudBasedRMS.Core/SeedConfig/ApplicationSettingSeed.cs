namespace CloudBasedRMS.Core.SeedConfig
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    public static class ApplicationSettingSeed
    {
        public static void SeedData(ApplicationDbContext dbContext)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(dbContext));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));

            var _adminRole = roleManager.FindByName("admin");
            var _userRole = roleManager.FindByName("Employee");
            var _adminUser = userManager.FindByName("admin");
            var _user = userManager.FindByName("user");

            List<ApplicationSetting> entities = new List<ApplicationSetting>()
            {
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "PagingSize", Value = "10", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "DateFormat", Value = "dd/MM/yyyy", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "TimeFormat", Value = "hh:mm tt", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "ApplicationName", Value = "Cloud Based Restaurent Management System", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "ApplicationVersion", Value = "CloudBasedRMSV0.1", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "FooterTradeMark", Value = "Copyright &copy; CloudBasedRMS 2012-2017.", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "DefaultUserPassword", Value = "user", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "IsWebConfigEncrypt", Value = "False", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "ExpiredDays", Value = "30", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  },
                new ApplicationSetting { ApplicationSettingID=Guid.NewGuid().ToString(), Key = "IsExpired", Value = "False", CreatedUserID =_adminUser.Id , CreatedDate = DateTime.Now , Active = true  }
            };

            dbContext.ApplicationSettings.AddRange(entities);
            dbContext.SaveChanges();
        }
    }
}
