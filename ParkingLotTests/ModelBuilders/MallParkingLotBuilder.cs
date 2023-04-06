using ParkingLot.Models;

namespace ParkingLotTests.Builders
{
    public class MallParkingLotBuilder : ParkingLotBuilder
    {
        private MallParkingLotDetails parkingLot;
        private decimal CarParkingRate;
        private decimal MotorcycleParkingRate;
        private decimal TruckParkingRate;

        public void SetMotorCycleRates(decimal motorcycleRates)
        {
            MotorcycleParkingRate = motorcycleRates;
        }

        public void SetCarRates(decimal carRates)
        {
            CarParkingRate = carRates;
        }

        public void SetTruckRates(decimal truckRates)
        {
            TruckParkingRate = truckRates;
        }

        public virtual MallParkingLotDetails Build()
        {
            parkingLot = new MallParkingLotDetails(LocationType.Mall);
            parkingLot.NoOfSpots.Add(ParkedVehicleType.Car, NoofSpotsCars);
            parkingLot.NoOfSpots.Add(ParkedVehicleType.Motorcycle, NoOfSpotsMotorcyle);
            parkingLot.NoOfSpots.Add(ParkedVehicleType.Truck, NoofSpotsTrucks);
            parkingLot.CarParkingRate = CarParkingRate;
            parkingLot.MotorcycleParkingRate = MotorcycleParkingRate;
            parkingLot.TruckParkingRate = TruckParkingRate;
            return parkingLot;
        }
    }
}
