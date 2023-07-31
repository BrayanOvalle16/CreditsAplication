using CreditsAplication.Api.Enums;
using CreditsAplication.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Dtos
{
    public class TransactionCreationDto
    {
        [Required]
        public TransactionType TransactionType { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public int? AccountId { get; set; }
    }
}
