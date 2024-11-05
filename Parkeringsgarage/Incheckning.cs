using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class Incheckning
    {
       
        public static void CheckIn()
        {
            
            Garage.GarageGrid();
            Console.WriteLine("Välkommen till Smart Parking!");
            bool running = true;
            while (running)
            {
                Console.SetCursorPosition(35, 0);
                Console.WriteLine("Meny - Smart Parking");
                Console.WriteLine("====================");
                Console.WriteLine("Tryck 1 för Parkeringsgäst");
                Console.WriteLine("Tryck 2 för Parkeringsvakt");
                Console.WriteLine("Tryck 3 för Parkeringschef");
                Console.WriteLine("Tryck 0 för att avsluta");

                ConsoleKeyInfo key = Console.ReadKey();
                Garage.DisplayGrid();

                switch (key.KeyChar)
                {
                    case '0':
                        running = false;
                        break;
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
            bool inParkingGuestMenu = true;
            while (inParkingGuestMenu)
            {
                Garage.DisplayGrid();
                Console.WriteLine("Välkommen Parkeringsgäst!");
                Console.WriteLine("Vad har du för fordonstyp");
                Console.WriteLine("====");
                Console.WriteLine("Tryck 1 för bil");
                Console.WriteLine("Tryck 2 för motorcykel");
                Console.WriteLine("Tryck 3 för buss");
                Console.WriteLine("Tryck 0 för att gå tillbaka till huvudmenyn");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();

                switch (key.KeyChar)
                {
                    case '0':
                        inParkingGuestMenu = false;
                        break;
                    case '1':
                        HandleVehicleCheckin("bil");
                        break;
                    case '2':
                        HandleVehicleCheckin("motorcykel");
                        break;
                    case '3':
                        HandleVehicleCheckin("buss");
                        break;
                }
            }
        }

        private static bool HandleVehicleCheckin(string vehicleType)
        {
            bool inCheckin = true;
            string regNr = "", carBrand = "", color = "", parkTime = "";

            while (inCheckin)
            {
                Console.Clear();
                Garage.DisplayGrid();
                Console.WriteLine($"Registrering av {vehicleType}");
                Console.WriteLine("====");
                Console.WriteLine("1. Ange registreringsnummer" + (!string.IsNullOrEmpty(regNr) ? $" (angivet: {regNr})" : ""));
                Console.WriteLine("2. Ange märke" + (!string.IsNullOrEmpty(carBrand) ? $" (angivet: {carBrand})" : ""));
                Console.WriteLine("3. Ange färg" + (!string.IsNullOrEmpty(color) ? $" (angivet: {color})" : ""));
                Console.WriteLine("4. Ange parkeringstid" + (!string.IsNullOrEmpty(parkTime) ? $" (angivet: {parkTime} minuter)" : ""));
                Console.WriteLine("5. Slutför registrering");
                Console.WriteLine("0. Gå tillbaka");

                ConsoleKeyInfo choice = Console.ReadKey();

                switch (choice.KeyChar)
                {
                    case '0':
                        return true;
                    case '1':
                        Console.WriteLine("Skriv in registreringsnummer:");
                        regNr = Console.ReadLine() ?? string.Empty;
                        break;
                    case '2':
                        Console.WriteLine("Skriv in märke:");
                        carBrand = Console.ReadLine() ?? string.Empty;
                        break;
                    case '3':
                        Console.WriteLine("Skriv in färg:");
                        color = Console.ReadLine() ?? string.Empty;
                        break;
                    case '4':
                        Console.WriteLine("Hur länge vill du parkera? (minuter)");
                        parkTime = Console.ReadLine() ?? "0";
                        break;
                    case '5':
                        if (ValidateCheckin(regNr, carBrand, color, parkTime))
                        {
                            ShowSummary(vehicleType, regNr, carBrand, color, parkTime);
                            return true;
                        }
                        break;
                }
            }
            return true;
        }

        private static bool ValidateCheckin(string regNr, string carBrand, string color, string parkTime)
        {
            if (string.IsNullOrEmpty(regNr) || string.IsNullOrEmpty(carBrand) ||
                string.IsNullOrEmpty(color) || string.IsNullOrEmpty(parkTime))
            {
                Console.WriteLine("Alla fält måste fyllas i!");
                Console.WriteLine("\nTryck Enter för att fortsätta...");
                Console.ReadLine();
                return false;
            }
            return true;
        }

        private static void ShowSummary(string vehicleType, string regNr, string carBrand, string color, string parkTime)
        {
            Console.Clear();
            Garage.DisplayGrid();
            Console.WriteLine("Sammanfattning av registrering");
            Console.WriteLine("====");
            Console.WriteLine($"Fordonstyp: {vehicleType}");
            Console.WriteLine($"Registreringsnummer: {regNr}");
            Console.WriteLine($"Märke: {carBrand}");
            Console.WriteLine($"Färg: {color}");
            Console.WriteLine($"Parkeringstid: {parkTime} minuter");
            Console.WriteLine($"Pris: {parkTime}kr");
            Console.WriteLine("\nTryck Enter för att återgå...");
            Console.ReadLine();
        }

        public static void ParkingGuard()
        {
            Console.WriteLine("Välkommen Jan-Erik");
            Console.WriteLine("\nTryck Enter för att återgå...");
            Console.ReadLine();
        }

        public static void ParkingOwner()
        {
            Console.WriteLine("Välkommen Karen!");
            Console.WriteLine("\nTryck Enter för att återgå...");
            Console.ReadLine();
        }
    }
}
