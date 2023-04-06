using ParkingLot.Models;

namespace ParkingLotTests.Builders
{
    public class LargeParkingLotBuilder : ParkingLotBuilder
    {
        private LargeParkingLotDetails parkingLot;
        private IEnumerable<Rates> TruckRates;
        private IEnumerable<Rates> CarRates;
        private IEnumerable<Rates> MotorcycleRates;

        public void AddTruckRates(IEnumerable<Rates> truckRates)
        {
            TruckRates = truckRates;
        }

        public void AddCarRates(IEnumerable<Rates> carRates)
        {
            CarRates = carRates;
        }

        public void AddMotorCycleRates(IEnumerable<Rates> motorcycleRates)
        {
            MotorcycleRates = motorcycleRates;
        }

        public virtual ParkingLotDetails Build()
        {
            parkingLot = new LargeParkingLotDetails(LocationType);
            parkingLot.NoOfSpots.Add(ParkedVehicleType.Car, NoofSpotsCars);
            parkingLot.NoOfSpots.Add(ParkedVehicleType.Motorcycle, NoOfSpotsMotorcyle);
            parkingLot.NoOfSpots.Add(ParkedVehicleType.Truck, NoofSpotsTrucks);
            parkingLot.CarRates = CarRates;
            parkingLot.MotorcycleRates = MotorcycleRates;
            parkingLot.TruckRates = TruckRates;
            return parkingLot;
        }
    }
}
