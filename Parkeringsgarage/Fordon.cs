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

    // The thief
    public class Car : Fordon
    { 
        
        public List<Helpers> ElBil { get; set; } = new List<Helpers>();
        public Car(string regNr, List<Helpers> elBil) : base(regNr)
        {
            ElBil = elBil;
        }
        public override string ToString() => $"Car {RegNr}";

    }
}
