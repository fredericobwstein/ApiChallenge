using FulltechAPI.Core.Entities;
using FulltechAPI.Core.Interfaces;
using System.Security.Principal;

namespace FulltechAPI.Core.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private List<Account> _accounts = new List<Account>();

        public void Add(Account account)
        {
            _accounts.Add(account);
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts;
        }

        public Account GetById(int accountId)
        {
            return _accounts.FirstOrDefault(a => a.AccountNumber == accountId);
        }

        public void Delete(Account account)
        {
            _accounts.Remove(account);
        }

        public void Update(Account account)
        {
            // Encontre a conta correspondente na lista
            var existingAccount = _accounts.FirstOrDefault(a => a.AccountNumber == account.AccountNumber);

            if (existingAccount != null)
            {
                // Atualize o saldo da conta na lista
                existingAccount.Balance = account.Balance;
            }
        }
    }
}
