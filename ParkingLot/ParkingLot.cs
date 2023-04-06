using ParkingLot.Exceptions;
using ParkingLot.Helpers;
using ParkingLot.Models;
using ParkingLot.Repositories;
using ParkingLot.Strategy;

namespace ParkingLot
{
    public class ParkingLot : IParkingLot
    {
        private readonly IParkedVehicleRepository _parkedVehicleRepository;
        private readonly ParkingLotDetails _parkingLotDetails;
        private readonly IFeesCalculationStrategy _feesCalculationStrategy;

        public ParkingLot(IParkedVehicleRepository parkedVehicleRepository, ParkingLotDetails parkingLotDetails, IFeesCalculationStrategy feesCalculationStrategy)
        {
            _parkedVehicleRepository = parkedVehicleRepository;
            _parkingLotDetails = parkingLotDetails;
            _feesCalculationStrategy = feesCalculationStrategy;
        }

        public string ParkVehicle(VehicleType vehicleType, DateTime dateIn)
        {
            var parkedVehicleType = ParkedVehicleHelper.GetParkedVehicleType(vehicleType);
            try
            {
                if (IsSpotAvailable(parkedVehicleType))
                {
                    int spotNumber = GetAvailableSpot(parkedVehicleType);
                    _parkedVehicleRepository.AddVehicle(parkedVehicleType, spotNumber, dateIn, out string ticketNumber);
                    return ticketNumber;
                }
            }
            catch(Exception)
            {
                //log exception.
                throw;
            }

            return string.Empty;
        }

        public decimal? UnparkVehicle(string ticketNumber, VehicleType vehicleType, DateTime dateOut)
        {
            var parkedVehicleType = ParkedVehicleHelper.GetParkedVehicleType(vehicleType);
            var parkedVehicleDetails = _parkedVehicleRepository.GetVehicle(ticketNumber, parkedVehicleType);
            if (parkedVehicleDetails != null)
            {
                ReleaseSpot(parkedVehicleDetails.TicketNumber, parkedVehicleDetails.VehicleType);

                try
                {
                    return _feesCalculationStrategy.CalculateFees(_parkingLotDetails, parkedVehicleDetails, dateOut);
                }
                catch(CalculationsError ex)
                {
                    //log exception.
                    throw ex;
                }
            }
            throw new ParkedVehicleDetailsNotFound(ticketNumber);
        }

        private void ReleaseSpot(string ticketNumber, ParkedVehicleType vehicleType)
        {
            _parkedVehicleRepository.RemoveVehicle(ticketNumber, vehicleType);
        }

        private bool IsSpotAvailable(ParkedVehicleType vehicleType)
        {
           var spotsAvailable =  _parkingLotDetails.NoOfSpots.FirstOrDefault(spot => spot.Key == vehicleType).Value;
            return _parkedVehicleRepository.GetAllVehicle(vehicleType).Count(veh => veh.VehicleType == vehicleType) < spotsAvailable;
        }

        private int GetAvailableSpot(ParkedVehicleType vehicleType)
        {
            var currentVehicles = _parkedVehicleRepository.GetAllVehicle(vehicleType);
            var spotNo = Enumerable.Range(1, _parkingLotDetails.NoOfSpots[vehicleType]).Except(currentVehicles.Select(n => n.SpotNumber));
            return spotNo.Min();
        }
    }
}
