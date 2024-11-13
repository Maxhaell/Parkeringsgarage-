using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class Incheckning
    {
       // public static void List<List>colorOptions();

        public static void CheckIn()
        {
            Garage.GarageGrid();
            Console.Clear();
            bool running = true;
            while (running)
            {
                Garage.DisplayGrid();
                Console.WriteLine("Välkommen till Smart Parking!");
                Console.WriteLine("====================");
                Console.WriteLine("Meny - Smart Parking");
                Console.WriteLine("====================");
                Console.WriteLine("Tryck 1 för Parkeringsgäst");
                Console.WriteLine("Tryck 2 för Parkeringsvakt");
                Console.WriteLine("Tryck 3 för Parkeringschef");
                Console.WriteLine("Tryck 0 för att avsluta");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();

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
                Console.WriteLine("Tryck 1 för bil (2x2 platser)");
                Console.WriteLine("Tryck 2 för motorcykel (1x1 plats)");
                Console.WriteLine("Tryck 3 för buss (4x4 platser)");
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

        public static bool HandleVehicleCheckin(string vehicleType)
        {
            bool inCheckin = true;
            string regNr = "", brand = "", selectedColorName = "", parkTime = "";
            ConsoleColor selectedColor = ConsoleColor.White;
            bool isElectric = false;
            int passengers = 0;

            List<(string name, ConsoleColor color)> colorOptions = new()
            {
                ("röd ", ConsoleColor.Red),
                ("grön ", ConsoleColor.Green),
                ("blå ", ConsoleColor.Blue),
                ("gul ", ConsoleColor.Yellow),
                ("cyan ", ConsoleColor.Cyan),
                ("magenta ", ConsoleColor.Magenta)
            };


            while (inCheckin)
            {
                Console.Clear();
                Garage.DisplayGrid();
                Console.WriteLine($"Registrering av {vehicleType}");
                Console.WriteLine("====");
                Console.WriteLine("1. Ange registreringsnummer" + (!string.IsNullOrEmpty(regNr) ? $" (angivet: {regNr})" : ""));
                Console.WriteLine("2. Ange märke" + (!string.IsNullOrEmpty(brand) ? $" (angivet: {brand})" : ""));
                selectedColorName = colorOptions.FirstOrDefault(X => X.color == selectedColor).name;
                Console.WriteLine("3. Ange färg" + (selectedColor != ConsoleColor.White ? $" (angivet: {selectedColorName})" : "")); 
                Console.WriteLine("4. Ange parkeringstid" + (!string.IsNullOrEmpty(parkTime) ? $" (angivet: {parkTime} minuter)" : ""));

                if (vehicleType == "bil")
                    Console.WriteLine("5. Är det en elbil?" + (isElectric ? " (Ja)" : " (Nej)"));
                else if (vehicleType == "buss")
                    Console.WriteLine("5. Antal passagerare" + (passengers > 0 ? $" (angivet: {passengers})" : ""));

                Console.WriteLine("6. Slutför registrering");
                Console.WriteLine("0. Gå tillbaka");

                ConsoleKeyInfo choice = Console.ReadKey();

                switch (choice.KeyChar)
                {
                    case '0':
                        return true;
                    case '1':
                        Console.WriteLine("\nSkriv in registreringsnummer:");
                        regNr = Console.ReadLine()?.ToUpper() ?? string.Empty;
                        break;
                    case '2':
                        Console.WriteLine("\nSkriv in märke:");
                        brand = Console.ReadLine() ?? string.Empty;
                        break;
                    case '3':
                        Console.WriteLine("\nSkriv in färg:");
                        for (int i = 0; i < colorOptions.Count; i++)
                        {
                            Console.ForegroundColor = colorOptions[i].color;
                            Console.WriteLine($"{i + 1}.{colorOptions[i].name}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        if(int.TryParse(Console.ReadLine(),out int colorChoice) && colorChoice > 0 && colorChoice <= colorOptions.Count)
                        {
                            selectedColor = colorOptions[colorChoice - 1].color;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt val, försök igen!");
                            Console.ReadLine();
                        }
                        break;
                    case '4':
                        Console.WriteLine("\nHur länge vill du parkera? (minuter)");
                        parkTime = Console.ReadLine() ?? "0";
                        break;
                    case '5':
                        if (vehicleType == "bil")
                        {
                            Console.WriteLine("\nÄr det en elbil? (j/n):");
                            isElectric = Console.ReadLine()?.ToLower().StartsWith("j") ?? false;
                        }
                        else if (vehicleType == "buss")
                        {
                            Console.WriteLine("\nAnge antal passagerare:");
                            if (!int.TryParse(Console.ReadLine(), out passengers))
                            {
                                Console.WriteLine("Ogiltigt antal passagerare. Försök igen.");
                                Console.ReadLine();
                            }
                        }
                        break;
                    case '6':
                        if (ValidateCheckin(regNr, brand, selectedColorName, parkTime))
                        {
                            Fordon vehicle;
                            if (vehicleType == "bil")
                            {
                                List<Helpers> helpers = new List<Helpers>();
                                if (isElectric)
                                    helpers.Add(new Helpers("Ja"));
                                vehicle = new Car(regNr, selectedColor, helpers);
                            }
                            else
                            {
                                vehicle = new Fordon(regNr, selectedColor);
                            }

                            ShowSummary(vehicleType, regNr, brand, selectedColorName, parkTime, isElectric, passengers);

                            int row;
                            int col;
                            if (Garage.ParkVehicle(vehicleType, selectedColor, regNr, out row, out col))
                            {
                                Garage.fordon.Add(vehicle);

                                string platsInfo = vehicleType switch
                                {
                                    "buss" => $"(4x4 platser, från rad {row + 1}, plats {col + 1})",
                                    "bil" => $"(2x2 platser, från rad {row + 1}, plats {col + 1})",
                                    "motorcykel" => $"(plats: rad {row + 1}, plats {col + 1})",
                                    _ => ""
                                };
                                Garage.PlaceVehicle(vehicleType, row, col, selectedColor, regNr);
                                Console.WriteLine($"\nFordon parkerat på position: Rad{row}, Col{col}");
                                Console.WriteLine("Tryck Enter för att fortsätta...");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("\nKunde inte hitta en ledig parkeringsplats. Garaget är fullt.");
                                Console.WriteLine("Tryck Enter för att fortsätta...");
                                Console.ReadLine();
                            }

                            return true;
                        }
                        break;
                }
            }
            return true;
        }

        public static bool ValidateCheckin(string regNr, string brand, string selectedColorName, string parkTime)
        {
            if (string.IsNullOrEmpty(regNr) || string.IsNullOrEmpty(brand) ||
                string.IsNullOrEmpty(selectedColorName) || string.IsNullOrEmpty(parkTime))
            {
                Console.WriteLine("\nAlla fält måste fyllas i!");
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
                return false;
            }

            if (!int.TryParse(parkTime, out int time) || time <= 0)
            {
                Console.WriteLine("\nOgiltig parkeringstid. Ange ett positivt antal minuter.");
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
                return false;
            }

            // registreringsnummer (svensk standard: ABC123)
            if (!System.Text.RegularExpressions.Regex.IsMatch(regNr, @"^[A-Z]{3}\d{3}$"))
            {
                Console.WriteLine("\nOgiltigt registreringsnummer. Använd formatet ABC123.");
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
                return false;
            }

            return true;
        }

        public static void ShowSummary(string vehicleType, string regNr, string brand, string selectedColorName, string parkTime, bool isElectric = false, int passengers = 0)
        {
            Console.Clear();
            Garage.DisplayGrid();
            Console.WriteLine("Sammanfattning av registrering");
            Console.WriteLine("====");
            Console.WriteLine($"Fordonstyp: {vehicleType}");
            Console.WriteLine($"Storlek: {GetVehicleSize(vehicleType)}");
            Console.WriteLine($"Registreringsnummer: {regNr}");
            Console.WriteLine($"Märke: {brand}");
            Console.WriteLine($"Färg: {selectedColorName}");
            if (vehicleType == "bil")
                Console.WriteLine($"Elbil: {(isElectric ? "Ja" : "Nej")}");
            else if (vehicleType == "buss")
                Console.WriteLine($"Antal passagerare: {passengers}");
            Console.WriteLine($"Parkeringstid: {parkTime} minuter");

            // Beräkna pris baserat på fordonstyp och tid
            double pricePerMinute = vehicleType switch
            {
                "buss" => 4.0,    // Högre pris för buss (4x4)
                "bil" => isElectric ? 1.0 : 2.0,  // Rabatt för elbil
                "motorcykel" => 1.0,
                _ => 2.0
            };

            double totalPrice = double.Parse(parkTime) * pricePerMinute;
            Console.WriteLine($"Pris: {totalPrice:F2}kr");

            Console.WriteLine("\nTryck Enter för att fortsätta...");
            Console.ReadLine();
        }

        private static string GetVehicleSize(string vehicleType) => vehicleType switch
        {
            "buss" => "4x4 platser",
            "bil" => "2x2 platser",
            "motorcykel" => "1x1 plats",
            _ => "okänd storlek"
        };

        public static void ParkingGuard()
        {
            Console.Clear();
            Garage.DisplayGrid();
            Console.WriteLine("Välkommen Jan-Erik (Parkeringsvakt)");
            Console.WriteLine("\nParkeringsöversikt:");
            Console.WriteLine($"Totalt antal fordon: {Garage.fordon.Count}");

            var parkedVehicles = Garage.fordon.GroupBy(v => v.GetType().Name)
                                             .ToDictionary(g => g.Key, g => g.Count());

            foreach (var vehicle in parkedVehicles)
            {
                Console.WriteLine($"{vehicle.Key}: {vehicle.Value} st");
            }

            
            int totalSpots = Garage.numberOfParkingSpots;
            int occupiedSpots = Garage.fordon.Count;
            double occupancyRate = (double)occupiedSpots / totalSpots * 100;
            Console.WriteLine($"\nBeläggningsgrad: {occupancyRate:F1}%");

            Console.WriteLine("\nTryck Enter för att återgå...");
            Console.ReadLine();
        }

        public static void ParkingOwner()
        {
            Console.Clear();
            Garage.DisplayGrid();
            Console.WriteLine("Välkommen Karen! (Parkeringschef)");

            // Beräkna intäkter och visa statistik
            var statistics = CalculateStatistics();
            Console.WriteLine($"\nDagens statistik:");
            Console.WriteLine($"Totala intäkter: {statistics.totalRevenue:F2}kr");
            Console.WriteLine($"Antal fordon: {statistics.totalVehicles}");
            Console.WriteLine($"Genomsnittlig parkeringstid: {statistics.averageParkingTime:F1} minuter");
            Console.WriteLine($"Beläggningsgrad: {statistics.occupancyRate:F1}%");

            Console.WriteLine("\nTryck Enter för att återgå...");
            Console.ReadLine();
        }

        private static (double totalRevenue, int totalVehicles, double averageParkingTime, double occupancyRate) CalculateStatistics()
        {
            
            return (totalRevenue: 2450.50,
                    totalVehicles: Garage.fordon.Count,
                    averageParkingTime: 120.0,
                    occupancyRate: (double)Garage.fordon.Count / Garage.numberOfParkingSpots * 100);
        }
    }
}
