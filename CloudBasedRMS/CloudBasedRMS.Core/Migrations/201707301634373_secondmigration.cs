namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersMember", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.UsersMember", new[] { "EmployeeID" });
            AddColumn("dbo.UsersMember", "UserInMemberID", c => c.String());
            DropColumn("dbo.UsersMember", "EmployeeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UsersMember", "EmployeeID", c => c.String(maxLength: 128));
            DropColumn("dbo.UsersMember", "UserInMemberID");
            CreateIndex("dbo.UsersMember", "EmployeeID");
            AddForeignKey("dbo.UsersMember", "EmployeeID", "dbo.Employee", "EmployeeID");
        }
    }
}
