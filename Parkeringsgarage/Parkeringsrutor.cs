using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringsgarage
{
    public class Parkeringsrutor
    {
        
        public static void ParkVehicle(Fordon vehicle, int vehicleValue)
        {
            int row, col;
            do
            {
                row = Garage.random.Next(Garage.garageGrid.GetLength(0));
                col = Garage.random.Next(Garage.garageGrid.GetLength(1));
            }
            while (Garage.garageGrid[row, col] != 3);

            Garage.garageGrid[row, col] = vehicleValue;
            vehicle.Row = row;
            vehicle.Col = col;
            Garage.fordon.Add(vehicle);
        }
    }
}
