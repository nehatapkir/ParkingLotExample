using ParkingLot;
using ParkingLotTests.Builders;
using ParkingLot.Models;
using ParkingLot.Repositories;
using ParkingLot.Strategy;

namespace ParkingLotTests
{
    [TestClass]
    public class StadiumParkingLotTests
    {
        private IParkedVehicleRepository parkedVehicleRepository;
        private IFeesCalculationStrategy feesCalculationStrategy;
        private IParkingLot parkingLot;
        private ParkingLotDetails stadiumParking;

        [TestInitialize]
        public void Setup()
        {
            parkedVehicleRepository = new ParkedVehicleRepository();
            feesCalculationStrategy = new FeesCalculationStrategyStadium();
            var stadiumParkingBuilder = new LargeParkingLotBuilder();
            stadiumParkingBuilder.AddMotorCycleSpots(1000);
            stadiumParkingBuilder.AddCarSpots(1500);
            stadiumParkingBuilder.AddCarRates(InitCarRates());
            stadiumParkingBuilder.AddMotorCycleRates(InitMotorcycleRates());
            stadiumParking = stadiumParkingBuilder.Build();
        }

        [TestMethod]
        public void TestStadiumParkingLot_TestMultipleScenarios_CalculatesFeesSuccessfully()
        {
            //Arrange
            parkingLot = new ParkingLot.ParkingLot(parkedVehicleRepository, stadiumParking, feesCalculationStrategy);
            InitVehicleParkingRecords(VehicleType.Scooter, ParkedVehicleType.Motorcycle, 10); 
            InitVehicleParkingRecords(VehicleType.Car, ParkedVehicleType.Car, 20);
            
            //Act
            var feesMotorcycle1 = parkingLot.UnparkVehicle("Ticket: 001", VehicleType.Motorcycle, new DateTime(2022, 5, 29, 17, 44, 7));
            var feesMotorcycle2 = parkingLot.UnparkVehicle("Ticket: 002", VehicleType.Motorcycle, new DateTime(2022, 5, 30, 05, 04, 7));

            var feesCar1 = parkingLot.UnparkVehicle("Ticket: 001", VehicleType.SUV, new DateTime(2022, 5, 30, 01, 34, 7));
            var feesCar2 = parkingLot.UnparkVehicle("Ticket: 002", VehicleType.SUV, new DateTime(2022, 5, 30, 03, 10, 7));

            //Assert
            Assert.AreEqual(feesMotorcycle1, 30);
            Assert.AreEqual(feesMotorcycle2, 390);
            Assert.AreEqual(feesCar1, 180);
            Assert.AreEqual(feesCar2, 580);
        }

        private void InitVehicleParkingRecords(VehicleType vehicleType, ParkedVehicleType parkedVehicleType, int noOfSpots)
        {
            if (stadiumParking.NoOfSpots[parkedVehicleType] >= noOfSpots)
            {
                for (int i = 0; i < noOfSpots; i++)
                {
                    parkingLot.ParkVehicle(vehicleType, new DateTime(2022, 5, 29, 14, 4, 7).AddMinutes(i));
                }
            }
        }

        private IEnumerable<Rates> InitCarRates() 
        {
            return new Rates[] { new Rates(0, 4, 60), new Rates(4, 12, 120), new Rates(12, Int32.MaxValue, 200, false) };    
        }

        private IEnumerable<Rates> InitMotorcycleRates()
        {
            return new Rates[] { new Rates(0, 4, 30), new Rates(4, 12, 60), new Rates(12, Int32.MaxValue, 100, false) };
        }
    }
}