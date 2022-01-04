namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItems_Details", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItems_Details", "ImagePath");
        }
    }
}
