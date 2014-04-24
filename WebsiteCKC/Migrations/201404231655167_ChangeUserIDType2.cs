namespace WebsiteCKC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIDType2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Competitions", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Competitions", "UserID");
        }
    }
}
