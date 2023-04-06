using ParkingLot.Models;

namespace ParkingLot.Repositories
{
    /// <summary>
    /// The repository that deals with the parking vehicle data.
    /// </summary>
    public interface IParkedVehicleRepository
    {
        /// <summary>
        /// Add a vehicle to the parking details list.
        /// </summary>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <param name="spotNumber">The spot no.</param>
        /// <param name="dateIn">The date time when the vehicle was parked.</param>
        /// <param name="tickeNumber">The ticket number generated.</param>
        void AddVehicle(ParkedVehicleType vehicleType, int spotNumber, DateTime dateIn, out string tickeNumber);

        /// <summary>
        /// Remove a vehicle from the parking details list.
        /// </summary>
        /// <param name="ticketNumber">The ticket number that identifies the vehicle.</param>
        /// <param name="vehicleType">The type of vehicle which will be unparked.</param>
        void RemoveVehicle(string ticketNumber, ParkedVehicleType vehicleType);

        /// <summary>
        /// Fetch a particular vehicle.
        /// </summary>
        /// <param name="ticketNumber">The ticket number that identifies the vehicle.</param>
        /// <param name="vehicleType">The type of vehicle.</param>
        /// <returns>The vehicle identified by the ticket no and vehicle type.</returns>
        ParkedVehicle? GetVehicle(string ticketNumber, ParkedVehicleType vehicleType);

        /// <summary>
        /// Get all vehicles of a particular type.
        /// </summary>
        /// <param name="vehicleType">The type of vehicle.</param>
        /// <returns>The vehicle identified by the ticket no and vehicle type.</returns>
        IEnumerable<ParkedVehicle> GetAllVehicle(ParkedVehicleType vehicleType);

    }
}
