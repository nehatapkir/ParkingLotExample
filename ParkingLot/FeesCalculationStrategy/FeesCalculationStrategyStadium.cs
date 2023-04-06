using ParkingLot.Helpers;
using ParkingLot.Models;

namespace ParkingLot.Strategy
{
    public class FeesCalculationStrategyStadium : IFeesCalculationStrategy
    {
        public decimal? CalculateFees(ParkingLotDetails parkingLotDetails, ParkedVehicle parkedVehicleDetails, DateTime timeOut)
        {
            var totalHours = DateTimeHelper.GetDifference(timeOut, parkedVehicleDetails.TimeIn);
            var largePrakingLotDetails = parkingLotDetails as LargeParkingLotDetails;
            if (largePrakingLotDetails is not null && totalHours > 0)
            {
                decimal totalFees = 0;
                var rates = ParkedVehicleHelper.GetRates(parkedVehicleDetails.VehicleType, largePrakingLotDetails);
                var bands = rates?.Where(rate => rate.MinHour < totalHours).ToList();
                if (bands is not null)
                {
                    foreach (var rate in bands.Where(r => r.IsFlatRate))
                    {
                        totalFees += rate.Rate;
                    }

                    if (bands.Any(rate => !rate.IsFlatRate))
                    {
                        var hourlyRate = bands.FirstOrDefault(rate => !rate.IsFlatRate);
                        var firstIntervalRates = (totalHours - hourlyRate.MinHour) * hourlyRate.Rate;
                        totalFees += firstIntervalRates;
                    }
                }
                return totalFees;
            }

            return null;
        }
    }
}
