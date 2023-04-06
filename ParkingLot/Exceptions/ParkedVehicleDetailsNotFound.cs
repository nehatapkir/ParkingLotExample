namespace ParkingLot.Exceptions
{
    public class ParkedVehicleDetailsNotFound : Exception
    {
        public ParkedVehicleDetailsNotFound()
        {
            
        }

        public ParkedVehicleDetailsNotFound(string ticketNumber) 
            : base($"Vehicle details not found for : {ticketNumber}")        
        {

        }

    }
}
