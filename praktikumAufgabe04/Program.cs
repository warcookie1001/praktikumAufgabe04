using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktikumAufgabe04
{
    class Program
    {
        static void Main(string[] args)
        {
            const int spielTage = 100;
            const int limit = 500;
            const int startGuthaben = 200;
            const int startEinsatz = 5;
            string meineFarbe = "rot";
            const int loggingLevel = 2; //0 - 2

            int guthaben;
            int einsatz;
            int aktuellerSpieltag;
            int gesamtGewinn = 0;
            int tageGewonnen = 0;
            int tageVerloren = 0;
            string farbe;
            int[] statistikZahlen = new int[37];

            // Zufallszahlengenerator initialisieren
            Random zufall = new Random();
            //Array zur Statistik initialisieren
            foreach (int i in statistikZahlen)
                statistikZahlen[i] = 0;


            // Kugel werfen: Zufallszahl zwischen 0 und 36 bestimmen

            for (aktuellerSpieltag = 1; aktuellerSpieltag <= spielTage; aktuellerSpieltag++)
            {

                if (loggingLevel >= 2)
                {
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Spieltag " + aktuellerSpieltag);
                    Console.WriteLine("----------------------------------------------------------");
                }
               

                guthaben = startGuthaben;
                einsatz = startEinsatz;

                while (guthaben >= startEinsatz && guthaben < limit)
                {
                    //neue Zahl "Werfen"
                    int zahl = zufall.Next(0, 37);
                    // Farbe rot oder schwarz bestimmen
                    if (zahl == 0)
                        farbe = "grün";
                    else if (zahl % 2 == 1)
                        farbe = "rot";
                    else
                        farbe = "schwarz";
                    //Getroffene Zahl zum statistik-Array hinzufügen
                    statistikZahlen[zahl]++;
                    

                    // Überprüfen ob Farbe richtig gewählt und guthaben anpassen
                    if (farbe == meineFarbe)
                    {
                        guthaben += einsatz;
                        einsatz = startEinsatz;
                    }

                    else
                    {
                        guthaben -= einsatz;
                        einsatz *= 2;
                    }

                    if (guthaben < einsatz)
                    {
                        einsatz = guthaben;
                    }




                    // Ergebnis ausgeben
                    if (loggingLevel >= 2)
                    {
                        Console.Write("Gefallen ist {0}, ", zahl);
                        if (farbe == "grün")
                            Console.ForegroundColor = ConsoleColor.Green;
                        else if (farbe == "rot")
                            Console.ForegroundColor = ConsoleColor.Red;
                        else if (farbe == "schwarz")
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }

                        Console.Write("{0}", farbe);

                        Console.ResetColor();
                        Console.WriteLine(".");
                        Console.WriteLine("Neues Guthaben: " + guthaben);
                    }

                }
                if (loggingLevel >= 1)
                {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Ergebnis Spieltag {0}: {1}", aktuellerSpieltag, (guthaben - 200));
                }

                gesamtGewinn += (guthaben - 200);
                if (guthaben < 5)
                    tageVerloren++;
                else if (guthaben >= 500)
                    tageGewonnen++;

            }
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Gesamtgewinn: " + gesamtGewinn);
            Console.WriteLine("Spieltage gewonnen: " + tageGewonnen);
            Console.WriteLine("Spieltage verloren: " + tageVerloren);
            Console.ReadLine();

        }
    }
}
