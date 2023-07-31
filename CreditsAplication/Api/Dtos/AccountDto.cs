using CreditsAplication.Api.Enums;

namespace CreditsAplication.Api.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }

        public Guid AccountNumber { get; set; }

        public AccountTypeEnum AccountType { get; set; }

        public decimal InitialBalance { get; set; }

        public bool Status { get; set; } = true!;

        public int? ClientId { get; set; }

        public virtual ClientDto? Client { get; set; }

        public virtual ICollection<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
