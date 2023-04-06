namespace ParkingLot.Exceptions
{
    public class CalculationsError : Exception
    {
        public CalculationsError() : base("An error occured while calculating the car fees.") 
        {
            
        }
    }
}
