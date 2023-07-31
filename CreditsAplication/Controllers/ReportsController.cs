using AutoMapper;
using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditsAplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public ReportsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetReport(int clientId, DateTime startTime, DateTime endTime)
        {
            return Ok(await _transactionService.GetTransactionReportAsync(clientId, startTime, endTime));
        }
    }
}
