namespace Facebook.Domains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromId = c.Int(nullable: false),
                        ToId = c.Int(nullable: false),
                        Datasent = c.DateTime(nullable: false),
                        message = c.String(maxLength: 500),
                        Read = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ToId, cascadeDelete: false)
                .Index(t => t.FromId)
                .Index(t => t.ToId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ToId", "dbo.Users");
            DropForeignKey("dbo.Messages", "FromId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "ToId" });
            DropIndex("dbo.Messages", new[] { "FromId" });
            DropTable("dbo.Messages");
        }
    }
}
