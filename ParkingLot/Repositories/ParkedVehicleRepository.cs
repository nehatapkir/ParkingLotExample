using ParkingLot.Models;

namespace ParkingLot.Repositories
{
    public class ParkedVehicleRepository : IParkedVehicleRepository
    {
        private readonly List<ParkedVehicle> parkedVehicles;
        public ParkedVehicleRepository()
        {
            parkedVehicles = new List<ParkedVehicle>();
        }

        public void AddVehicle(ParkedVehicleType vehicleType, int spotNumber, DateTime dateIn, out string ticketNumber)
        {
            var vehicle = new ParkedVehicle(dateIn, vehicleType, spotNumber);
            parkedVehicles.Add(vehicle);
            ticketNumber = vehicle.TicketNumber;
        }

        public IEnumerable<ParkedVehicle> GetAllVehicle(ParkedVehicleType vehicleType)
        {
            return parkedVehicles.Where(veh => veh.VehicleType == vehicleType);
        }

        public ParkedVehicle? GetVehicle(string ticketNumber, ParkedVehicleType vehicleType)
        {          
            return parkedVehicles.FirstOrDefault(veh => veh.TicketNumber.Equals(ticketNumber) && veh.VehicleType == vehicleType);
        }

        public void RemoveVehicle(string ticketNumber, ParkedVehicleType vehicleType)
        {
            var parkedVehicleDetails = parkedVehicles.FirstOrDefault(veh => veh.TicketNumber == ticketNumber && veh.VehicleType == vehicleType);
            if (parkedVehicleDetails != null)
            {
                parkedVehicles.Remove(parkedVehicleDetails);
            }
        }
    }
}
