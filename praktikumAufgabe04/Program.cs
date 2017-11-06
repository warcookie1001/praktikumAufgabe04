using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Dieses Programm simuliert ein Roulett-Spiel mit beliebig vielen Spieltagen und setzt dabei nach einer vorgegebenen Strategie:
/// Es wird ein Starteinsatz auf eine Farbe gesetzt. Wird die Runde verloren, wird der Einsatz verdoppelt, so lange bis eine Runde gewonnen wird.
/// Wird eine Runde gewonnen, wird in der nächsten wieder der Starteinsatz gesetzt.
/// 
/// Im ersten Block können alle Einstellungen wie z.B. die zu setzende Farbe vorgenommen werden.
/// </summary>

namespace praktikumAufgabe04
{
    class Program
    {   
        static void Main(string[] args)
        {
            //--------------------------------------------------------------------
            //Einstellungen des Programms, hier editieren
            const int spielTage = 100;        //Anzahl an Spieltagen, die simuliert werden
            const int limit = 500;          //Ausstiegslimit pro Spieltag
            const int startGuthaben = 200;  //Startguthaben pro Spieltag
            const int startEinsatz = 5;     //(Start-)Einsatz 
            string meineFarbe = "rot";      //Farbe auf die gesetzt wird

            const int loggingLevel = 1;     //Level der Konsolenausgabe (0-2) | 0:Nur Ausgabe des Gesammtergebnisses
                                            //1: Ausgabe des Ergebnisses pro Spieltag | 2: Ausgabe jedes einzelnen Wurfes
            //---------------------------------------------------------------------

            //Deklaration anderer Variablen - Finger weg!
            int guthaben;           //Laufendes Guthaben eines Spieltages
            int einsatz;            //Laufender Einsatz eines Spieltages
            int aktuellerSpieltag;  //Zähler des aktuellen Spieltages
            int gesamtGewinn = 0;   //Gesamtgewinn aller Spieltage
            int tageGewonnen = 0;   //Zähler der gewonnenen Spieltage
            int tageVerloren = 0;   //Zähler der verlorenen Spieltage
            string farbe;           //aktuell "geworfene" Farbe



            // Zufallszahlengenerator initialisieren
            Random zufall = new Random();


            
            //Schleife, bestimmt Anzahl an Spieltagen 
            for (aktuellerSpieltag = 1; aktuellerSpieltag <= spielTage; aktuellerSpieltag++)
            {

                if (loggingLevel >= 2)
                {
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Spieltag " + aktuellerSpieltag);
                    Console.WriteLine("----------------------------------------------------------");
                }
               
                //Setzt guthaben und einsatz am Anfang jedes Spieltages auf eingestellte Werte
                guthaben = startGuthaben;
                einsatz = startEinsatz;

                //Schleife, bestimmt wie lange pro Spieltag gespielt wird
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

                    //Abfrage, geht sicher das einsatz nicht größer als das Guthaben wird
                    if (guthaben < einsatz)
                    {
                        einsatz = guthaben;
                    }




                    // Einzelne Würfe ausgeben (nur wenn loggingLevel 2 (oder größer))
                    if (loggingLevel >= 2)
                    {
                        Console.Write("Gefallen ist {0}, ", zahl);
                        //Bestimmung der Farbausgabe in Konsole
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

                //ENDE Schleife Spieltag
                }

                //Ausgabe des Ergebniss des Spieltages, nur wenn loggingLevel 1 oder größer
                if (loggingLevel >= 1)
                {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Ergebnis Spieltag {0}: {1}", aktuellerSpieltag, (guthaben - 200));
                }

                //Bestimmung des Gewinns/Verlust des Spieltages, addieren zu gesammtGewinn
                gesamtGewinn += (guthaben - startGuthaben);
                //Bestimmen ob Spieltag verloren oder gewonnen, inkrementieren des entsprechenden Zählers
                if (guthaben < 5)
                    tageVerloren++;
                else if (guthaben >= 500)
                    tageGewonnen++;
            
            //ENDE Schleife aller Spieltage
            }

            //Ausgabe des Gesamtergebnisses
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Gesamtgewinn: " + gesamtGewinn);
            Console.WriteLine("Spieltage gewonnen: " + tageGewonnen);
            Console.WriteLine("Spieltage verloren: " + tageVerloren);
            Console.ReadLine();

        }
    }
}
