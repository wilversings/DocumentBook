namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        MimeType = c.String(),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "Attachment_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Attachment_Id");
            AddForeignKey("dbo.Posts", "Attachment_Id", "dbo.Files", "Id");
            DropColumn("dbo.Posts", "HasAttachment");
            DropColumn("dbo.Posts", "Attachment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Attachment", c => c.Binary());
            AddColumn("dbo.Posts", "HasAttachment", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Posts", "Attachment_Id", "dbo.Files");
            DropIndex("dbo.Posts", new[] { "Attachment_Id" });
            DropColumn("dbo.Posts", "Attachment_Id");
            DropTable("dbo.Files");
        }
    }
}
