using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteCKC.Models
{
    public class MatchResult
    {
        public int MatchID { get; set; }
        public int CompetitionID { get; set; }
        public int HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamScored { get; set; }
        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayTeamScored { get; set; }
        public DateTime MatchPlayed { get; set; }

    }
}