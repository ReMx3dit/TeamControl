using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TeamControlLib.Model;

namespace TeamControlLib
{
    public class CSVImporter
    {
        public void Import(string pathToFile)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(pathToFile))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            SortedList<int, Team> teams = new SortedList<int, Team>();
            foreach (string line in lines)
            {
                string[] entry = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                Team t = new Team
                {
                    Naam = entry[2],
                    StamNummer = int.Parse(entry[4]),
                    Bijnaam = entry[6],
                    Trainer = entry[5]
                };
                teams.TryAdd(t.StamNummer, t);
            }
            foreach (string line in lines)
            {
                string[] entry = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                Speler s = new Speler
                {
                    Naam = entry[0],
                    Rugnummer = int.Parse(entry[1]),
                    Waarde = int.Parse(entry[3].Replace(" ", "")),
                    TeamStamnummer = int.Parse(entry[4])
                };
                teams[int.Parse(entry[4])].Spelers.Add(s);
            }

            TeamController tc = new TeamController();
            foreach (Team t in teams.Values)
                tc.VoegTeamToe(t);
        }
    }
}
