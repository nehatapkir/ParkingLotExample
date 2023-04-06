using ParkingLot;
using ParkingLotTests.Builders;
using ParkingLot.Models;
using ParkingLot.Repositories;
using ParkingLot.Strategy;

namespace ParkingLotTests
{
    [TestClass]
    public class MidSizedMallParkingLotTests
    {
        private IParkedVehicleRepository parkedVehicleRepository;
        private IFeesCalculationStrategy feesCalculationStrategy;
        private IParkingLot parkingLocationFactory;
        private MallParkingLotDetails mallParking;

      [TestInitialize]
        public void Setup()
        {
            parkedVehicleRepository = new ParkedVehicleRepository();     
            feesCalculationStrategy = new FeesCalculationStrategyMall();
            var mallParkingLotBuilder = new MallParkingLotBuilder();
            mallParkingLotBuilder.AddMotorCycleSpots(100);
            mallParkingLotBuilder.AddTruckSpots(10);
            mallParkingLotBuilder.AddCarSpots(80);
            mallParkingLotBuilder.SetMotorCycleRates(10);
            mallParkingLotBuilder.SetTruckRates(50);
            mallParkingLotBuilder.SetCarRates(20);
            mallParking = mallParkingLotBuilder.Build();
        }


        [TestMethod]
        public void TestSmallMallParking_TestMultipleScenarios_CalculatesFeesSuccessfully()
        {
            //Arrange
            parkingLocationFactory = new ParkingLot.ParkingLot(parkedVehicleRepository, mallParking, feesCalculationStrategy);
            InitVehicleParkingRecords(VehicleType.Scooter, 50);
            InitVehicleParkingRecords(VehicleType.Truck, 10);
            InitVehicleParkingRecords(VehicleType.Car, 77);

            //Act
            var ticketNumber = parkingLocationFactory.ParkVehicle(VehicleType.Truck, new DateTime(2022, 5, 29, 17, 4, 7));
            var feesTruck = parkingLocationFactory.UnparkVehicle("Ticket: 001", VehicleType.Truck, new DateTime(2022, 5, 29, 16, 03, 7));
            var feesCar = parkingLocationFactory.UnparkVehicle("Ticket: 001", VehicleType.Car, new DateTime(2022, 5, 29, 20, 05, 7));
            var feesMotorcycle = parkingLocationFactory.UnparkVehicle("Ticket: 001", VehicleType.Motorcycle, new DateTime(2022, 5, 29, 17, 34, 7));
            var ticketNumberTruck = parkingLocationFactory.ParkVehicle(VehicleType.Truck, new DateTime(2022, 5, 29, 15, 35, 7));

            //Assert
            Assert.AreNotEqual(ticketNumberTruck, string.Empty);
            Assert.AreEqual(ticketNumber, string.Empty);
            Assert.AreEqual(feesTruck, 100);
            Assert.AreEqual(feesCar, 140);
            Assert.AreEqual(feesMotorcycle, 40);
        }


        private void InitVehicleParkingRecords(VehicleType vehicleType, int noOfSpots)
        {
            for(int i = 0; i < noOfSpots; i++)
            {
               parkingLocationFactory.ParkVehicle(vehicleType, new DateTime(2022, 5, 29, 14, 4, 7).AddMinutes(i));
            }
        }
    }
}