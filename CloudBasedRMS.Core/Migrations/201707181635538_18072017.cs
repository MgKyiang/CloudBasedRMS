namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18072017 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 500),
                        Email = c.String(),
                        AddressID = c.String(maxLength: 128),
                        PhoneNo = c.Int(nullable: false),
                        MobileNo = c.Int(nullable: false),
                        Note = c.String(maxLength: 500),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Address", t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.AddressID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customer", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customer", "AddressID", "dbo.Address");
            DropIndex("dbo.Customer", new[] { "UpdatedUserID" });
            DropIndex("dbo.Customer", new[] { "CreatedUserID" });
            DropIndex("dbo.Customer", new[] { "AddressID" });
            DropTable("dbo.Customer");
        }
    }
}
