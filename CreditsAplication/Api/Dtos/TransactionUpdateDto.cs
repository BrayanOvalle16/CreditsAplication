using CreditsAplication.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Dtos
{
    public class TransactionUpdateDto
    {
        [Required]
        public decimal Value { get; set; }
        [Required]
        public int? AccountId { get; set; }
    }
}
