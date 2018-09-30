namespace Facebook.Domains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "User_Id", newName: "Users_Id");
            RenameIndex(table: "dbo.Users", name: "IX_User_Id", newName: "IX_Users_Id");
            AddColumn("dbo.UserFriend", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserFriend", "Active");
            RenameIndex(table: "dbo.Users", name: "IX_Users_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Users", name: "Users_Id", newName: "User_Id");
        }
    }
}
