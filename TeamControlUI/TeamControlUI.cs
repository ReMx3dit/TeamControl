using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Text;
using TeamControlLib;
using TeamControlLib.Model;

namespace TeamControlUI
{
    class TeamControlUI
    {
        private TeamController TC { get; set; } = new TeamController();
        public void MainUI(string message = "no")
        {
            Console.WriteLine("TeamController v1");
            Console.WriteLine();
            if (message != "no")
                Console.WriteLine(message);
            Console.WriteLine();
            Console.WriteLine("Gelieve een keuze te maken uit onderstaand menu:");
            Console.WriteLine(" [F1] - Toon alle spelers");
            Console.WriteLine(" [F2] - Toon alle teams");
            Console.WriteLine(" [F3] - Toon alle transfers");
            Console.WriteLine(" [F4] - Voeg een speler toe");
            Console.WriteLine(" [F5] - Voeg een team toe");
            Console.WriteLine(" [F6] - Voeg een transfer toe");
            Console.WriteLine(" [F7] - Wijzig een speler");
            Console.WriteLine(" [F8] - Wijzig een team");
            Console.WriteLine(" [ 1] - Zoek een speler op ID");
            Console.WriteLine(" [ 2] - Zoek een team op ID");
            Console.WriteLine(" [ 3] - Zoek een transfer op ID");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.F1:
                    F1();
                    break;
                case ConsoleKey.F2:
                    F2();
                    break;
                case ConsoleKey.F3:
                    F3();
                    break;
                case ConsoleKey.F4:
                    F4();
                    break;
                case ConsoleKey.F5:
                    F5();
                    break;
                case ConsoleKey.F6:
                    F6();
                    break;
                case ConsoleKey.F7:
                    F7();
                    break;
                case ConsoleKey.F8:
                    F8();
                    break;
                case ConsoleKey.D1:
                    F9();
                    break;
                case ConsoleKey.D2:
                    F10();
                    break;
                case ConsoleKey.D3:
                    F11();
                    break;
                case ConsoleKey.Q:
                    ImporterUI();
                    break;
                default:
                    break;
            }
        }
        public void F1() // Geef alle Spelers
        {
            Console.Clear();
            Console.WriteLine("Alle spelers:");
            StringBuilder st = new StringBuilder();
            foreach (var x in TC.GeefAlleSpelers())
            {
                st.Append(x.ToString() + "\n");
            }
            Console.WriteLine(st.ToString());
            Console.WriteLine();
            Console.WriteLine("Om te herbeginnen, druk op een toets.");
            Console.ReadLine();
            Console.Clear();
            MainUI();
        }
        public void F2() // Geef alle Teams
        {
            Console.Clear();
            Console.WriteLine("Alle teams:");
            StringBuilder st = new StringBuilder();
            foreach (var x in TC.GeefAlleTeams())
            {
                st.Append(x.ToString());
                foreach (var y in x.Spelers)
                {
                    st.Append("     - " + y + "\n");
                }
            }
            Console.WriteLine(st.ToString());
            Console.WriteLine();
            Console.WriteLine("Om te herbeginnen, druk op een toets.");
            Console.ReadLine();
            Console.Clear();
            MainUI();
        }
        public void F3() // Geef alle Transfers
        {
            Console.Clear();
            Console.WriteLine("Alle transfers:");
            StringBuilder st = new StringBuilder();
            foreach (var x in TC.GeefAlleTransfers())
            {
                st.Append(x.ToString());
            }
            Console.WriteLine(st.ToString());
            Console.WriteLine();
            Console.WriteLine("Om te herbeginnen, druk op een toets.");
            Console.ReadLine();
            Console.Clear();
            MainUI();
        }
        public void F4() // Voeg speler toe
        {
            Console.Clear();
            Console.Write("Naam van de speler: ");
            string naam = Console.ReadLine();

            int rugnummer;
            do
            {
                Console.Clear();
                Console.Write("Rugnummer van de speler: ");
            }
            while (!int.TryParse(Console.ReadLine(), out rugnummer));

            int waarde;
            do
            {
                Console.Clear();
                Console.Write("Waarde van de speler: ");
            }
            while (!int.TryParse(Console.ReadLine(), out waarde));

            Console.Clear();
            Console.WriteLine("Bij welke club speelt deze speler: ");
            foreach (var x in TC.GeefAlleTeams())
            {
                Console.WriteLine(x);
            }
            int clubnummer;
            do
            {
                Console.Write("Geef de club-ID in:");
            } while (!int.TryParse(Console.ReadLine(), out clubnummer) && TC.SelecteerTeam(clubnummer) != null);
            Speler s = new Speler
            {
                Naam = naam,
                Rugnummer = rugnummer,
                Waarde = waarde,
                TeamId = TC.SelecteerTeam(clubnummer).Id,
                TeamStamnummer = clubnummer
            };
            int spelerID = TC.VoegSpelerToe(s);
            TC.VoegSpelerToeAanTeam(spelerID, clubnummer);
            Console.Clear();
            MainUI($"Speler {naam} met id {spelerID} is toegevoegd aan team met stamnummer {clubnummer}");
        }
        public void F5() // Voeg team toe
        {
            Console.Clear();
            Console.Write("Naam van het team: ");
            string naam = Console.ReadLine();
            Console.Clear();
            Console.Write("Bijnaam van het team: ");
            string bijnaam = Console.ReadLine();
            Console.Clear();
            Console.Write("Wie is de trainer van het team: ");
            string trainer = Console.ReadLine();
            int clubnummer;
            do
            {
                Console.Clear();
                Console.Write("Stamnummer van het team: ");
            } while (!int.TryParse(Console.ReadLine(), out clubnummer) && TC.SelecteerTeam(clubnummer) == null);
            Team t = new Team
            {
                Naam = naam,
                Bijnaam = bijnaam,
                Trainer = trainer,
                StamNummer = clubnummer
            };
            TC.VoegTeamToe(t);
            Console.Clear();
            MainUI($"Team {naam} is toegevoegd met stamnummer {clubnummer}");
        }
        public void F6() // Voeg transfer toe
        {
            int spelerID;
            do
            {
                Console.Clear();
                foreach (var x in TC.GeefAlleSpelers())
                {
                    Console.WriteLine(x);
                }
                Console.Write("Welke speler wordt getransfereerd?: ");
            } while (!int.TryParse(Console.ReadLine(), out spelerID) && TC.SelecteerSpeler(spelerID) != null);
            Speler s = TC.SelecteerSpeler(spelerID);
            Console.Clear();
            int teamID;
            do
            {
                Console.Clear();
                foreach (var x in TC.GeefAlleTeams())
                {
                    if (!x.Spelers.Contains(s))
                        Console.WriteLine(x);
                }
                Console.Write("Naar welk team gaat deze speler?: ");
            } while (!int.TryParse(Console.ReadLine(), out teamID) && TC.SelecteerTeam(teamID) != null);
            Team oldTeam = TC.SelecteerTeam(s.TeamStamnummer);
            Team newTeam = TC.SelecteerTeam(teamID);
            TC.VerwijderSpelerVanTeam(s.Id, oldTeam.StamNummer);
            TC.VoegSpelerToeAanTeam(s.Id, newTeam.StamNummer);
            Transfer transfer = new Transfer
            {
                SpelerId = s.Id,
                OudTeamId = oldTeam.Id,
                NieuwTeamId = newTeam.Id
            };
            TC.VoegTransferToe(transfer);
            Console.Clear();
            MainUI("Transfer is toegevoegd.");
        }
        public void F7() // Wijzig speler
        {
            Console.Clear();
            int spelerID;
            do
            {
                Console.Clear();
                foreach (var x in TC.GeefAlleSpelers())
                {
                    Console.WriteLine(x);
                }
                Console.Write("Welke speler wordt bewerkt?: ");
            } while (!int.TryParse(Console.ReadLine(), out spelerID) && TC.SelecteerSpeler(spelerID) != null);
            Speler s = TC.SelecteerSpeler(spelerID);

            Console.Clear();
            Console.Write("Speler naam: ");
            string naam = Tools.ReadLine(s.Naam);

            int rugnummer;
            do
            {
                Console.Clear();
                Console.Write("Rugnummer van de speler: ");
            }
            while (!int.TryParse(Tools.ReadLine(s.Rugnummer.ToString()), out rugnummer));

            int waarde;
            do
            {
                Console.Clear();
                Console.Write("Waarde van de speler: ");
            }
            while (!int.TryParse(Tools.ReadLine(s.Waarde.ToString()), out waarde));

            Console.Clear();
            Console.WriteLine("De speler zal als volgt gewijzigd worden:");
            Console.WriteLine($"Naam:           {s.Naam} => {naam}");
            Console.WriteLine($"RugNummer:      {s.Rugnummer} => {rugnummer}");
            Console.WriteLine($"Waarde:         {s.Waarde} => {waarde}");
            Console.WriteLine();
            s.Naam = naam;
            s.Rugnummer = rugnummer;
            s.Waarde = waarde;
            
            if (TC.UpdateSpeler(s))
            {
                Console.Clear();
                MainUI("Speler is gewijzigd");
            }
        }
        public void F8() // Wijzig team
        {
            Console.Clear();
            foreach (var x in TC.GeefAlleTeams())
            {
                Console.WriteLine(x);
            }
            int clubnummer;
            do
            {
                Console.Write("Geef de club-ID in:");
            } while (!int.TryParse(Console.ReadLine(), out clubnummer) && TC.SelecteerTeam(clubnummer) != null);
            Team t = TC.SelecteerTeam(clubnummer);

            Console.Clear();
            Console.Write("Naam van het team: ");
            string naam = Tools.ReadLine(t.Naam);
            Console.Clear();
            Console.Write("Bijnaam van het team: ");
            string bijnaam = Tools.ReadLine(t.Bijnaam);
            Console.Clear();
            Console.Write("Wie is de trainer van het team: ");
            string trainer = Tools.ReadLine(t.Trainer);

            Console.Clear();
            Console.WriteLine("Het team zal als volgt gewijzigd worden:");
            Console.WriteLine($"Naam:           {t.Naam} => {naam}");
            Console.WriteLine($"Bijnaam:        {t.Bijnaam} => {bijnaam}");
            Console.WriteLine($"Waarde:         {t.Trainer} => {trainer}");
            Console.WriteLine();
            t.Naam = naam;
            t.Bijnaam = bijnaam;
            t.Trainer = trainer;

            if (TC.UpdateTeam(t))
            {
                Console.Clear();
                MainUI("Team is gewijzigd");
            }
        }
        public void F9() // Zoek speler op ID
        {
            Console.Clear();
            int spelerID;
            do
            {
                Console.Write("Geef een speler ID in: ");
            } while (!int.TryParse(Console.ReadLine(), out spelerID) && TC.SelecteerSpeler(spelerID) != null);
            Console.Clear();
            Console.WriteLine(TC.SelecteerSpeler(spelerID));
            Console.WriteLine();
            Console.WriteLine("Om te herbeginnen, druk op een toets.");
            Console.ReadLine();
            Console.Clear();
            MainUI();
        }
        public void F10() // Zoek team op stamnummer
        {
            Console.Clear();
            int teamID;
            do
            {
                Console.Write("Geef een stamnummer in: ");
            } while (!int.TryParse(Console.ReadLine(), out teamID) && TC.SelecteerTeam(teamID) != null);
            Console.Clear();
            Console.WriteLine(TC.SelecteerTeam(teamID));
            Console.WriteLine();
            Console.WriteLine("Om te herbeginnen, druk op een toets.");
            Console.ReadLine();
            Console.Clear();
            MainUI();
        }
        public void F11() // Zoek transfer op ID
        {
            Console.Clear();
            int transfID;
            do
            {
                Console.Write("Geef een transfer ID in: ");
            } while (!int.TryParse(Console.ReadLine(), out transfID) && TC.SelecteerTransfer(transfID) != null);
            Console.Clear();
            Console.WriteLine(TC.SelecteerTransfer(transfID));
            Console.WriteLine();
            Console.WriteLine("Om te herbeginnen, druk op een toets.");
            Console.ReadLine();
            Console.Clear();
            MainUI();
        }
        public void ImporterUI()
        {
            Console.Clear();
            Console.WriteLine("Please drag and drop the CSV file for import in this window.");
            string path = Console.ReadLine();

            CSVImporter imp = new CSVImporter();
            imp.Import(path);

            Console.Clear();
            MainUI("Import finished.");
        }
    }
}
