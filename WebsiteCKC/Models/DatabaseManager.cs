using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteCKC.Models
{
    public class DatabaseManager
    {
        CKCDatabase db = new CKCDatabase();
        public int AddCompetition(int compClass, int compGroup, string userID)
        {
            // Temp solution to bug: Competition ID doens't match teamID
            Competition compo = new Competition
            {
                ID = (from c in db.Competitions select c).Count(),
                Class = compClass,
                Group = compGroup,
                UserID = userID
            };

            if(compo.ID == 0)
            {
                compo.ID = 1;
            }
            db.Competitions.Add(compo);
            
            int compID = db.SaveChanges();

            Competition comp = (from c in db.Competitions where c.Group == compo.Group && c.Class == compo.Class select c).FirstOrDefault();
            return comp.ID;
        }

        public int AddTeam(string clubname, int teamNumber, int compID)
        {
            db.Teams.Add(new Team
            {
                ClubName = clubname,
                TeamNumber = teamNumber,
                CompID = compID
            });
            db.SaveChanges();
            Team team = this.GetTeam(clubname, teamNumber, compID);
            return team.ID;
        }

        public void DeleteCompetition(int competitionID)
        {
            Competition comp = (from c in db.Competitions where c.ID == competitionID select c).FirstOrDefault();
            db.Database.ExecuteSqlCommand("DELETE * FROM Competitions");
            db.Competitions.Remove(comp);
            db.SaveChanges();
        }

        public void DeleteTeam(int teamID)
        {
            Team team = (from t in db.Teams where t.ID == teamID select t).FirstOrDefault();
        }

        public void ClearCompetitions()
        {
            List<Competition> comps = (from c in db.Competitions select c).ToList();

            foreach(Competition c in comps)
            {
                db.Competitions.Remove(c);
                db.SaveChanges();
            }
        }

        public void ClearTeams()
        {
            List<Team> teams = (from t in db.Teams select t).ToList();
            foreach(Team t in teams)
            {
                db.Teams.Remove(t);
                db.SaveChanges();
            }
        }

        public Competition GetCompetitionByID(int compID)
        {
            Competition comp = (from c in db.Competitions select c).FirstOrDefault();
            return comp;
        }

        public Competition GetCompetitionByUserID (string userID)
        {
            Competition c = (from comp in db.Competitions where comp.UserID == userID select comp).FirstOrDefault();

            return c;
        }

        public List<Team> GetTeamsByCompID(int compID)
        {
            List<Team> teams = (from t in db.Teams where t.CompID == compID select t).ToList();
            return teams;
        }

        public Team GetFavoriteTeamByUserID(string userID)
        {
            try
            {
                OwnedTeam team = (from t in db.OwnedTeams where t.UserID == userID select t).FirstOrDefault();
                Team ownedTeam = (from t in db.Teams where t.ID == team.TeamID select t).FirstOrDefault();

                return ownedTeam;
            }
            catch(Exception e )
            {
                return null;
            }
        }

        public Competition GetCompetition()
        {
            Competition comp = (from c in db.Competitions select c).FirstOrDefault();
            return comp;
        }

        public Boolean RemoveTeam(int teamID)
        {
            Team team = (from t in db.Teams where t.ID == teamID select t).FirstOrDefault();
            try
            {
                db.Teams.Remove(team);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            
            return true;
        }

        public Team GetTeam(string clubName, int TeamNumber, int compID)
        {
            Team team = (from t in db.Teams where t.ClubName == clubName && t.TeamNumber == TeamNumber && t.CompID == compID select t).FirstOrDefault();
            return team;
        }

        public Boolean SetFavoriteTeam(string userID, int compID, int teamID)
        {
            OwnedTeam team = (from t in db.OwnedTeams where t.UserID == userID && t.TeamID == teamID select t).FirstOrDefault();
            if(team == null)
            {
                OwnedTeam favTeam = new OwnedTeam
                {
                    TeamID = teamID,
                    UserID = userID
                };

                db.OwnedTeams.Add(favTeam);
                db.SaveChanges();
                
            }
            else
            {
                db.OwnedTeams.Remove(team);
                db.SaveChanges();

                OwnedTeam favTeam = new OwnedTeam
                {
                    TeamID = teamID,
                    UserID = userID
                };

                db.OwnedTeams.Add(favTeam);

            }

            return true;
        }

        public void AddMatch(MatchViewModels model)
        {
            Match match = new Match
            {
                MatchedPlayed = DateTime.Parse(model.MatchDate),
                HomeTeamID = model.HomeTeamID,
                HomeTeamScored = model.HomeTeamScored,
                AwayTeamID = model.AwayTeamID,
                AwayTeamScored = model.AwayTeamScored
            };

            db.Matches.Add(match);
            db.SaveChanges();

            return;
        }
    }
}
