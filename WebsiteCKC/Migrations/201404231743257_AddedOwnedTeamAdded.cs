namespace WebsiteCKC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOwnedTeamAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OwnedTeams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamID = c.Int(nullable: false),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OwnedTeams");
        }
    }
}
