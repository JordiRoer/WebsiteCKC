using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteCKC.Models
{
    public class StatisticsCenter
    {
        public List<TeamStats> SortTeamsByPoint ( List<Team> teams )
        {
            List<TeamStats> teamstats = new List<TeamStats>();
            foreach(Team team in teams)
            {
                TeamStats stats = new TeamStats();
                stats.TeamID = team.ID;
                stats.TeamName = team.ClubName + " " + team.TeamNumber;
                stats.MatchesPlayed = 0;
                stats.Points = 0;

                teamstats.Add(stats);
            }
            teamstats.OrderBy(o => o.Points);

            int position = 1;
            foreach( TeamStats teamstatistics in teamstats)
            {
                teamstatistics.Position = position;
                position++;
            }

            return teamstats;
        }
    }
}