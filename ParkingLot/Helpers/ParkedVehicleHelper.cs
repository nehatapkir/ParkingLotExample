using ParkingLot.Exceptions;
using ParkingLot.Models;

namespace ParkingLot.Helpers
{
    public static class ParkedVehicleHelper
    {
        /// <summary>
        /// Maps the different vehicle type inputs to parking slot types.
        /// </summary>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <returns>The parking slot applicable.</returns>
        public static ParkedVehicleType GetParkedVehicleType(VehicleType vehicleType)
        {
            return vehicleType switch
            {
                VehicleType.Car or VehicleType.SUV => ParkedVehicleType.Car,
                VehicleType.Truck or VehicleType.Bus => ParkedVehicleType.Truck,
                VehicleType.Motorcycle or VehicleType.Scooter => ParkedVehicleType.Motorcycle
            };
        }

        /// <summary>
        /// Get Rates for a parked vehicle type.
        /// </summary>
        /// <param name="vehicleType">The vehicle type.</param>
        /// <param name="parkingLotDetails">The parking lot details.</param>
        /// <returns>The list of rates</returns>
        /// <exception cref="CalculationsError">An exception when rates are not found.</exception>
        public static IEnumerable<Rates>? GetRates(ParkedVehicleType vehicleType, LargeParkingLotDetails parkingLotDetails)
        {
            return vehicleType switch
            {
                ParkedVehicleType.Truck => parkingLotDetails.TruckRates,
                ParkedVehicleType.Car => parkingLotDetails.CarRates,
                ParkedVehicleType.Motorcycle => parkingLotDetails.MotorcycleRates,
                _ => throw new CalculationsError()
            }; ;
        }

        /// <summary>
        /// Get Rates for a parked vehicle type.
        /// </summary>
        /// <param name="vehicleType">The vehicle type.</param>
        /// <param name="parkingLotDetails">The parking lot details.</param>
        /// <returns>The list of rates</returns>
        /// <exception cref="CalculationsError">An exception when rates are not found.</exception>
        public static decimal? GetRates(ParkedVehicleType vehicleType, MallParkingLotDetails parkingLotDetails)
        {
            return vehicleType switch
            {
                ParkedVehicleType.Car => parkingLotDetails.CarParkingRate,
                ParkedVehicleType.Motorcycle => parkingLotDetails.MotorcycleParkingRate,
                ParkedVehicleType.Truck => parkingLotDetails.TruckParkingRate,
                _ => throw new CalculationsError()
            };
        }
    }
}
