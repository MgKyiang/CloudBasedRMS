namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kitchen",
                c => new
                    {
                        KitchenID = c.String(nullable: false, maxLength: 128),
                        KitchenName = c.String(),
                        KitchenDescription = c.String(),
                        EmployeeID = c.String(maxLength: 128),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.KitchenID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.EmployeeID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            AddColumn("dbo.FoodItems_Details", "KitchenID", c => c.String(maxLength: 128));
            CreateIndex("dbo.FoodItems_Details", "KitchenID");
            AddForeignKey("dbo.FoodItems_Details", "KitchenID", "dbo.Kitchen", "KitchenID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodItems_Details", "KitchenID", "dbo.Kitchen");
            DropForeignKey("dbo.Kitchen", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Kitchen", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Kitchen", "CreatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Kitchen", new[] { "UpdatedUserID" });
            DropIndex("dbo.Kitchen", new[] { "CreatedUserID" });
            DropIndex("dbo.Kitchen", new[] { "EmployeeID" });
            DropIndex("dbo.FoodItems_Details", new[] { "KitchenID" });
            DropColumn("dbo.FoodItems_Details", "KitchenID");
            DropTable("dbo.Kitchen");
        }
    }
}
