using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class Helpers
    {
        public string ElBil { get; set; }
        public string Brand { get; set; }
        public string Seats { get; set; }
        public Helpers(string elBil)
        {
            ElBil = elBil;
            
        }
    }



}
