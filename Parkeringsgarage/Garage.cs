using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class Garage
    {
        public static int[,] garageGrid;
        public static int[,] parkingGrid;
        public static int numberOfParkingSpots = 2;
        public static List<Fordon> fordon = new List<Fordon>();
        public static Random random = new Random();
        private static int currentParkingRow = 1;
        private static int currentParkingCol = 1;
        private static readonly List<ParkingTimer> activeTimers = new List<ParkingTimer>();

        public static void GarageGrid()
        {
            garageGrid = new int[24, 40]; 
            parkingGrid = new int[2, 2] { { 3, 3 }, { 3, 3 } };
            fordon = new List<Fordon>();
            currentParkingRow = 1;
            currentParkingCol = 1;
            PlaceParkingGrid();
            DrawGarageFrame();
            DisplayGrid();
        }

        public static void PlaceParkingGrid()
        {
            // Markera alla möjliga parkeringsplatser
            for (int i = 1; i < garageGrid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < garageGrid.GetLength(1) - 1; j++)
                {
                    if (j % 8 != 0) // Bredare körvägar
                    {
                        garageGrid[i, j] = 3; // Markera som parkeringsplats
                    }
                }
            }
        }

        public static string GetVehicleType(Fordon vehicle)
        {
            if (vehicle is Bus) return "buss";
            if (vehicle is Car) return "bil";
            return "motorcykel";
        }

        public static bool ParkVehicle(string vehicleType, ConsoleColor color, string regNr, out int row, out int col)
        {
            row = -1;
            col = -1;

            while (currentParkingCol < garageGrid.GetLength(1) - 4) 
            {
                if (currentParkingRow >= garageGrid.GetLength(0) - 4) 
                {
                    currentParkingRow = 1;
                    currentParkingCol += 8; 
                    continue;
                }

                bool canPark = true;
                switch (vehicleType.ToLower())
                {
                    case "buss":
                        canPark = CanParkBus(currentParkingRow, currentParkingCol);
                        break;
                    case "bil":
                        canPark = CanParkCar(currentParkingRow, currentParkingCol);
                        break;
                    case "motorcykel":
                        canPark = CanParkMotorcycle(currentParkingRow, currentParkingCol);
                        break;
                }

                if (canPark)
                {
                    row = currentParkingRow;
                    col = currentParkingCol;
                    PlaceVehicle(vehicleType, row, col, color, regNr);
                    currentParkingRow += GetVehicleHeight(vehicleType);
                    return true;
                }

                currentParkingRow += 1;
            }

            return false;
        }

        public static void PlaceVehicle(string vehicleType, int row, int col, ConsoleColor color, string regNr)
        {
            // Kontrollera att positionen är inom giltiga gränser
            if (row < 1 || col < 1 || row >= garageGrid.GetLength(0) - 1 || col >= garageGrid.GetLength(1) - 1)
                return;

            Fordon vehicle;
            switch (vehicleType.ToLower())
            {
                case "bil":
                    // Kontrollera utrymme för bil (2x2)
                    if (row + 1 >= garageGrid.GetLength(0) - 1 || col + 1 >= garageGrid.GetLength(1) - 1)
                        return;

                    vehicle = new Car(regNr, color, new List<Helpers>());
                    // Placera bilen på lediga parkeringsrutor
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (garageGrid[row + i, col + j] != 3) // Kontrollera att rutan är ledig
                                return;
                            garageGrid[row + i, col + j] = 4;
                        }
                    }
                    break;

                case "motorcykel":
                    vehicle = new Motorcycle(regNr, color, new List<Helpers>());
                    for (int j = 0; j < 2; j++)
                    {
                        if (garageGrid[row, col + j] != 3)
                            return;
                        garageGrid[row, col + j] = 2;
                    }
                    break;

                case "buss":
                    // Kontrollera utrymme för buss (4x2)
                    if (row + 3 >= garageGrid.GetLength(0) - 1 || col + 1 >= garageGrid.GetLength(1) - 1)
                        return;

                    vehicle = new Bus(regNr, color, new List<Helpers>());
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (garageGrid[row + i, col + j] != 3)
                                return;
                            garageGrid[row + i, col + j] = 6;
                        }
                    }
                    break;

                default:
                    return;
            }

            vehicle.Row = row;
            vehicle.Col = col;
            fordon.Add(vehicle);
        }

        private static bool CanParkBus(int row, int col)
        {
            if (row + 3 >= garageGrid.GetLength(0) || col + 3 >= garageGrid.GetLength(1))
                return false;

            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (garageGrid[row + i, col + j] != 3)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool CanParkCar(int row, int col)
        {
            if (row >= garageGrid.GetLength(0) || col + 1 >= garageGrid.GetLength(1))
                return false;

            
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (garageGrid[row + i, col + j] != 3)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool CanParkMotorcycle(int row, int col)
        {
            if (row >= garageGrid.GetLength(0) || col + 1 >= garageGrid.GetLength(1))
                return false;

            
            for (int j = 0; j < 2; j++)
            {
                if (garageGrid[row, col + j] != 3)
                {
                    return false;
                }
            }
            return true;
        }

        private static int GetVehicleHeight(string vehicleType)
        {
            switch (vehicleType.ToLower())
            {
                case "bus":
                    return 4;
                case "car":
                    return 2;
                case "motorcycle":
                    return 1;
                default:
                    return 1;
            }
        }

        private static int GetVehicleWidth(string vehicleType)
        {
            switch (vehicleType.ToLower())
            {
                case "bus":
                    return 2;
                case "car":
                    return 2;
                case "motorcycle":
                    return 2;
                default:
                    return 1;
            }
        }

        private static void DrawGarageFrame()
        {
            
            for (int i = 0; i < garageGrid.GetLength(0); i++)
            {
                garageGrid[i, 0] = 4;
                garageGrid[i, garageGrid.GetLength(1) - 1] = 4;
            }
            for (int j = 0; j < garageGrid.GetLength(1); j++)
            {
                garageGrid[0, j] = 4;
                garageGrid[garageGrid.GetLength(0) - 1, j] = 4;
            }

            
            for (int j = 15; j < 25; j++)
            {
                garageGrid[garageGrid.GetLength(0) - 1, j] = 5;
            }
        }


        public static void DisplayGrid()
        {
            Console.Clear();
            int width = garageGrid.GetLength(1);
            int height = garageGrid.GetLength(0);

            // Rita övre ramen

            Console.Write("┌");
            for (int i = 0; i < width - 2; i++) Console.Write("─");
            Console.WriteLine("┐");


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // Hitta eventuellt fordon på denna position

                    var vehicle = fordon.FirstOrDefault(v =>
                        v.Row <= i && i < v.Row + GetVehicleHeight(v.GetType().Name.ToLower()) &&
                        v.Col <= j && j < v.Col + GetVehicleWidth(v.GetType().Name.ToLower()));
                    

                    if (j == 0) // Vänster kant
                    {
                        Console.Write("│");
                    }
                    else if (j == width - 1) // Höger kant
                    {
                        Console.Write("│");
                    }
                    else if (vehicle != null)
                    {
                        
                        // Visa fordonet i dess valda färg och med korrekt symbol
                        Console.ForegroundColor = vehicle.Color;
                        
                        switch (vehicle.GetType().Name.ToLower())
                        {
                            case "car":
                                Console.Write("C"); // Car symbol
                                break;
                            case "motorcycle":
                                Console.Write("M"); // Motorcycle symbol
                                break;
                            case "bus":
                                Console.Write("B"); // Bus symbol
                                break;
                        }
                        Console.ResetColor();
                        
                    }

                    else
                    {
                        
                        // Visa olika typer av rutor baserat på grid-värdet
                        switch (garageGrid[i, j])
                        {
                            case 0: // Tom yta
                                Console.Write(" ");
                                break;
                            case 1: // Vertikal vägg
                                Console.Write("│");
                                break;
                            case 2: // Horisontell vägg
                                Console.Write("─");
                                break;
                            case 3: // Ledig parkeringsplats
                                Console.Write("·");
                                break;
                            case 4: // Upptagen parkeringsplats
                                Console.Write("■");
                                break;
                            case 5: // Körväg
                                Console.Write(" ");
                                break;
                            case 6: // Parkerat fordon (används ej nu när vi har fordonslistan)
                                Console.Write("■");
                                break;
                            default:
                                Console.Write(" ");
                                break;

                        }
                    }
                }
                Console.WriteLine(); // Ny rad efter varje rad i griden
               
                
            }

            
            Console.Write("└");
            for (int i = 0; i < width - 2; i++) Console.Write("─");
            Console.WriteLine("┘");

            //// Visa en förklaring av symbolerna
            //Console.WriteLine("\nFörklaring:");
            //Console.WriteLine("· = Ledig parkeringsplats");
            //Console.WriteLine("█ = Parkerat fordon");
            //Console.WriteLine("│ = Vägg");
            //Console.WriteLine("─ = Vägg");
        }


        public static double CalculateOccupancyRate()
        {
            int totalParkingSpaces = 0;
            int occupiedSpaces = 0;

            // Räkna totala antalet parkeringsrutor
            for (int i = 1; i < garageGrid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < garageGrid.GetLength(1) - 1; j++)
                {
                    if (garageGrid[i, j] == 3 || 
                        garageGrid[i, j] == 1 || 
                        garageGrid[i, j] == 2 || 
                        garageGrid[i, j] == 6)   
                    {
                        totalParkingSpaces++;
                    }
                }
            }

            // Räkna upptagna parkeringsrutor
            foreach (var fordon in fordon)
            {
                if (fordon is Car)
                {
                    occupiedSpaces += 0;  
                }
                else if (fordon.GetType().Name == "Motorcykel")
                {
                    occupiedSpaces += 0;  
                }
                else 
                {
                    occupiedSpaces += 0; 
                }
            }

            return totalParkingSpaces > 0 ? ((double)occupiedSpaces / totalParkingSpaces) * 100 : 0;
        }

        public static void StartParkingTimer(Fordon vehicle, int seconds)
        {
            var timer = new ParkingTimer(vehicle.RegNr, seconds);
            vehicle.Timer = timer;
            activeTimers.Add(timer);

            // Starta en ny tråd för att hantera nedräkningen
            Task.Run(() => RunTimer(vehicle));
        }

        private static async Task RunTimer(Fordon vehicle)
        {
            while (vehicle.Timer.IsActive)
            {
                var timeLeft = vehicle.Timer.EndTime - DateTime.Now;

                if (timeLeft.TotalSeconds <= 0)
                {
                    vehicle.Timer.IsActive = false;
                    vehicle.Timer.HasExpired = true; // Ny property för att markera utgången tid

                    // Spara tiden när parkeringen löpte ut
                    vehicle.Timer.ExpiredAt = DateTime.Now;

                    Console.SetCursorPosition(0, Console.WindowHeight - 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Parkering har löpt ut för fordon: {vehicle.RegNr}!");
                    Console.ResetColor();
                    break;
                }

                UpdateTimerDisplay(vehicle, timeLeft);
                await Task.Delay(1000);
            }
        }

        private static void UpdateTimerDisplay(Fordon vehicle, TimeSpan timeLeft)
        {
            // Spara nuvarande cursorposition
            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;

            // Flytta till timer-området (under garagevisningen)
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write(new string(' ', Console.WindowWidth)); // Rensa raden
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine($"Tid kvar för {vehicle.RegNr}: {timeLeft.Minutes:D2}:{timeLeft.Seconds:D2}");

            // Återställ cursorposition
            Console.SetCursorPosition(originalLeft, originalTop);
        }

        public static void DisplayActiveTimers()
        {
            Console.WriteLine("\nAktiva parkeringar:");
            foreach (var timer in activeTimers.Where(t => t.IsActive))
            {
                var timeLeft = timer.EndTime - DateTime.Now;
                if (timeLeft.TotalSeconds > 0)
                {
                    Console.WriteLine($"{timer.RegNr}:{timeLeft.Minutes:D2}:{timeLeft.Seconds:D2}");
                }
            }
        }

        
        
            public static double CalculateParking(Fordon vehicle)
            {
                if (vehicle.Timer == null) return 0;

                double pricePerMinute = vehicle switch
                {
                    Car => 2.0,
                    Bus => 4.0,
                    Motorcycle => 1.0,
                    _ => 2.0
                };

                return 60 * pricePerMinute; // För en timme
            }
        

    }
}


    




