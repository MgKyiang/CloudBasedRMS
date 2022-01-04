namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        ContactUsID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Company = c.String(),
                        WebSite = c.String(),
                        Message = c.String(),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ContactUsID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactUs", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactUs", "CreatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.ContactUs", new[] { "UpdatedUserID" });
            DropIndex("dbo.ContactUs", new[] { "CreatedUserID" });
            DropTable("dbo.ContactUs");
        }
    }
}
