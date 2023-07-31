using CreditsAplication.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Dtos
{
    public class PersonUpdateDto
    {
        [Required]
        public string FullName { get; set; } = null!;

        public Gender? Gender { get; set; }

        public int? Age { get; set; }
        [Required]
        public string Identification { get; set; } = null!;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
