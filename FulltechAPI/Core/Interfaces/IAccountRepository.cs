using FulltechAPI.Core.Entities;

using System.Security.Principal;

namespace FulltechAPI.Core.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        Account GetById(int accountId);
        void Add(Account account);
        void Delete(Account account);
        void Update(Account account);
    }
}
