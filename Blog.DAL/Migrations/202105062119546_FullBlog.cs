namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullBlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ClientProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientProfiles", t => t.ClientProfile_Id)
                .Index(t => t.ClientProfile_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ClientProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientProfiles", t => t.ClientProfile_Id)
                .Index(t => t.ClientProfile_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Post_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Post_Id);
            
            AddColumn("dbo.ClientProfiles", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ClientProfile_Id", "dbo.ClientProfiles");
            DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Comments", "ClientProfile_Id", "dbo.ClientProfiles");
            DropIndex("dbo.TagPosts", new[] { "Post_Id" });
            DropIndex("dbo.TagPosts", new[] { "Tag_Id" });
            DropIndex("dbo.Posts", new[] { "ClientProfile_Id" });
            DropIndex("dbo.Comments", new[] { "ClientProfile_Id" });
            DropColumn("dbo.ClientProfiles", "IsDeleted");
            DropTable("dbo.TagPosts");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
