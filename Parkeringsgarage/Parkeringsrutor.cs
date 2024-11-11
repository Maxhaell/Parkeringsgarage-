using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Parkeringsgarage
//{

//    public class Parkeringsrutor : Garage
//    {
//        public static int currentParkingRow = 1;
//        public static int currentParkingCol = 1;
//        public static bool ParkVehicle(string vehicleType, out int row, out int col)
//        {
            
//            row = -1;
//            col = -1;

//            while (currentParkingCol < garageGrid.GetLength(1) - 4)
//                if (currentParkingRow >= garageGrid.GetLength(0) - 4)
//                {
//                    currentParkingRow = 1;
//                    currentParkingCol += 8;
//                    continue;
//                }

//            bool canPark = true;

//            switch (vehicleType.ToLower())
//            {
//                case "buss":
//                    canPark = CanParkBus(currentParkingRow, currentParkingCol);
//                    break;

//                case "bil":
//                    canPark = CanParkCar(currentParkingRow, currentParkingCol);
//                    break;

//                case "motorcykel":
//                    canPark = CanParkMotorcycle(currentParkingRow, currentParkingCol);
//                    break;
//            }

//            if (canPark)
//            {
//                row = currentParkingRow;
//                col = currentParkingCol;

//                PlaceVehicle(vehicleType, row, col);

//                currentParkingRow += GetVehicleHeight(vehicleType);
//                return false;
//            }

            

//        }

//        internal static void ParkVehicle(Car car, int v)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
