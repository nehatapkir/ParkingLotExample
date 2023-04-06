using ParkingLot;
using ParkingLotTests.Builders;
using ParkingLot.Models;
using ParkingLot.Repositories;
using ParkingLot.Strategy;
using ParkingLot.Exceptions;

namespace ParkingLotTests
{
    [TestClass]
    public class SmallMallParkingLotTests
    {
        private IParkedVehicleRepository parkedVehicleRepository;
        private IFeesCalculationStrategy feesCalculationStrategy;
        private IParkingLot parkingLocationFactory;

       [TestInitialize]
        public void Setup()
        {
            parkedVehicleRepository = new ParkedVehicleRepository();     
            feesCalculationStrategy = new FeesCalculationStrategyMall();                  
        }

        [TestMethod]
        public void TestSmallMallParking_ParkSingleMotorcycle_CalculatesFeesSuccessfully()
        {
            //Arrange
            var mallParkingLotBuilder = new MallParkingLotBuilder();
            mallParkingLotBuilder.AddCarSpots(2);
            mallParkingLotBuilder.SetCarRates(20);
            var mallParking = mallParkingLotBuilder.Build();
            parkingLocationFactory = new ParkingLot.ParkingLot(parkedVehicleRepository, mallParking, feesCalculationStrategy);

            //Act
            var ticketNumber1 = parkingLocationFactory.ParkVehicle(VehicleType.Car, new DateTime(2023, 12, 01, 10, 50, 00));
            var fees = parkingLocationFactory.UnparkVehicle(ticketNumber1, VehicleType.Car, new DateTime(2023, 12, 01, 12, 55, 00));

            //Assert
            Assert.AreNotEqual(ticketNumber1, string.Empty);
            Assert.AreEqual(fees, 60);
        }

        [TestMethod]
        public void TestSmallMallParking_ParkMultipleMotorcycle_CalculatesFeesSuccessfully()
        {
            //Arrange
            var mallParkingLotBuilder = new MallParkingLotBuilder();
            mallParkingLotBuilder.AddCarSpots(2);
            mallParkingLotBuilder.SetCarRates(20);
            var mallParking = mallParkingLotBuilder.Build();
            parkingLocationFactory = new ParkingLot.ParkingLot(parkedVehicleRepository, mallParking, feesCalculationStrategy);
            
            //Act
            var ticketNumber1 = parkingLocationFactory.ParkVehicle(VehicleType.Car, new DateTime(2023, 12, 01, 10, 50, 00));
            var ticketNumber2 = parkingLocationFactory.ParkVehicle(VehicleType.Car, new DateTime(2023, 12, 01, 10, 55, 00));
            var fees = parkingLocationFactory.UnparkVehicle(ticketNumber1, VehicleType.Car, new DateTime(2023, 12, 01, 12, 55, 00));
            var ticketNumber3 = parkingLocationFactory.ParkVehicle(VehicleType.Car, new DateTime(2023, 12, 01, 1, 55, 00));
            
            //Assert
            Assert.AreNotEqual(ticketNumber1, string.Empty);
            Assert.AreNotEqual(ticketNumber2, string.Empty);
            Assert.AreNotEqual(ticketNumber3, string.Empty);
            Assert.AreEqual(fees, 60);
        }

        [TestMethod]
        public void TestSmallMallParking_TestMultipleScenarios_CalculatesFeesSuccessfully()
        {
            //Arrange
            var mallParkingLotBuilder = new MallParkingLotBuilder();
            mallParkingLotBuilder.AddMotorCycleSpots(2);
            mallParkingLotBuilder.SetMotorCycleRates(10);
            var mallParking = mallParkingLotBuilder.Build();
            parkingLocationFactory = new ParkingLot.ParkingLot(parkedVehicleRepository, mallParking, feesCalculationStrategy);
           
            //Act
            var ticketNumber1 = parkingLocationFactory.ParkVehicle(VehicleType.Scooter, new DateTime(2022, 5, 29, 14, 4, 7));
            var ticketNumber2 = parkingLocationFactory.ParkVehicle(VehicleType.Motorcycle, new DateTime(2022, 5, 29, 14, 44, 7));
            var ticketNumber3 = parkingLocationFactory.ParkVehicle(VehicleType.Scooter, new DateTime(2022, 5, 29, 15, 39, 7));
            parkingLocationFactory.UnparkVehicle(ticketNumber2, VehicleType.Motorcycle, new DateTime(2022, 5, 29, 15, 40, 7));
            var ticketNumber4 = parkingLocationFactory.ParkVehicle(VehicleType.Motorcycle, new DateTime(2022, 5, 29, 15, 35, 7));
            var fees1 = parkingLocationFactory.UnparkVehicle(ticketNumber1, VehicleType.Scooter, new DateTime(2022, 5, 29, 17, 44, 7));
            
            //Assert
            Assert.AreNotEqual(ticketNumber1, string.Empty);
            Assert.AreNotEqual(ticketNumber2, string.Empty);
            Assert.AreEqual(ticketNumber3, string.Empty);
            Assert.AreNotEqual(ticketNumber4, string.Empty);
            Assert.AreEqual(fees1, 40);
        }

        [TestMethod]
        [ExpectedException(typeof(ParkedVehicleDetailsNotFound))]
        public void TestSmallMallParking_TestUnparkCar_ThrowsExceptionIfTicketNotFound()
        {
            //Arrange
            var mallParkingLotBuilder = new MallParkingLotBuilder();
            mallParkingLotBuilder.AddMotorCycleSpots(2);
            mallParkingLotBuilder.SetMotorCycleRates(10);
            var mallParking = mallParkingLotBuilder.Build();
            parkingLocationFactory = new ParkingLot.ParkingLot(parkedVehicleRepository, mallParking, feesCalculationStrategy);

            //Act
            parkingLocationFactory.ParkVehicle(VehicleType.Scooter, new DateTime(2022, 5, 29, 14, 4, 7));
            parkingLocationFactory.ParkVehicle(VehicleType.Motorcycle, new DateTime(2022, 5, 29, 14, 44, 7));
           
            parkingLocationFactory.UnparkVehicle("23444", VehicleType.Motorcycle, new DateTime(2022, 5, 29, 15, 40, 7));
        }
    }
}