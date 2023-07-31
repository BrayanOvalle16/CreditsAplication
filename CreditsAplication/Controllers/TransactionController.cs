using AutoMapper;
using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Interface.Services;
using CreditsAplication.Api.Models;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;

namespace CreditsAplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionTransactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionTransactionService, IMapper mapper)
        {
            _transactionTransactionService = transactionTransactionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _transactionTransactionService.GetAllAsync();
            return Ok(_mapper.Map<List<TransactionDto>>(transactions));
        }

        [HttpGet("{id}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionTransactionService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound();

            return Ok(_mapper.Map<TransactionDto>(transaction));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTransaction(TransactionCreationDto transactionDto)
        {
            if (transactionDto.Value < 0)
            {
                return BadRequest("Values should be positive");
            }
            
            var transaction = _mapper.Map<Transaction>(transactionDto);
            await _transactionTransactionService.AddAsync(transaction);
            // Generate the URL for the newly created transaction
            return CreatedAtRoute("GetTransactionById", new { id = transaction.Id }, _mapper.Map<TransactionDto>(transaction));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionUpdateDto updatedTransaction)
        {
            if (updatedTransaction.Value < 0)
            {
                return BadRequest("Values should be possitive");
            }
            var existingTransaction = await _transactionTransactionService.GetByIdAsync(id);
            if (existingTransaction == null)
                return NotFound();

            var transaction = _mapper.Map<Transaction>(updatedTransaction);
            transaction.Id = existingTransaction.Id; // Ensure the correct ID is set
            transaction.AccountId = existingTransaction.AccountId; // Ensure the correct ID is set
            await _transactionTransactionService.UpdateAsync(transaction);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTransaction(int id, [FromBody] JsonPatchDocument patchDocument)
        {
            var transaction = await _transactionTransactionService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound();

            patchDocument.ApplyTo(transaction);
            transaction.Id = id;
            await _transactionTransactionService.UpdateAsync(transaction);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _transactionTransactionService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound();

            await _transactionTransactionService.DeleteAsync(id);

            return NoContent();
        }
    }
}

