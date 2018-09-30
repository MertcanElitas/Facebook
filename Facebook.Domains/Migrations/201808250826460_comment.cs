namespace Facebook.Domains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Post");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Comments", "PostId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Comments", "PostId", "dbo.Post", "Id", cascadeDelete: false);
        }
    }
}
