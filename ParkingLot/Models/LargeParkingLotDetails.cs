namespace ParkingLot.Models
{
    public class LargeParkingLotDetails : ParkingLotDetails
    {
        public IEnumerable<Rates>? CarRates;

        public IEnumerable<Rates>? MotorcycleRates;

        public IEnumerable<Rates>? TruckRates;       

        public LargeParkingLotDetails(LocationType locationType) : base(locationType)
        {
            CarRates = new List<Rates>();
            MotorcycleRates = new List<Rates>();
            TruckRates = new List<Rates>();
        }

    }

    public struct Rates
    {
        public Rates(int minHour, int maxHour, decimal rate, bool isFlatRate = true)
        {
            MinHour = minHour;
            MaxHour = maxHour;
            Rate = rate;
            IsFlatRate = isFlatRate;
        }

        public int MinHour { get; set; }
        public int MaxHour { get; set; }
        public bool IsFlatRate { get; set; }
        public decimal Rate { get; set; }
    }
}
