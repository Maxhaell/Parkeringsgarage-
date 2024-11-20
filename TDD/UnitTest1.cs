using Parkeringsgarage;
using System.Drawing;

namespace TDD
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ParkingPriceTest()
        {
            double expected = 150;
            double result = Parkeringsgarage.Helpers.CalculateParking(100);
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void CalculateParking_CarWithOneHour_Returns120Kr()
        {
            var car = new Car("ABC123", ConsoleColor.Red, new List<Helpers>());
            car.Timer = new ParkingTimer("ABC123", 60);

            double cost = Garage.CalculateParking(car);

            Assert.AreEqual(120.0, cost);
        }


        [TestMethod]
        public void RegesterVehicleTest()
        {
       
            var car = new Car("ABC123", Console.ForegroundColor, new List<Helpers>());
            var motorcycle = new Motorcycle("DEF456", Console.ForegroundColor, new List<Helpers>());
            var bus = new Bus("GHI789", Console.ForegroundColor, new List<Helpers>());

            var carString = car.ToString();
            var motorcycleString = motorcycle.ToString();
            var busString = bus.ToString();

            Assert.AreEqual("Car ABC123", carString);
            Assert.AreEqual("Motorcycle DEF456", motorcycleString);
            Assert.AreEqual("Bus GHI789", busString);
        }
    }
}