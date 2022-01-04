namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class erdchange : DbMigration
    {
        public override void Up()
        {

          
            
            CreateTable(
                "dbo.KOTPickUp",
                c => new
                    {
                        KOTPickUpID = c.String(nullable: false, maxLength: 128),
                        OrderMasterID = c.String(maxLength: 128),
                        OrderItemsID = c.String(maxLength: 128),
                        IsReadyPickup = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KOTPickUpID)
                .ForeignKey("dbo.OrderItems", t => t.OrderItemsID)
                .ForeignKey("dbo.OrderMaster", t => t.OrderMasterID)
                .Index(t => t.OrderMasterID)
                .Index(t => t.OrderItemsID);
            
         
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicle", "VehicleTypeID", "dbo.VehicleType");
            DropForeignKey("dbo.VehicleType", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.VehicleType", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vehicle", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vehicle", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersMember", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersMember", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersMember", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInRole", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInRole", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInRole", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInRole", "RoleID", "dbo.AspNetRoles");
            DropForeignKey("dbo.TransactionLog", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransactionLog", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Supplier", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Supplier", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Supplier", "AddressID", "dbo.Address");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RestaurantProfile", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.RestaurantProfile", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.KOTPickUp", "OrderMasterID", "dbo.OrderMaster");
            DropForeignKey("dbo.KOTPickUp", "OrderItemsID", "dbo.OrderItems");
            DropForeignKey("dbo.Event", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Event", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ErrorLog", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ErrorLog", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.BillFoodItems", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.BillFoodItems", "SaleBillID", "dbo.SaleBill");
            DropForeignKey("dbo.SaleBill", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaleBill", "OrderMasterID", "dbo.OrderMaster");
            DropForeignKey("dbo.OrderMaster", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderMaster", "TableID", "dbo.Tables");
            DropForeignKey("dbo.Tables", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tables", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Tables", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "OrderMasterID", "dbo.OrderMaster");
            DropForeignKey("dbo.OrderItems", "FoodItemID", "dbo.FoodItems_Details");
            DropForeignKey("dbo.OrderItems", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderMaster", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaleBill", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.SaleBill", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Customer", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customer", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customer", "AddressID", "dbo.Address");
            DropForeignKey("dbo.SaleBill", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.BillFoodItems", "FoodItemID", "dbo.FoodItems_Details");
            DropForeignKey("dbo.FoodItems_Details", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FoodItems_Details", "KitchenID", "dbo.Kitchen");
            DropForeignKey("dbo.Kitchen", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Kitchen", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Employee", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employee", "RankID", "dbo.Rank");
            DropForeignKey("dbo.Rank", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rank", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employee", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employee", "AddressID", "dbo.Address");
            DropForeignKey("dbo.Kitchen", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FoodItems_Details", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FoodItems_Details", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Category", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Category", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.BillFoodItems", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Authorizations", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Authorizations", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Authorizations", "RoleID", "dbo.AspNetRoles");
            DropForeignKey("dbo.ApplicationSetting", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationSetting", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.APIAuthorization", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.APIAuthorization", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.APIAuthorization", "RoleID", "dbo.AspNetRoles");
            DropForeignKey("dbo.Address", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Address", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.VehicleType", new[] { "UpdatedUserID" });
            DropIndex("dbo.VehicleType", new[] { "CreatedUserID" });
            DropIndex("dbo.Vehicle", new[] { "UpdatedUserID" });
            DropIndex("dbo.Vehicle", new[] { "CreatedUserID" });
            DropIndex("dbo.Vehicle", new[] { "VehicleTypeID" });
            DropIndex("dbo.UsersMember", new[] { "UpdatedUserID" });
            DropIndex("dbo.UsersMember", new[] { "CreatedUserID" });
            DropIndex("dbo.UsersMember", new[] { "UserID" });
            DropIndex("dbo.UserInRole", new[] { "UpdatedUserID" });
            DropIndex("dbo.UserInRole", new[] { "CreatedUserID" });
            DropIndex("dbo.UserInRole", new[] { "RoleID" });
            DropIndex("dbo.UserInRole", new[] { "UserID" });
            DropIndex("dbo.TransactionLog", new[] { "UpdatedUserID" });
            DropIndex("dbo.TransactionLog", new[] { "CreatedUserID" });
            DropIndex("dbo.Supplier", new[] { "UpdatedUserID" });
            DropIndex("dbo.Supplier", new[] { "CreatedUserID" });
            DropIndex("dbo.Supplier", new[] { "AddressID" });
            DropIndex("dbo.RestaurantProfile", new[] { "UpdatedUserID" });
            DropIndex("dbo.RestaurantProfile", new[] { "CreatedUserID" });
            DropIndex("dbo.KOTPickUp", new[] { "OrderItemsID" });
            DropIndex("dbo.KOTPickUp", new[] { "OrderMasterID" });
            DropIndex("dbo.Event", new[] { "UpdatedUserID" });
            DropIndex("dbo.Event", new[] { "CreatedUserID" });
            DropIndex("dbo.ErrorLog", new[] { "UpdatedUserID" });
            DropIndex("dbo.ErrorLog", new[] { "CreatedUserID" });
            DropIndex("dbo.Tables", new[] { "UpdatedUserID" });
            DropIndex("dbo.Tables", new[] { "CreatedUserID" });
            DropIndex("dbo.Tables", new[] { "EmployeeID" });
            DropIndex("dbo.OrderItems", new[] { "UpdatedUserID" });
            DropIndex("dbo.OrderItems", new[] { "CreatedUserID" });
            DropIndex("dbo.OrderItems", new[] { "FoodItemID" });
            DropIndex("dbo.OrderItems", new[] { "OrderMasterID" });
            DropIndex("dbo.OrderMaster", new[] { "UpdatedUserID" });
            DropIndex("dbo.OrderMaster", new[] { "CreatedUserID" });
            DropIndex("dbo.OrderMaster", new[] { "TableID" });
            DropIndex("dbo.Customer", new[] { "UpdatedUserID" });
            DropIndex("dbo.Customer", new[] { "CreatedUserID" });
            DropIndex("dbo.Customer", new[] { "AddressID" });
            DropIndex("dbo.SaleBill", new[] { "UpdatedUserID" });
            DropIndex("dbo.SaleBill", new[] { "CreatedUserID" });
            DropIndex("dbo.SaleBill", new[] { "CustomerID" });
            DropIndex("dbo.SaleBill", new[] { "OrderMasterID" });
            DropIndex("dbo.SaleBill", new[] { "EmployeeID" });
            DropIndex("dbo.Rank", new[] { "UpdatedUserID" });
            DropIndex("dbo.Rank", new[] { "CreatedUserID" });
            DropIndex("dbo.Employee", new[] { "UpdatedUserID" });
            DropIndex("dbo.Employee", new[] { "CreatedUserID" });
            DropIndex("dbo.Employee", new[] { "AddressID" });
            DropIndex("dbo.Employee", new[] { "RankID" });
            DropIndex("dbo.Kitchen", new[] { "UpdatedUserID" });
            DropIndex("dbo.Kitchen", new[] { "CreatedUserID" });
            DropIndex("dbo.Kitchen", new[] { "EmployeeID" });
            DropIndex("dbo.Category", new[] { "UpdatedUserID" });
            DropIndex("dbo.Category", new[] { "CreatedUserID" });
            DropIndex("dbo.FoodItems_Details", new[] { "UpdatedUserID" });
            DropIndex("dbo.FoodItems_Details", new[] { "CreatedUserID" });
            DropIndex("dbo.FoodItems_Details", new[] { "KitchenID" });
            DropIndex("dbo.FoodItems_Details", new[] { "CategoryID" });
            DropIndex("dbo.BillFoodItems", new[] { "UpdatedUserID" });
            DropIndex("dbo.BillFoodItems", new[] { "CreatedUserID" });
            DropIndex("dbo.BillFoodItems", new[] { "FoodItemID" });
            DropIndex("dbo.BillFoodItems", new[] { "SaleBillID" });
            DropIndex("dbo.Authorizations", new[] { "UpdatedUserID" });
            DropIndex("dbo.Authorizations", new[] { "CreatedUserID" });
            DropIndex("dbo.Authorizations", new[] { "RoleID" });
            DropIndex("dbo.ApplicationSetting", new[] { "UpdatedUserID" });
            DropIndex("dbo.ApplicationSetting", new[] { "CreatedUserID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.APIAuthorization", new[] { "UpdatedUserID" });
            DropIndex("dbo.APIAuthorization", new[] { "CreatedUserID" });
            DropIndex("dbo.APIAuthorization", new[] { "RoleID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Address", new[] { "UpdatedUserID" });
            DropIndex("dbo.Address", new[] { "CreatedUserID" });
            DropTable("dbo.VehicleType");
            DropTable("dbo.Vehicle");
            DropTable("dbo.UsersMember");
            DropTable("dbo.UserInRole");
            DropTable("dbo.TransactionLog");
            DropTable("dbo.Supplier");
            DropTable("dbo.RestaurantProfile");
            DropTable("dbo.KOTPickUp");
            DropTable("dbo.Event");
            DropTable("dbo.ErrorLog");
            DropTable("dbo.ContactUs");
            DropTable("dbo.Tables");
            DropTable("dbo.OrderItems");
            DropTable("dbo.OrderMaster");
            DropTable("dbo.Customer");
            DropTable("dbo.SaleBill");
            DropTable("dbo.Rank");
            DropTable("dbo.Employee");
            DropTable("dbo.Kitchen");
            DropTable("dbo.Category");
            DropTable("dbo.FoodItems_Details");
            DropTable("dbo.BillFoodItems");
            DropTable("dbo.Authorizations");
            DropTable("dbo.ApplicationSetting");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.APIAuthorization");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Address");
        }
    }
}
