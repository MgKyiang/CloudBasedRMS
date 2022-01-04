namespace CloudBasedRMS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ok : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "Title", c => c.String());
            DropColumn("dbo.Event", "Code");
            DropColumn("dbo.Event", "Subject");
            DropColumn("dbo.Event", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "Description", c => c.String());
            AddColumn("dbo.Event", "Subject", c => c.String());
            AddColumn("dbo.Event", "Code", c => c.String());
            DropColumn("dbo.Event", "Title");
        }
    }
}
