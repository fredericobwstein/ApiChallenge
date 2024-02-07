using FulltechAPI.Core.Entities;
using FulltechAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace FulltechAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController (IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            _accountRepository.Add(account);
            return CreatedAtAction(nameof(GetAccount), new { id = account.AccountNumber }, account);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var account = _accountRepository.GetById(id);

            if (account == null)
            {
                return NotFound();  
            }
                return Ok(account);
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountRepository.GetAll();
            return Ok(accounts);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var account = _accountRepository.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            _accountRepository.Delete(account);
            return NoContent();
        }

    }
}
