using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteCKC.Models
{
    public class DatabaseManager
    {
        CKCDatabase db = new CKCDatabase();
        public int AddCompetition(int compClass, int compGroup)
        {
            // Temp solution to bug: Competition ID doens't match teamID
            Competition compo = new Competition
            {
                ID = (from c in db.Competitions select c).Count(),
                Class = compClass,
                Group = compGroup
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
            int teamID = db.SaveChanges();
            return 0;
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

        public List<Team> GetTeamsByCompID(int compID)
        {
            List<Team> teams = (from t in db.Teams where t.CompID == compID select t).ToList();
            return teams;
        }
    }
}
