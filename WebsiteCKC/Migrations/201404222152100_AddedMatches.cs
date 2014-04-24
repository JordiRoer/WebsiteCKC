namespace WebsiteCKC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMatches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        MatchedPlayed = c.DateTime(nullable: false),
                        HomeTeamID = c.Int(nullable: false),
                        HomeTeamScored = c.Int(nullable: false),
                        AwayTeamID = c.Int(nullable: false),
                        AwayTeamScored = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Matches");
        }
    }
}
