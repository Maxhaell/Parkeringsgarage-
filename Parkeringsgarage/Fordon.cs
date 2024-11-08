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

        public Fordon(string regNr)
        {
            RegNr = regNr;
        }
    }

    public class Car : Fordon
    { 
        
        public List<Helpers> ElBil { get; set; } = new List<Helpers>();
        public Car(string regNr, List<Helpers> elBil) : base(regNr)
        {
            ElBil = elBil;
        }
        public override string ToString() => $"Car {RegNr}";

    }

    public class Motorcycle : Fordon
    {

        public List<Helpers> Brand { get; set; } = new List<Helpers>();
        public Motorcycle(string regNr, List<Helpers> brand) : base(regNr)
        {
            Brand = brand;
        }
        public override string ToString() => $"Motorcycle {RegNr}";
      

    }

    public class Bus : Fordon
    {

        public List<Helpers> Seats { get; set; } = new List<Helpers>();
        public Bus(string regNr, List<Helpers> seats) : base(regNr)
        {
            Seats = seats;
        }
        public override string ToString() => $"Bus {RegNr}";


    }

}
