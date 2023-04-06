using ParkingLot.Models;

namespace ParkingLotTests.Builders
{
    public class ParkingLotBuilder
    {
        protected LocationType LocationType { get; set; }
        protected int NoofSpotsCars { get; set; }
        protected int NoofSpotsTrucks { get; set; }
        protected int NoOfSpotsMotorcyle { get; set; }

        public void AddCarSpots(int carSpots)
        {
            NoofSpotsCars = carSpots;
        }

        public void AddTruckSpots(int truckSpots)
        {
            NoofSpotsTrucks = truckSpots;
        }

        public void AddMotorCycleSpots(int motorCycleSpots)
        {
            NoOfSpotsMotorcyle = motorCycleSpots;
        }

    }
}
