namespace ParkingLot.Models
{
    public class ParkingLotDetails
    {
        public LocationType LocationType { get; set; }

        public Dictionary<ParkedVehicleType, int> NoOfSpots;
        public ParkingLotDetails(LocationType locationType)
        {
            LocationType = locationType;
            NoOfSpots = new Dictionary<ParkedVehicleType, int>();
        }
    }

    public enum LocationType
    {
        Mall,
        Airport,
        Stadium
    }
}
