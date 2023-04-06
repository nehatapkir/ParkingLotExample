using ParkingLot.Models;

namespace ParkingLot
{
    /// <summary>
    /// Logic for parking and unparking a vehicle.
    /// </summary>
    public interface IParkingLot
    {
        /// <summary>
        /// Adds a vehicle to the parking list.
        /// </summary>
        /// <param name="vehicleType">The vehicle type.</param>
        /// <param name="dateIn">The time at which the vehicle is parked.</param>
        /// <returns>The ticket number generated.</returns>
        string? ParkVehicle(VehicleType vehicleType, DateTime dateIn);

        /// <summary>
        /// Removes a vehicle from the parking list and calculates the total fees.
        /// </summary>
        /// <param name="ticketNumber">The ticket number generated</param>
        /// <param name="vehicleType">The vehicle type.</param>
        /// <param name="dateOut">The time at which the vehicle is unparked.</param>
        /// <returns>The total fees.</returns>
        decimal? UnparkVehicle(string ticketNumber,VehicleType vehicleType, DateTime dateOut);
    }
}
