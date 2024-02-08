using FulltechAPI.Core.Entities;
using FulltechAPI.Core.Interfaces;
using FulltechAPI.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FulltechAPI.Core.Services
{
    public class TransferService
    {
        private readonly HolidayCheckerService _holidayChecker;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferRepository _transferRepository;

        public TransferService(HolidayCheckerService holidayChecker, IAccountRepository accountRepository, ITransferRepository transferRepository)
        {
            _holidayChecker = holidayChecker;
            _accountRepository = accountRepository;
            _transferRepository = transferRepository;
        }

        public async Task<bool> IsWorkingDay(DateTime date)
        {
            return await _holidayChecker.IsWorkingDay(date);
        }

        public async Task<ActionResult> TransferValues(int sourceAccountId, int targetAccountId, decimal amount, [FromServices] TransferService transferService)
        {
            var dataAlternativa = new DateTime(2023, 12, 25);
            // Usar data de feriado para validar, se necessário
            if (!await IsWorkingDay(DateTime.Today))
            {
                return new BadRequestObjectResult("Transferências só podem ser realizadas em dias úteis.");
            }

            var sourceAccount = _accountRepository.GetById(sourceAccountId);
            var targetAccount = _accountRepository.GetById(targetAccountId);

            if (sourceAccount == null || targetAccount == null)
            {
                return new NotFoundObjectResult("Uma das contas não foi encontrada.");
            }

            if (sourceAccount.Balance < amount)
            {
                return new NotFoundObjectResult("Saldo insuficiente para realizar a transferência.");
            }

            // Realizar a transferência
            sourceAccount.Balance -= amount;
            targetAccount.Balance += amount;

            var transferEntity = new Transfer
            {
                SourceAccountId = sourceAccountId,
                TargetAccountId = targetAccountId,
                Amount = amount,
                Date = DateTime.Now
            };

            _transferRepository.Add(transferEntity);

            // Atualizar as contas no repositório
            _accountRepository.Update(sourceAccount);
            _accountRepository.Update(targetAccount);

            return new OkObjectResult("Transferência realizada com sucesso.");
        }
    }
}
