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
                        ;
                        break;
                    case '2':
                        ;
                        break;
                    case '3':
                        ;
                        break;
                }
            }
        }
    }
}
