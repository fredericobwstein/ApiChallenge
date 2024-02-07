namespace FulltechAPI.Core.Services
{
    public class TransferService
    {
        private readonly HolidayCheckerService _holidayChecker;

        public TransferService(HolidayCheckerService holidayChecker)
        {
            _holidayChecker = holidayChecker;
        }

        public async Task<bool> IsWorkingDay(DateTime date)
        {
            return await _holidayChecker.IsWorkingDay(date);
        }
    }
}
