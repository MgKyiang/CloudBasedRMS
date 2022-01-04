namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class today : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.KOTPickUp", "KOTStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KOTPickUp", "KOTStatus", c => c.String());
        }
    }
}
