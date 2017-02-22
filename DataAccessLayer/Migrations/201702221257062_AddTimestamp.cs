namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CreateTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreateTimestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CreateTimestamp");
            DropColumn("dbo.Posts", "CreateTimestamp");
        }
    }
}
