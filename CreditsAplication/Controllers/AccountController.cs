using AutoMapper;
using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Interface.Facades;
using CreditsAplication.Api.Interface.Services;
using CreditsAplication.Api.Models;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;

namespace CreditsAplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountAccountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountAccountService, IMapper mapper)
        {
            _accountAccountService = accountAccountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountAccountService.GetAllAsync();
            return Ok(_mapper.Map<List<AccountDto>>(accounts));
        }

        [HttpGet("{id}", Name = "GetAccountById")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountAccountService.GetByIdAsync(id);
            if (account == null)
                return NotFound();

            return Ok(_mapper.Map<AccountDto>(account));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAccount(AccountCreationDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            await _accountAccountService.AddAsync(account);

            // Generate the URL for the newly created account
            return CreatedAtRoute("GetAccountById", new { id = account.Id }, _mapper.Map<AccountDto>(account));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountUpdateDto updatedAccount)
        {
            var existingAccount = await _accountAccountService.GetByIdAsync(id);
            if (existingAccount == null)
                return NotFound();

            var account = _mapper.Map<Account>(updatedAccount);
            existingAccount.AccountType = updatedAccount.AccountType;
            existingAccount.InitialBalance = updatedAccount.InitialBalance;
            existingAccount.Client = null;
            await _accountAccountService.UpdateAsync(existingAccount);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAccount(int id, [FromBody] JsonPatchDocument patchDocument)
        {
            var account = await _accountAccountService.GetByIdAsync(id);
            if (account == null)
                return NotFound();

            patchDocument.ApplyTo(account);
            account.Id = id;
            await _accountAccountService.UpdateAsync(account);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _accountAccountService.GetByIdAsync(id);
            if (account == null)
                return NotFound();

            await _accountAccountService.DeleteAsync(id);

            return NoContent();
        }
    }
}
