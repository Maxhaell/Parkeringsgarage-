using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class Garage
    {
        public static int[,] garageGrid;
        public static int [,] parkingGrid;
        public static int numberOfParkingSpots = 30;
        public static List<Fordon> fordon = new List<Fordon>();
        public static Random random = new Random();


        public static void GarageGrid()
        {
            garageGrid = new int[30, 85];
            parkingGrid = new int[2, 2] { { 3, 3 }, { 3, 3 } };
            fordon = new List<Fordon>();
            PlaceParkingGrid();
            DisplayGrid();
        }

        public static void PlaceParkingGrid()
        {
            int parkingGridCount = 0;
            int parkingGridRows = parkingGrid.GetLength(0);
            int parkingGridCols = parkingGrid.GetLength(1);

            for (int i = 0; i < garageGrid.GetLength(0); i += parkingGridRows)
            {
                for (int j = 0; j < garageGrid.GetLength(1); j += parkingGridCols)
                {
                    if (parkingGridCount >= numberOfParkingSpots)
                        break;


                    for (int x = 0; x < parkingGridRows && i + x < garageGrid.GetLength(0); x++)
                    {
                        for (int y = 0; y < parkingGridCols && j + y < garageGrid.GetLength(1); y++)
                        {
                            garageGrid[i + x, j + y] = parkingGrid[x, y];
                        }
                    }

                    parkingGridCount++;
                }
            }
        }


        //public static void ParkVehicle(Fordon vehicle, int vehicleValue)
        //{
        //    int row, col;
        //    do
        //    {
        //        row = random.Next(garageGrid.GetLength(0));
        //        col = random.Next(garageGrid.GetLength(1));
        //    }
        //    while (garageGrid[row, col] != 0);

        //    garageGrid[row, col] = vehicleValue;
        //    vehicle.Row = row;
        //    vehicle.Col = col;
        //    fordon.Add(vehicle);
        //}


        public static void DisplayGrid()
        {
            Console.Clear();
            for (int row = 0; row < garageGrid.GetLength(0); row++)
                {
                    for (int col = 0; col < garageGrid.GetLength(1); col++)
                    {
                        switch (garageGrid[row, col])
                        {
                        case 0:
                            Console.Write(". ");
                            break;
                        case 1:
                            Console.Write("C ");
                            break;
                        case 2:
                            Console.Write("M ");
                            break;
                        case 3:
                            Console.Write("P ");
                            break;
                        default:
                                Console.Write("? ");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            Console.WriteLine();
        }
    }
}
