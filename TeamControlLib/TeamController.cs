using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamControlLib.Model;

namespace TeamControlLib
{
    public class TeamController
    {
        public int VoegSpelerToe(Speler s)
        {
            using (var context = new TeamDbContext())
            {
                try
                {
                    if (!context.Spelers.Contains(s))
                    {
                        context.Spelers.Add(s);
                        SelecteerTeam(s.TeamStamnummer).Spelers.Add(s);
                        context.SaveChanges();
                        return context.Spelers.Single(x => x.Rugnummer == s.Rugnummer).Id;
                    }
                    else
                        return 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }
        public bool VoegTeamToe(Team team)
        {
            using (var context = new TeamDbContext())
            {
                try
                {
                    if (!context.Teams.Contains(team))
                    {
                        context.Teams.Add(team);
                        context.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public bool VoegTransferToe(Transfer transfer)
        {
            using (var context = new TeamDbContext())
            {
                try
                {
                    if (!context.Transfers.Contains(transfer))
                    {
                        VoegSpelerToeAanTeam(transfer.SpelerId, transfer.NieuwTeamId);
                        VerwijderSpelerVanTeam(transfer.SpelerId, transfer.OudTeamId);
                        SelecteerSpeler(transfer.SpelerId).TeamId = transfer.NieuwTeamId;
                        context.Transfers.Add(transfer);
                        context.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public bool VoegSpelerToeAanTeam(int spelerID, int teamID)
        {
            using (var context = new TeamDbContext())
            {
                try
                {
                    if (context.Teams.Find(teamID) != null)
                    {
                        Team toAddTo = context.Teams.Find(teamID);
                        Speler toAdd = SelecteerSpeler(spelerID);
                        if (!toAddTo.Spelers.Contains(toAdd))
                        {
                            toAdd.TeamId = toAddTo.Id;
                            toAddTo.Spelers.Add(toAdd);
                            context.SaveChanges();
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public bool VerwijderSpelerVanTeam(int spelerID, int teamID)
        {
            using (var context = new TeamDbContext())
            {
                try
                {
                    if (context.Teams.Find(teamID) != null)
                    {
                        Team toRemoveFrom = context.Teams.Find(teamID);
                        Speler toRemove = SelecteerSpeler(spelerID);
                        if (toRemoveFrom.Spelers.Contains(toRemove))
                        {
                            toRemove.TeamId = 0;
                            toRemoveFrom.Spelers.Remove(toRemove);
                            context.SaveChanges();
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public bool UpdateSpeler(Speler speler)
        {
            using (var context = new TeamDbContext())
            {
                try
                {
                    if (SelecteerSpeler(speler.Id) != null)
                    {
                        var toEdit = context.Spelers.Find(speler.Id);
                        toEdit.Naam = speler.Naam;
                        toEdit.Rugnummer = speler.Rugnummer;
                        toEdit.Waarde = speler.Waarde;
                        context.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public bool UpdateTeam(Team team)
        {
            using (var context = new TeamDbContext())
            {
                try
                {
                    if (SelecteerTeam(team.StamNummer) != null)
                    {
                        var toEdit = context.Teams.Find(team.Id);
                        toEdit.Naam = team.Naam;
                        toEdit.Bijnaam = team.Bijnaam;
                        toEdit.Trainer = team.Trainer;
                        context.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public Speler SelecteerSpeler(int spelerID)
        {
            Speler select = null;
            using (var context = new TeamDbContext())
            {
                select = context.Spelers.SingleOrDefault(x => x.Id == spelerID);
            }
            return select;
        }
        public Team SelecteerTeam(int stamnummer)
        {
            Team select = null;
            using (var context = new TeamDbContext())
            {
                select = context.Teams.SingleOrDefault(x => x.StamNummer == stamnummer);
                var spelerList = context.Spelers.Where(x => x.TeamStamnummer == select.StamNummer).ToList();
            }
            return select;
        }
        public Transfer SelecteerTransfer(int transferID)
        {
            Transfer select = null;
            using (var context = new TeamDbContext())
            {
                select = context.Transfers.SingleOrDefault(x => x.Id == transferID);
            }
            return select;
        }
        public List<Speler> GeefAlleSpelers()
        {
            List<Speler> spelers = new List<Speler>();
            using (var context = new TeamDbContext())
            {
                spelers = context.Spelers.ToList();
            }
            return spelers;
        }
        public List<Team> GeefAlleTeams()
        {
            List<Team> teams;
            using (var context = new TeamDbContext())
            {
                teams = context.Teams.ToList();
                foreach (var x in teams)
                {
                    var spelerList = context.Spelers.Where(y => y.TeamStamnummer == x.StamNummer).ToList();
                }
            }
            return teams;
        }
        public List<Transfer> GeefAlleTransfers()
        {
            List<Transfer> transfers = new List<Transfer>();
            using (var context = new TeamDbContext())
            {
                transfers = context.Transfers.ToList();
            }
            return transfers;
        }
    }
}
