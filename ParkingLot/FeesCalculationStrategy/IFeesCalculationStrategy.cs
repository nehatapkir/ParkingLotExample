using ParkingLot.Models;

namespace ParkingLot.Strategy
{
    /// <summary>
    /// The fees calculating strategy.
    /// </summary>
    public interface IFeesCalculationStrategy
    {
        /// <summary>
        /// Calculate fees for a given parking lot and parked vehicle type.
        /// </summary>
        /// <param name="parkingLotDetails">The parking lot type and its fee model.</param>
        /// <param name="parkedVehicleDetails">The parked vehicle details.</param>
        /// <param name="timeOut">The time at which the vehicle is unparked.</param>
        /// <returns>The total fees.</returns>
        decimal? CalculateFees(ParkingLotDetails parkingLotDetails, ParkedVehicle parkedVehicleDetails, DateTime timeOut);

    }
}
