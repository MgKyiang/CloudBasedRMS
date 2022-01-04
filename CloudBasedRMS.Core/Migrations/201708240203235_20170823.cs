namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170823 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderMaster", "IsBillPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderMaster", "IsBillPaid");
        }
    }
}
