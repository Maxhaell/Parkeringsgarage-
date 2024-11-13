using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class ParkeringsTimer
    {
        public string RegNr { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }

        public ParkeringsTimer(string regNr, int seconds)
        {
            RegNr = regNr;
            EndTime = DateTime.Now.AddSeconds(seconds);
            IsActive = true;
        }
    }
}
