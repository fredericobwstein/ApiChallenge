using FulltechAPI.Core.Entities;
using FulltechAPI.Core.Interfaces;
using FulltechAPI.Core.Services;

using Microsoft.AspNetCore.Mvc;

namespace FulltechAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractController : ControllerBase
    {
        private readonly ITransferRepository _transferRepository;

        public ExtractController(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        [HttpGet("statement")]
        public  ActionResult<List<Transfer>> GetAccountStatement(DateTime startDate, DateTime endDate)
        {
            var transfers = _transferRepository.GetTransfersByPeriod(startDate, endDate);

            if (transfers.Count == 0)
            {
                return NotFound("Nenhuma transferência encontrada neste período.");
            }

            return transfers;
        }

    }
}
