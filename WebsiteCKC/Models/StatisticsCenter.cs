using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteCKC.Models
{
    public class StatisticsCenter
    {
        CKCDatabase db = new CKCDatabase();

        public List<TeamStats> SortTeamsByPoint ( List<Team> teams )
        {
            List<TeamStats> teamstats = new List<TeamStats>();
            
            foreach(Team team in teams)
            {
                TeamStats stats = new TeamStats();
                stats.TeamID = team.ID;
                stats.TeamName = team.ClubName + " " + team.TeamNumber;
                stats.MatchesPlayed = this.GetPlayedMatchesCount(team.ID);
                stats.MatchesWon = this.GetWonMatchesCount(team.ID);
                stats.MatchesTied = this.GetMatchesTiedCount(team.ID);
                stats.MatchesLost = this.GetMatchesLostCount(team.ID);
                stats.GoalsScored = this.GetGoalsScoredCount(team.ID);
                stats.GoalsConceded = this.GetGoalsConcededCount(team.ID);
                stats.GoalsAverage = this.GetGoalDifference(stats.GoalsScored, stats.GoalsConceded);
                stats.PointsLost = 0;
                stats.Points = this.GetPoints(stats.MatchesWon, stats.MatchesTied, stats.MatchesLost, stats.PointsLost);

                teamstats.Add(stats);
            }
            List<TeamStats> orderedList = teamstats.OrderByDescending(o => o.Points).ToList();

            int position = 1;
            foreach (TeamStats teamstatistics in orderedList)
            {
                teamstatistics.Position = position;
                position++;
            }

            return orderedList;
        }

        private int GetPlayedMatchesCount(int teamID)
        {
            List<Match> matches = (from m in db.Matches where m.AwayTeamID == teamID || m.HomeTeamID == teamID select m).ToList();

            return matches.Count;
        }

        private int GetWonMatchesCount(int teamID)
        {
            List<Match> matches = (from m in db.Matches where m.AwayTeamID == teamID || m.HomeTeamID == teamID select m).ToList();
            int winCount = 0;

            foreach(Match match in matches)
            {
                if(match.HomeTeamID == teamID)
                {
                    if (match.HomeTeamScored > match.AwayTeamScored)
                        winCount++;
                }

                if (match.AwayTeamID == teamID)
                {
                    if (match.HomeTeamScored < match.AwayTeamScored)
                        winCount++;
                }
            }

            return winCount;
        }

        private int GetMatchesTiedCount(int teamID)
        {
            List<Match> matches = (from m in db.Matches where m.AwayTeamID == teamID || m.HomeTeamID == teamID select m).ToList();

            int tiedCount = 0;

            foreach(Match match in matches)
            {
                if (match.HomeTeamScored == match.AwayTeamScored)
                    tiedCount++;
            }

            return tiedCount;
        }

        private int GetMatchesLostCount(int teamID)
        {
            List<Match> matches = (from m in db.Matches where m.AwayTeamID == teamID || m.HomeTeamID == teamID select m).ToList();

            int lostCount = 0;

            foreach (Match match in matches)
            {
                if (match.HomeTeamID == teamID)
                {
                    if (match.HomeTeamScored < match.AwayTeamScored)
                        lostCount++;
                }

                if (match.AwayTeamID == teamID)
                {
                    if (match.HomeTeamScored > match.AwayTeamScored)
                        lostCount++;
                }
            }

            return lostCount;
        }

        private int GetGoalsScoredCount(int teamID)
        {
            List<Match> matches = (from m in db.Matches where m.AwayTeamID == teamID || m.HomeTeamID == teamID select m).ToList();
            int goalCount = 0;
            foreach(Match match in matches)
            {
                if (match.HomeTeamID == teamID)
                    goalCount += match.HomeTeamScored;
                if (match.AwayTeamID == teamID)
                    goalCount += match.AwayTeamScored;
            }

            return goalCount;
        }

        private int GetGoalsConcededCount(int teamID)
        {
            List<Match> matches = (from m in db.Matches where m.AwayTeamID == teamID || m.HomeTeamID == teamID select m).ToList();
            int goalCount = 0;
            foreach(Match match in matches)
            {
                if (match.HomeTeamID == teamID)
                    goalCount += match.AwayTeamScored;
                if (match.AwayTeamID == teamID)
                    goalCount += match.HomeTeamScored;
            }

            return goalCount;
        }

        private int GetGoalDifference(int scored, int conceded)
        {
            int difference = 0;
            difference = (scored - conceded);

            return difference;
        }

        private int GetPoints(int won, int tied, int lost, int substracted)
        {
            int points = 0;
            int pointsWon = (won * 3);
            int pointsTied = (tied * 1);
            int pointsLost = (lost * 0);
            points = ((pointsWon + pointsTied + pointsLost) - substracted );
            return points;
        }
    }
}