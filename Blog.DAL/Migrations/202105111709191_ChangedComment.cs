namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "UserEmail");
        }
    }
}
