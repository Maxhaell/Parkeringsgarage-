using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class ParkingFine
    {
        public DateTime IssueDate { get; set; }
        public double Amount { get; set; }
        public string Reason { get; set; }
        public bool IsPaid { get; set; }
    }
}
