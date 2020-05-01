using System;
using System.Collections.Generic;
using System.Text;

namespace TeamControlLib.Model
{
    public class Team
    {
        public int Id { get; set; }
        public int StamNummer { get; set; }
        public string Naam { get; set; }
        public string Bijnaam { get; set; }
        public string Trainer { get; set; }
        public List<Speler> Spelers { get; set; } = new List<Speler>();

        public override bool Equals(object obj)
        {
            return obj is Team team &&
                   StamNummer == team.StamNummer &&
                   Naam == team.Naam &&
                   Bijnaam == team.Bijnaam &&
                   Trainer == team.Trainer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StamNummer, Naam, Bijnaam, Trainer);
        }

        public override string ToString()
        {
            return $"Team: {StamNummer}, {Naam}, {Bijnaam}, {Trainer}\n";
        }
    }
}
