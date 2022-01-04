namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ok : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItems_Details", "OldPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.FoodItems_Details", "NewPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItems_Details", "NewPrice");
            DropColumn("dbo.FoodItems_Details", "OldPrice");
        }
    }
}
