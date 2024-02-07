using FulltechAPI.Core.Entities;
using FulltechAPI.Core.Interfaces;
using FulltechAPI.Core.Services;

using Microsoft.AspNetCore.Mvc;

namespace FulltechAPI.Controller
{
    public class TransferController : ControllerBase    
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferRepository _transferRepository;

        public TransferController(IAccountRepository accountRepository,
                                  ITransferRepository transferRepository)
        {
            _accountRepository = accountRepository;
            _transferRepository = transferRepository;
        }

        [HttpPut("transfer")]
        public async Task<IActionResult> TransferFunds(int sourceAccountId, int targetAccountId, decimal amount, [FromServices] TransferService transferService)
        {

            var transfer = new Transfer
            {
                SourceAccountId = sourceAccountId,
                TargetAccountId = targetAccountId,
                Amount = amount,
                Date = DateTime.Now 
            };

             _transferRepository.Add(transfer);

            var dataAlternativa = new DateTime(2023, 12, 28);

            if (!await transferService.IsWorkingDay(dataAlternativa))
            {
                return BadRequest("Transferências só podem ser realizadas em dias úteis.");
            }

            var sourceAccount = _accountRepository.GetById(sourceAccountId);
            var targetAccount = _accountRepository.GetById(targetAccountId);

            if (sourceAccount == null || targetAccount == null)
            {
                return NotFound("Uma das contas não foi encontrada.");
            }

            if (sourceAccount.Balance < amount)
            {
                return BadRequest("Saldo insuficiente para realizar a transferência.");
            }

            // Realizar a transferência
            sourceAccount.Balance -= amount;
            targetAccount.Balance += amount;

            // Atualizar as contas no repositório
            _accountRepository.Update(sourceAccount);
            _accountRepository.Update(targetAccount);

            return Ok("Transferência realizada com sucesso.");
        }
    }
}
