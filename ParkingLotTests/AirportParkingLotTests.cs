using ParkingLotTests.Builders;
using ParkingLot.Models;
using ParkingLot.Repositories;
using ParkingLot.Strategy;
using ParkingLot;

namespace ParkingLotTests
{
    [TestClass]
    public class AirportParkingLotTests
    {
        private IParkedVehicleRepository? parkedVehicleRepository;
        private IFeesCalculationStrategy? feesCalculationStrategy;
        private IParkingLot? parkingLot;
        private ParkingLotDetails ? airportParking;

        [TestInitialize]
        public void Setup()
        {
            parkedVehicleRepository = new ParkedVehicleRepository();
            feesCalculationStrategy = new FeesCalculationStrategyAirport();
            var airportParkingBuilder = new LargeParkingLotBuilder();

            //Init parking lot fees model and spots.
            airportParkingBuilder.AddMotorCycleSpots(200);
            airportParkingBuilder.AddCarSpots(500);            
            airportParkingBuilder.AddCarRates(InitCarRates());
            airportParkingBuilder.AddMotorCycleRates(InitMotorcycleRates());         
            airportParking = airportParkingBuilder.Build();
        }

        [TestMethod]
        public void TestAirportParkingLot_TestMultipleScenarios_CalculatesFeesSuccessfully()
        {
            //Arrange         
            parkingLot = new ParkingLot.ParkingLot(parkedVehicleRepository, airportParking, feesCalculationStrategy);
            InitVehicleParkingRecords(VehicleType.Scooter, ParkedVehicleType.Motorcycle, 10); 
            InitVehicleParkingRecords(VehicleType.Car, ParkedVehicleType.Car, 10);

            //Act
            var feesMotorcycle1 = parkingLot.UnparkVehicle("Ticket: 001", VehicleType.Motorcycle, new DateTime(2022, 5, 29, 15, 00, 7));
            var feesMotorcycle2 = parkingLot.UnparkVehicle("Ticket: 002", VehicleType.Motorcycle, new DateTime(2022, 5, 30, 05, 04, 7));
            var feesMotorcycle3 = parkingLot.UnparkVehicle("Ticket: 003", VehicleType.Motorcycle, new DateTime(2022, 5, 31, 02, 06, 7));

            var feesCar1 = parkingLot.UnparkVehicle("Ticket: 001", VehicleType.SUV, new DateTime(2022, 5, 29, 14, 59, 7));
            var feesCar2 = parkingLot.UnparkVehicle("Ticket: 002", VehicleType.SUV, new DateTime(2022, 5, 30, 14, 04, 7));
            var feesCar3 = parkingLot.UnparkVehicle("Ticket: 003", VehicleType.SUV, new DateTime(2022, 6, 1, 15, 06, 7));

            //Assert
            Assert.AreEqual(feesMotorcycle1, 0);
            Assert.AreEqual(feesMotorcycle2, 60);
            Assert.AreEqual(feesMotorcycle3, 160);
            Assert.AreEqual(feesCar1, 60);
            Assert.AreEqual(feesCar2, 80);
            Assert.AreEqual(feesCar3, 400);
        }

        [TestMethod]
        public void TestAirportParkingLot_TestSpotAvailibility_GeneratesTicketsSuccessfully()
        {
            //Arrange         
            parkingLot = new ParkingLot.ParkingLot(parkedVehicleRepository, airportParking, feesCalculationStrategy);
            InitVehicleParkingRecords(VehicleType.Car, ParkedVehicleType.Car, 498);

            //Act
            var ticket1 = parkingLot.ParkVehicle(VehicleType.Car, DateTime.Now);
            var ticket2 = parkingLot.ParkVehicle(VehicleType.Car, DateTime.Now.AddMinutes(10));
            var ticket3 = parkingLot.ParkVehicle(VehicleType.Car, DateTime.Now.AddMinutes(10));
            parkingLot.UnparkVehicle("Ticket: 00477", VehicleType.Car, DateTime.Now.AddMinutes(40));
            parkingLot.UnparkVehicle("Ticket: 00499", VehicleType.Car, DateTime.Now.AddMinutes(40));
            var ticket4 = parkingLot.ParkVehicle(VehicleType.Car, DateTime.Now.AddMinutes(70));
        
            //Assert
            Assert.AreEqual(ticket1, "Ticket: 00499");
            Assert.AreEqual(ticket2, "Ticket: 00500");
            Assert.AreEqual(ticket3, string.Empty);
            Assert.AreEqual(ticket4, "Ticket: 00477");
        }

        private void InitVehicleParkingRecords(VehicleType vehicleType, ParkedVehicleType parkedVehicleType, int noOfSpots)
        {
            if (airportParking.NoOfSpots[parkedVehicleType] >= noOfSpots)
            {
                for (int i = 0; i < noOfSpots; i++)
                {
                    parkingLot.ParkVehicle(vehicleType, new DateTime(2022, 5, 29, 14, 4, 7).AddMinutes(i));
                }
            }
        }

        private IEnumerable<Rates> InitCarRates() 
        {
            return new Rates[] { new Rates(0, 12, 60), new Rates(12, 24, 80), new Rates(24, Int32.MaxValue, 100, false) };    
        }

        private IEnumerable<Rates> InitMotorcycleRates()
        {
            return new Rates[] { new Rates(0, 1, 0), new Rates(1, 8, 40), new Rates(8, 24, 60),new Rates(24, Int32.MaxValue, 80, false) };
        }

        private IEnumerable<Rates> InitTruckRates()
        {
            return new Rates[] { new Rates(0, 1, 0), new Rates(1, 8, 40), new Rates(8, 24, 60), new Rates(24, Int32.MaxValue, 80, false) };
        }
    }
}