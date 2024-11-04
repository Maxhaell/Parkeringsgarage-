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


        public static void GarageGrid()
        {
            garageGrid = new int[100, 25];

            while (true)
            {
                
                DisplayGrid();
               
                Console.Clear();
            }
        }


        public static void DisplayGrid()
        {
            bool minbool=true;
            while (minbool) 
            for (int row = 0; row < garageGrid.GetLength(0); row++)
            {
                for (int col = 0; col < garageGrid.GetLength(1); col++)
                {
                    switch (garageGrid[row, col])
                    {
                        case 0:
                            Console.Write(". ");
                            
                            break;
                           
                        default:
                            Console.Write("? ");

                            break;
                    }
                    
                }  
                Console.WriteLine();
              
            }
        }
    }
}
