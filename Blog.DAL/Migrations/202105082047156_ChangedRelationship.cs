namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRelationship : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ClientProfiles", newName: "UserProfiles");
            DropForeignKey("dbo.Comments", "ClientProfile_Id", "dbo.ClientProfiles");
            DropIndex("dbo.Comments", new[] { "ClientProfile_Id" });
            RenameColumn(table: "dbo.Comments", name: "ClientProfile_Id", newName: "UserProfileId");
            RenameColumn(table: "dbo.Posts", name: "ClientProfile_Id", newName: "UserProfileId");
            RenameIndex(table: "dbo.Posts", name: "IX_ClientProfile_Id", newName: "IX_UserProfileId");
            AddColumn("dbo.Comments", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "PostId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "CreateAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "UserProfileId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Comments", "UserProfileId");
            CreateIndex("dbo.Comments", "PostId");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserProfileId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            DropColumn("dbo.Comments", "Date");
            DropColumn("dbo.Posts", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "Date", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Comments", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "UserProfileId" });
            AlterColumn("dbo.Comments", "UserProfileId", c => c.String(maxLength: 128));
            DropColumn("dbo.Posts", "CreateAt");
            DropColumn("dbo.Comments", "PostId");
            DropColumn("dbo.Comments", "CreateAt");
            RenameIndex(table: "dbo.Posts", name: "IX_UserProfileId", newName: "IX_ClientProfile_Id");
            RenameColumn(table: "dbo.Posts", name: "UserProfileId", newName: "ClientProfile_Id");
            RenameColumn(table: "dbo.Comments", name: "UserProfileId", newName: "ClientProfile_Id");
            CreateIndex("dbo.Comments", "ClientProfile_Id");
            AddForeignKey("dbo.Comments", "ClientProfile_Id", "dbo.ClientProfiles", "Id");
            RenameTable(name: "dbo.UserProfiles", newName: "ClientProfiles");
        }
    }
}
