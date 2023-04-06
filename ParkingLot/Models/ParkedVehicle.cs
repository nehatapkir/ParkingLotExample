namespace ParkingLot.Models
{
    public class ParkedVehicle
    {
        public DateTime TimeIn { get; set; }

        public ParkedVehicleType VehicleType { get; set; }  

        public string TicketNumber { get; }

        public int SpotNumber { get; set; }

        public ParkedVehicle(DateTime timeIn, ParkedVehicleType vehicle, int spotNumber) 
        {
            TimeIn = timeIn;
            VehicleType = vehicle;
            SpotNumber = spotNumber;
            TicketNumber = $"Ticket: 00{spotNumber}";
        }
    }


    public enum VehicleType
    {
        Motorcycle,
        Car,
        Truck,
        Scooter,
        Bus,
        SUV
    }

    public enum ParkedVehicleType
    {
        Motorcycle,
        Car,
        Truck
    }
}
