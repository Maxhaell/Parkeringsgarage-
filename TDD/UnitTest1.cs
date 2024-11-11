using Parkeringsgarage;
using System.Drawing;

namespace TDD
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ParkTimeTest()
        {
            int expected = invalidNumber;
            int result = Parkeringsgarage.Incheckning.HandleVehicleCheckin(parkTime);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void VehicleColorTest()
        {
            Car expected = Console.ForegroundColor = ConsoleColor.Blue; 
            Car result = Parkeringsgarage.Helpers.VehicleColor(Console.ForegroundColor = ConsoleColor.Blue);
            Assert.AreEqual(expected, result);
        }
    }

}