using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public static void VehicleColor(Car[] args, string selectedColorName)
        {
            List<(string name, ConsoleColor color)> colorOptions = new()
            {
                ("röd ", ConsoleColor.Red),
                ("grön ", ConsoleColor.Green),
                ("blå ", ConsoleColor.Blue),
                ("gul ", ConsoleColor.Yellow),
                ("cyan ", ConsoleColor.Cyan),
                ("magenta ", ConsoleColor.Magenta)
            };
        }
    }



}
