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
        public static int numberOfParkingSpots = 75;
        public static List<Fordon> fordon = new List<Fordon>();
        public static Random random = new Random();
        public static int currentParkingRow = 1;
        public static int currentParkingCol = 1;

        public static void GarageGrid()
        {
            garageGrid = new int[24, 40];
            parkingGrid = new int[2, 2] { { 3, 3 }, { 3, 3 } };
            fordon = new List<Fordon>();
            PlaceParkingGrid();
            DisplayGrid();
            DrawGarageFrame();
        }

        public static void PlaceParkingGrid()
        {


            for (int i = 0; i < garageGrid.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < garageGrid.GetLength(1) - 1; j++)
                {
                    if (j % 8 != 0)
                    {
                        garageGrid[i, j] = 3;
                    }
                }
            }
        }

        public static void PlaceVehicle(string vehicleType, int row, int col)
        {
            switch (vehicleType.ToLower())
            {
                case "buss":

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            garageGrid[row + i, col + j] = 6;
                        }
                    }
                    break;

                case "bil":

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            garageGrid[row + i, col + j] = 1;
                        }
                    }
                    break;

                case "Motorcykel":

                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            garageGrid[row + i, col + j] = 2;
                        }
                    }
                    break;
            }
        }


        public static bool CanParkBus(int row, int col)
        {
            // Kontrollera 4x4 område
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (row + i >= garageGrid.GetLength(0) ||
                        col + j >= garageGrid.GetLength(1) ||
                        garageGrid[row + i, col + j] != 3)
                     {
                         return false;
                     }
                }
            }
            return true;
        }

        public static bool CanParkCar(int row, int col)
        {
            // Kontrollera 4x4 område
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (row + i >= garageGrid.GetLength(0) ||
                        col + j >= garageGrid.GetLength(1) ||
                        garageGrid[row + i, col + j] != 3)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool CanParkMotorcycle(int row, int col)
        {
            return  row < garageGrid.GetLength(0) && 
                col < garageGrid.GetLength(1) &&
                garageGrid[row, col] ==3;
        }

        public static int GetVehicleHeight(string vehicleType)
        {
            return vehicleType.ToLower()
                switch
            {
                "Buss" => 4,
                "Bil" => 2,
                "Motorcykel" => 1,
            };
        }

        public static void DrawGarageFrame()
        {
            for (int i = 0; i < garageGrid.GetLength(0); i++)
            {
                garageGrid[i, 0] = 4;
                garageGrid[i, garageGrid.GetLength (1) - 1] = 4;
            }

            for (int j = 0; j < garageGrid.GetLength(1); j++)
            {
                garageGrid [j, 0] = 4;
                garageGrid [garageGrid.GetLength(1) - 1, j] = 4;
            }

            for (int j = 15; j < 25; j++)
            {
                garageGrid[garageGrid.GetLength(0) - 1, j] = 5;
            }
        }

        public static void DisplayGrid()
        {
            Console.Clear();
            Console.WriteLine("\n === Välkommen TILL SMART PARKING ===\n");

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
                            Console.Write("▓▓");
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

            // Legend
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
            Console.Write("▓▓ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- Motorcykel (1x1)  ");

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
    }

    
   
}

    




