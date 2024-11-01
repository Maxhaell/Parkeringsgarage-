using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class Incheckning
    {
        public static void CheckIn()
        {
            Console.WriteLine("Välkommen till Smart Parking!");

            while (true)
            {
                Console.WriteLine("Meny - Smart Parking");
                Console.WriteLine("====");
                Console.WriteLine("Tryck 1 för Parkeringsgäst");
                Console.WriteLine("Tryck 2 för Parkeringsvakt");
                Console.WriteLine("Tryck 3 för Parkeringschef");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();

                switch (key.KeyChar)
                {
                    case '1':
                        ParkingGuest();
                        break;
                    case '2':
                        ParkingGuard();
                        break;
                    case '3':
                        ParkingOwner();
                        break;
                }
            }
        }

        public static void ParkingGuest()
        {
            {
                Console.WriteLine("Välkommen Parkeringsgäst!");
                Console.WriteLine("Vad har du för fordonstyp");
                Console.WriteLine("Tryck 1 för bil");
                Console.WriteLine("Tryck 2 för motorcykel");
                Console.WriteLine("Tryck 3 för buss");
                Console.WriteLine("Tryck 0 för att gå tillbaka till förra menyn");


                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();

                switch (key.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Du har en bil");
                        break;
                    case '2':
                        Console.WriteLine("Du har en motorcykel");
                        break;
                    case '3':
                        Console.WriteLine("Du har en buss");
                        break;
                    case '0':
                        CheckIn();
                        break;
                }
                
                        Console.WriteLine("Skriv in registreringsnummer:");
                        string regNr = Console.ReadLine();
                        Console.WriteLine("Ditt registreringsnummer är: " + regNr);

                        Console.WriteLine("Skriv in bilmärke: ");
                        string carBrand = Console.ReadLine();
                        Console.WriteLine("Ditt bilmärke är: " + carBrand);
                        Console.WriteLine("Skriv in färg: ");
                        string color = Console.ReadLine();
                        Console.WriteLine("Din färg är: " + color);
                        Console.WriteLine("Hur länge vill du parkera? ");
                        string parkTime = Console.ReadLine();
                        Console.WriteLine("Du vill parkera i " + parkTime + " minuter");
                        Console.WriteLine("Ditt pris blir: " + parkTime + "kr");
                   
            }
        }

            public static void ParkingGuard()
        {
            Console.WriteLine("Välkommen Jan-Erik");
        }

        public static void ParkingOwner()
        {
            Console.WriteLine("Välkommen Karen!");
        }
    }
}
