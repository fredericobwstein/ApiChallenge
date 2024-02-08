using FulltechAPI.Core.Entities;
using FulltechAPI.Core.Interfaces;
using FulltechAPI.Core.Services;

using Microsoft.AspNetCore.Mvc;

namespace FulltechAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase    
    {
        private readonly TransferService _transferService;


        public TransferController(TransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer(int sourceAccountId, int targetAccountId, decimal amount, [FromServices] TransferService transferService)
        {

            var result = await _transferService.TransferValues(sourceAccountId, targetAccountId, amount, transferService);

            return result;
        }
    }
}
