using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class Helpers
    {
        public string ElBil { get; set; }
        public string Brand { get; set; }
        public string Seats { get; set; }
        public string Color { get; set; }
        public Helpers(string elBil)
        {
            ElBil = elBil;

        }

        

        public class Garage
        {
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
        public static double CalculateParking(double price)
        {
            price = Math.Abs(price);
            return price * 1.5;
        }
        //public static void VehicleColor(Car[] args, string selectedColorName)
        //{
        //    List<(string name, ConsoleColor color)> colorOptions = new()
        //    {
        //        ("röd ", ConsoleColor.Red),
        //        ("grön ", ConsoleColor.Green),
        //        ("blå ", ConsoleColor.Blue),
        //        ("gul ", ConsoleColor.Yellow),
        //        ("cyan ", ConsoleColor.Cyan),
        //        ("magenta ", ConsoleColor.Magenta)
        //    };
        //}
    }



}
