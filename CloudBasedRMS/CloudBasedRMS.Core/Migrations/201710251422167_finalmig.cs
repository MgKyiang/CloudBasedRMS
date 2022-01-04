namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalmig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactUs", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactUs", "UpdatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.ContactUs", new[] { "CreatedUserID" });
            DropIndex("dbo.ContactUs", new[] { "UpdatedUserID" });
            DropColumn("dbo.ContactUs", "CreatedUserID");
            DropColumn("dbo.ContactUs", "CreatedDate");
            DropColumn("dbo.ContactUs", "UpdatedUserID");
            DropColumn("dbo.ContactUs", "UpdatedDate");
            DropColumn("dbo.ContactUs", "Active");
            DropColumn("dbo.ContactUs", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactUs", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.ContactUs", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ContactUs", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.ContactUs", "UpdatedUserID", c => c.String(maxLength: 128));
            AddColumn("dbo.ContactUs", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContactUs", "CreatedUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.ContactUs", "UpdatedUserID");
            CreateIndex("dbo.ContactUs", "CreatedUserID");
            AddForeignKey("dbo.ContactUs", "UpdatedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ContactUs", "CreatedUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
