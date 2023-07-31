using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string TransactionType { get; set; } = null!;

        public decimal Value { get; set; }

        public decimal Balance { get; set; }

        public bool Status { get; set; } = true;

        public int? AccountId { get; set; }

        public virtual AccountDto? Account { get; set; }
    }
}
