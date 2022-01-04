namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderMaster", "OrderStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderMaster", "OrderStatus");
        }
    }
}
