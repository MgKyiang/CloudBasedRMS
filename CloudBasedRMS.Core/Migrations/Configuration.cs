namespace CloudBasedRMS.Core.Migrations
{
    using CloudBasedRMS.Core.SeedConfig;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CloudBasedRMS.Core.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CloudBasedRMS.Core.ApplicationDbContext context)
        {
            //Default Admin/User Account and Roles
            IdentitySeed.SeedData(context);
            Authorizationseed.SeedData(context);
            //Default ApplicationSetting Data
            ApplicationSettingSeed.SeedData(context);
        }
    }
}
