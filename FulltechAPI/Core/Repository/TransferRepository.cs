using FulltechAPI.Core.Entities;
using FulltechAPI.Core.Interfaces;

namespace FulltechAPI.Core.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private List<Transfer> transfers = new List<Transfer>(); 

        public void Add(Transfer transfer)
        {
            transfers.Add(transfer); 
        }

        public List<Transfer> GetTransfersByPeriod(DateTime startDate, DateTime endDate)
        {
            return transfers.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
        }
    }
}
