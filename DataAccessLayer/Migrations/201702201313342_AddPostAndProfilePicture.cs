namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostAndProfilePicture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        HasAttachment = c.Boolean(nullable: false),
                        Attachment = c.Binary(),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropColumn("dbo.AspNetUsers", "ProfilePicture");
            DropTable("dbo.Posts");
        }
    }
}
