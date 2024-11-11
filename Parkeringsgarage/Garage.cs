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
        public static int numberOfParkingSpots = 20;
        public static List<Fordon> fordon = new List<Fordon>();
        public static Random random = new Random();
        private static int currentParkingRow = 1;
        private static int currentParkingCol = 1;

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

        public static bool ParkVehicle(string vehicleType, out int row, out int col)
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
                    PlaceVehicle(vehicleType, row, col);
                    currentParkingRow += GetVehicleHeight(vehicleType);
                    return true;
                }

                currentParkingRow += 1;
            }

            return false;
        }

        private static void PlaceVehicle(string vehicleType, int row, int col)
        {
            switch (vehicleType.ToLower())
            {
                case "buss":
                    // Buss (4x4)
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            garageGrid[row + i, col + j] = 6;
                        }
                    }
                    break;

                case "bil":
                    // Bil (2x2)
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            garageGrid[row + i, col + j] = 1;
                        }
                    }
                    break;

                case "motorcykel":
                    // Motorcykel (1x2)
                    for (int j = 0; j < 2; j++)
                    {
                        garageGrid[row, col + j] = 2;
                    }
                    break;
            }
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
            if (row + 1 >= garageGrid.GetLength(0) || col + 1 >= garageGrid.GetLength(1))
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
            return vehicleType.ToLower() switch
            {
                "buss" => 4,
                "bil" => 2,
                "motorcykel" => 1,
                _ => 1
            };
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
            Console.WriteLine("\n    === VÄLKOMMEN TILL PARKERINGSHUSET ===\n");

            for (int row = 0; row < garageGrid.GetLength(0); row++)
            {
                Console.Write("    ");
                for (int col = 0; col < garageGrid.GetLength(1); col++)
                {
                    switch (garageGrid[row, col])
                    {
                        case 0: // Körväg
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("░░");
                            break;
                        case 1: // Bil
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("██");
                            break;
                        case 2: // Motorcykel
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("██");
                            break;
                        case 3: // Ledig plats
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("□□");
                            break;
                        case 4: // Väggar
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("██");
                            break;
                        case 5: // Ingång
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("▒▒");
                            break;
                        case 6: // Buss
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("██");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("??");
                            break;
                    }
                }
                Console.WriteLine();
            }

            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n    Legend:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("    □□ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- Ledig plats  ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("██ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- Bil (2x2)  ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("██ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- Motorcykel (1x2)  ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("██ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("- Buss (4x4)");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    ██ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- Vägg  ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("░░ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- Körväg  ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("▒▒ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("- Ingång");
            Console.WriteLine();
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
                    occupiedSpaces += 4;  
                }
                else if (fordon.GetType().Name == "Motorcykel")
                {
                    occupiedSpaces += 2;  
                }
                else 
                {
                    occupiedSpaces += 16; 
                }
            }

            return totalParkingSpaces > 0 ? ((double)occupiedSpaces / totalParkingSpaces) * 100 : 0;
        }
    }
}

    




