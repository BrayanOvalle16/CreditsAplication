using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Enums;
using CreditsAplication.Api.Exceptions;
using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Interface.Services;
using CreditsAplication.Api.Models;
using System.Numerics;

namespace CreditsAplication.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConfiguration _configuration;
        public TransactionService(ITransactionRepository transactionRepository, IConfiguration configuration)
        {
            _transactionRepository = transactionRepository;
            _configuration = configuration;
        }

        public Task<Transaction> GetByIdAsync(int id)
        {
            return _transactionRepository.GetByIdAsync(id);
        }

        public Task<List<Transaction>> GetAllAsync()
        {
            return _transactionRepository.GetAllAsync();
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            var currentBalance = await _transactionRepository.GetTotalBalanceByIdAsync(transaction.AccountId.Value, null);
            await ValidateConditionsForTransactionAsync(transaction.AccountId.Value, currentBalance, transaction.Value, transaction);
            var valueToApply = GetFormattedValue(transaction.Value, transaction.TransactionType);
            transaction.Balance = currentBalance + valueToApply;
            transaction.Value = valueToApply;
            return await _transactionRepository.AddAsync(transaction);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            var currentBalance = await _transactionRepository.GetTotalBalanceByIdAsync(transaction.AccountId.Value, transaction.Id);
            await ValidateConditionsForTransactionAsync(transaction.AccountId.Value, currentBalance, transaction.Value, transaction);
            var valueToApply = GetFormattedValue(transaction.Value, transaction.TransactionType);
            transaction.Balance = currentBalance + valueToApply;
            transaction.Value = valueToApply;
            await _transactionRepository.UpdateAsync(transaction);
        }

        public Task DeleteAsync(int id)
        {
            return _transactionRepository.DeleteAsync(id);
        }

        public async Task<List<TrasactionReport>> GetTransactionReportAsync(int id, DateTime startDateTime, DateTime endDateTime)
        {
            return await _transactionRepository.GetTransactionReportAsync(id, startDateTime, endDateTime);
        }

        protected async Task ValidateConditionsForTransactionAsync(int accountId, decimal currentBalance, decimal value, Transaction transaction)
        {

            var todayTransactions = await _transactionRepository.GetAllByIdAndDateRangeAsync(accountId, 
                DateTime.Today, 
                DateTime.Today.AddDays(1).AddTicks(-1),
                new List<TransactionType>() { TransactionType.Retiro });
            var todayTransactionTotal = todayTransactions
                .Select(x => x.Value)
                .Sum() + value;

            var maxAmount = decimal.Parse(_configuration.GetSection("TransactionConfigs:LimitOfWithdrawsTotalPerDay").Value);
            if (todayTransactionTotal > maxAmount) 
            {
                throw new InsuficientBalanceException($"Cupo Diario Excedido");
            }
            if (currentBalance < transaction.Value && transaction.TransactionType == TransactionType.Retiro)
            {
                throw new InsuficientBalanceException($"Saldo No disponible");
            }
        }

        protected decimal GetFormattedValue(decimal value, TransactionType transactionType)
        {
            return TransactionType.Retiro == transactionType ? value * -1: value;
        }

    }
}
