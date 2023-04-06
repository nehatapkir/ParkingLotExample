namespace ParkingLot.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Get difference between 2 datetimes.
        /// </summary>
        /// <param name="dateTimeTo">The datetime out for the vehicle.</param>
        /// <param name="dateTimeFrom">The datetime in for the vehicle.</param>
        /// <returns></returns>
        public static int GetDifference(DateTime dateTimeTo, DateTime dateTimeFrom)
        {
            return (int)Math.Ceiling((dateTimeTo - dateTimeFrom).TotalHours);
        }
    }
}
