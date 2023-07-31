using CreditsAplication.Api.Enums;
using CreditsAplication.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Dtos
{
    public class AccountCreationDto
    {
        [Required]
        public AccountTypeEnum AccountType { get; set; }
        [Required]
        public decimal InitialBalance { get; set; }
        [Required]
        public int? ClientId { get; set; }
    }
}
