using FulltechAPI.Core.Entities;

namespace FulltechAPI.Core.Interfaces
{
    public interface ITransferRepository
    {
        void Add(Transfer transfer);
        List<Transfer> GetTransfersByPeriod(DateTime startDate, DateTime endDate);
    }
}
