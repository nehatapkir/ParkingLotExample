using ParkingLot.Helpers;
using ParkingLot.Models;

namespace ParkingLot.Strategy
{
    public class FeesCalculationStrategyMall : IFeesCalculationStrategy
    {
        public decimal? CalculateFees(ParkingLotDetails parkingLotDetails, ParkedVehicle parkedVehicleDetails, DateTime timeOut)
        {
            var totalHours = DateTimeHelper.GetDifference(timeOut, parkedVehicleDetails.TimeIn);
            var mallPrakingLotDetails = parkingLotDetails as MallParkingLotDetails;
            if (mallPrakingLotDetails is not null && totalHours > 0)
            {
                var rate = ParkedVehicleHelper.GetRates(parkedVehicleDetails.VehicleType, mallPrakingLotDetails);
                return rate * totalHours;
            }
            return null;
        }
       
    }
}
