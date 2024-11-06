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
        public static List<Fordon> fordon = new List<Fordon>();
        public static Random random = new Random();


        public static void GarageGrid()
        {
            
            garageGrid = new int[30, 85];
            fordon = new List<Fordon>();
            DisplayGrid();
        }


        public static void ParkVehicle(Fordon person, int personValue)
        {
            int row, col;
            do
            {
                row = random.Next(garageGrid.GetLength(0));
                col = random.Next(garageGrid.GetLength(1));
            }
            while (garageGrid[row, col] != 0);

            garageGrid[row, col] = personValue;
            person.Row = row;
            person.Col = col;
            fordon.Add(person);
        }


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
