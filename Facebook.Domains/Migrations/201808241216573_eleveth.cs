namespace Facebook.Domains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eleveth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Post", "UserId", "dbo.Users");
            DropIndex("dbo.Post", new[] { "UserId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Post", "UserId");
            AddForeignKey("dbo.Post", "UserId", "dbo.Users", "Id", cascadeDelete: false);
        }
    }
}
