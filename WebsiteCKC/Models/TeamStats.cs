using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteCKC.Models
{
    public class TeamStats
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int Position { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesTied { get; set; }
        public int MatchesLost { get; set; }
        public int Points { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalsAverage { get; set; }
        public int PointsLost { get; set; }

    }
}