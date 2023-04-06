namespace ParkingLot.Models
{
    public class MallParkingLotDetails : ParkingLotDetails
    {
        public MallParkingLotDetails(LocationType locationType) :base(locationType)
        {
            
        }

        public decimal CarParkingRate { get; set; }
        public decimal MotorcycleParkingRate { get; set; }
        public decimal TruckParkingRate { get; set; }
    }
}
