namespace WebsiteCKC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIDType1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Competitions", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Competitions", "UserID", c => c.Int(nullable: false));
        }
    }
}
