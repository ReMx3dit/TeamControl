using System;
using System.Collections.Generic;
using System.Text;

namespace TeamControlLib.Model
{
    public class Speler
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int Rugnummer { get; set; }
        public int Waarde { get; set; }
        public int TeamId { get; set; }
        public int TeamStamnummer { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Speler speler &&
                   Naam == speler.Naam &&
                   Rugnummer == speler.Rugnummer &&
                   Waarde == speler.Waarde;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam, Rugnummer, Waarde);
        }

        public override string ToString()
        {
            return $"Speler: {Id}, {Naam}, {Rugnummer}, {Waarde}";
        }
    }
}
