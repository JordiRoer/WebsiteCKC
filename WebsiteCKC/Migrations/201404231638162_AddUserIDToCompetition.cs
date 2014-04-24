namespace WebsiteCKC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIDToCompetition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Competitions", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Competitions", "UserID");
        }
    }
}
