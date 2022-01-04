namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsersMember", "EmployeeID", c => c.String(maxLength: 128));
            CreateIndex("dbo.UsersMember", "EmployeeID");
            AddForeignKey("dbo.UsersMember", "EmployeeID", "dbo.Employee", "EmployeeID");
            DropColumn("dbo.UsersMember", "UserInMemberID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UsersMember", "UserInMemberID", c => c.String());
            DropForeignKey("dbo.UsersMember", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.UsersMember", new[] { "EmployeeID" });
            DropColumn("dbo.UsersMember", "EmployeeID");
        }
    }
}
