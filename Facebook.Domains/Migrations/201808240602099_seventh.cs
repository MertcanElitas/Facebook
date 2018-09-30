namespace Facebook.Domains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seventh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "ParentId", c => c.Int(nullable: false));
        }
    }
}
