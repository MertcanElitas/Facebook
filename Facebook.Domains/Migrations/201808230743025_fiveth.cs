namespace Facebook.Domains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fiveth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Walls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        DateEdited = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Walls", "UserId", "dbo.Users");
            DropIndex("dbo.Walls", new[] { "UserId" });
            DropTable("dbo.Walls");
        }
    }
}
