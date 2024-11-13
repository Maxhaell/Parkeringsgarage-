using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Parkeringsgarage
{
    public class Fordon
    {
        public string RegNr { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public ConsoleColor Color { get; set; }

        public Fordon(string regNr, ConsoleColor color)
        {
            RegNr = regNr;
            Color = color;
        }
    }

    public class Car : Fordon
    {
        public List<Helpers> ElBil { get; set; } = new List<Helpers>();
        public Car(string regNr, ConsoleColor color, List<Helpers> elBil) : base(regNr, color)
        {
            ElBil = elBil;
        }
        public override string ToString() => $"Car {RegNr}";
    }

    public class Motorcycle : Fordon
    {
        public List<Helpers> Brand { get; set; } = new List<Helpers>();
        public Motorcycle(string regNr, ConsoleColor color, List<Helpers> brand) : base(regNr, color)
        {
            Brand = brand;
        }
        public override string ToString() => $"Motorcycle {RegNr}";
    }

    public class Bus : Fordon
    {
        public List<Helpers> Seats { get; set; } = new List<Helpers>();
        public Bus(string regNr, ConsoleColor color, List<Helpers> seats) : base(regNr, color)
        {
            Seats = seats;
        }
        public override string ToString() => $"Bus {RegNr}";
    }
}