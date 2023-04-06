using ParkingLot.Helpers;
using ParkingLot.Models;

namespace ParkingLot.Strategy
{
    public class FeesCalculationStrategyAirport : IFeesCalculationStrategy
    {
        public decimal? CalculateFees(ParkingLotDetails parkingLotDetails, ParkedVehicle parkedVehicleDetails, DateTime timeOut)
        {
            var totalHours = DateTimeHelper.GetDifference(timeOut, parkedVehicleDetails.TimeIn);
            var largeParkingLotDetails = parkingLotDetails as LargeParkingLotDetails;
            if (largeParkingLotDetails is not null && totalHours > 0)
            {
                var rates = ParkedVehicleHelper.GetRates(parkedVehicleDetails.VehicleType, largeParkingLotDetails);
                var bands = rates?.Where(rate => rate.MinHour < totalHours).ToList();
                decimal totalFees = 0;
                if (bands is not null)
                {
                    if (bands.Any(rate => !rate.IsFlatRate))
                    {
                        var dailyRate = bands.FirstOrDefault(rate => !rate.IsFlatRate);
                        var totalNoOfDays = (int)Math.Ceiling(((double)totalHours / 24));
                        var intervalRates = totalNoOfDays * dailyRate.Rate;
                        totalFees += intervalRates;
                    }
                    else
                    {
                        totalFees = bands.Last().Rate;
                    }
                }

                return totalFees;
            }
            return null;
        }
    }
}
