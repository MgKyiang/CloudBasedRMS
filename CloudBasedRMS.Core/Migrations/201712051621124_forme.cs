namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressID = c.String(nullable: false, maxLength: 128),
                        City = c.String(),
                        Township = c.String(),
                        Place = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Area = c.String(),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        NRCNo = c.String(),
                        Designation = c.String(),
                        Telephone = c.String(),
                        Fax = c.String(),
                        PasswordQuestion = c.String(),
                        PasswordAnswer = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.APIAuthorization",
                c => new
                    {
                        APIAuthorizationID = c.String(nullable: false, maxLength: 128),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        TransactionLog = c.Boolean(nullable: false),
                        AllowOrDeny = c.Boolean(nullable: false),
                        RoleID = c.String(maxLength: 128),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.APIAuthorizationID)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.RoleID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        IsBuildIn = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ApplicationSetting",
                c => new
                    {
                        ApplicationSettingID = c.String(nullable: false, maxLength: 128),
                        Key = c.String(nullable: false, maxLength: 500),
                        Value = c.String(nullable: false, maxLength: 4000),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ApplicationSettingID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.Authorizations",
                c => new
                    {
                        AuthorizationsID = c.String(nullable: false, maxLength: 128),
                        ControllerName = c.String(nullable: false, maxLength: 100),
                        ActionName = c.String(nullable: false, maxLength: 100),
                        IsAllow = c.Boolean(nullable: false),
                        RoleID = c.String(maxLength: 128),
                        IsUseLog = c.Boolean(nullable: false),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.AuthorizationsID)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.RoleID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.BillFoodItems",
                c => new
                    {
                        BillFoodItemsID = c.String(nullable: false, maxLength: 128),
                        SaleBillID = c.String(maxLength: 128),
                        FoodItemID = c.String(maxLength: 128),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RatePerItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.BillFoodItemsID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.FoodItems_Details", t => t.FoodItemID)
                .ForeignKey("dbo.SaleBill", t => t.SaleBillID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.SaleBillID)
                .Index(t => t.FoodItemID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.FoodItems_Details",
                c => new
                    {
                        FoodItemID = c.String(nullable: false, maxLength: 128),
                        Code = c.String(maxLength: 500),
                        Description = c.String(maxLength: 4000),
                        ImagePath = c.String(),
                        CategoryID = c.String(maxLength: 128),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Offer = c.String(maxLength: 20),
                        Note = c.String(maxLength: 4000),
                        IsJam = c.Boolean(nullable: false),
                        Spicy = c.String(maxLength: 6),
                        OldPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NewPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsTodaySpecial = c.Boolean(nullable: false),
                        KitchenID = c.String(maxLength: 128),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.FoodItemID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.Kitchen", t => t.KitchenID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CategoryID)
                .Index(t => t.KitchenID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.String(nullable: false, maxLength: 128),
                        Code = c.String(maxLength: 500),
                        Description = c.String(maxLength: 4000),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
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
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.String(nullable: false, maxLength: 128),
                        EmployeeNo = c.String(),
                        Name = c.String(),
                        ImagePath = c.String(),
                        RankID = c.String(maxLength: 128),
                        WorkType = c.String(),
                        AddressID = c.String(maxLength: 128),
                        PhoneNo = c.String(),
                        MobileNo = c.String(),
                        BasicPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sex = c.String(),
                        NRC = c.String(),
                        DOB = c.DateTime(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Address", t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.Rank", t => t.RankID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.RankID)
                .Index(t => t.AddressID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.Rank",
                c => new
                    {
                        RankID = c.String(nullable: false, maxLength: 128),
                        Code = c.String(),
                        Description = c.String(),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.RankID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.SaleBill",
                c => new
                    {
                        SaleBillID = c.String(nullable: false, maxLength: 128),
                        SaleBillNo = c.String(),
                        SaleBillDate = c.DateTime(nullable: false),
                        EmployeeID = c.String(maxLength: 128),
                        OrderMasterID = c.String(maxLength: 128),
                        CustomerID = c.String(maxLength: 128),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.SaleBillID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .ForeignKey("dbo.OrderMaster", t => t.OrderMasterID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.EmployeeID)
                .Index(t => t.OrderMasterID)
                .Index(t => t.CustomerID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 500),
                        Email = c.String(),
                        AddressID = c.String(maxLength: 128),
                        PhoneNo = c.String(),
                        MobileNo = c.String(),
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
            
            CreateTable(
                "dbo.OrderMaster",
                c => new
                    {
                        OrderMasterID = c.String(nullable: false, maxLength: 128),
                        OrderNo = c.String(),
                        OrderDate = c.DateTime(),
                        Description = c.String(),
                        IsParcel = c.Boolean(nullable: false),
                        TableID = c.String(maxLength: 128),
                        OrderStatus = c.String(),
                        IsBillPaid = c.Boolean(nullable: false),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.OrderMasterID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.Tables", t => t.TableID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.TableID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemsID = c.String(nullable: false, maxLength: 128),
                        OrderMasterID = c.String(maxLength: 128),
                        FoodItemID = c.String(maxLength: 128),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RatePerItems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.OrderItemsID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.FoodItems_Details", t => t.FoodItemID)
                .ForeignKey("dbo.OrderMaster", t => t.OrderMasterID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.OrderMasterID)
                .Index(t => t.FoodItemID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        TableID = c.String(nullable: false, maxLength: 128),
                        TableNo = c.String(),
                        Capacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAvailable = c.Boolean(nullable: false),
                        Status = c.String(),
                        EmployeeID = c.String(maxLength: 128),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TableID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.EmployeeID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
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
                    })
                .PrimaryKey(t => t.ContactUsID);
            
            CreateTable(
                "dbo.ErrorLog",
                c => new
                    {
                        ErrorLogID = c.String(nullable: false, maxLength: 128),
                        Url = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        ErrorMessage = c.String(),
                        IdentityName = c.String(),
                        MachineName = c.String(),
                        ClientMachineName = c.String(),
                        ClientIPAddress = c.String(),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ErrorLogID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventID = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        ThemeColor = c.String(),
                        IsFullDay = c.Boolean(nullable: false),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
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
            
            CreateTable(
                "dbo.RestaurantProfile",
                c => new
                    {
                        RestaurantProfileID = c.String(nullable: false, maxLength: 128),
                        RestaurantName = c.String(),
                        ContactAddress = c.String(),
                        EmailAddress = c.String(),
                        FacebookAddress = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Logo = c.Binary(),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.RestaurantProfileID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        SupplierID = c.String(nullable: false, maxLength: 128),
                        SupplierName = c.String(),
                        Phone = c.String(),
                        AddressID = c.String(maxLength: 128),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.SupplierID)
                .ForeignKey("dbo.Address", t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.AddressID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.TransactionLog",
                c => new
                    {
                        LogID = c.String(nullable: false, maxLength: 128),
                        Url = c.String(),
                        ServerName = c.String(),
                        HostName = c.String(),
                        Port = c.String(),
                        HttpRequestType = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        RawData = c.String(),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.LogID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.UserInRole",
                c => new
                    {
                        UserInRoleID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        RoleID = c.String(maxLength: 128),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.UserInRoleID)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.UserID)
                .Index(t => t.RoleID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.UsersMember",
                c => new
                    {
                        UsersMemberID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        UserInMemberID = c.String(),
                        MemberStatus = c.String(),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.UsersMemberID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.UserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        VehicleID = c.String(nullable: false, maxLength: 128),
                        RegistrationNo = c.String(maxLength: 500),
                        VehicleTypeID = c.String(maxLength: 128),
                        Status = c.String(),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.VehicleID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .ForeignKey("dbo.VehicleType", t => t.VehicleTypeID)
                .Index(t => t.VehicleTypeID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
            CreateTable(
                "dbo.VehicleType",
                c => new
                    {
                        VehicleTypeID = c.String(nullable: false, maxLength: 128),
                        Code = c.String(maxLength: 500),
                        Description = c.String(maxLength: 4000),
                        CreatedUserID = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.VehicleTypeID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID);
            
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
