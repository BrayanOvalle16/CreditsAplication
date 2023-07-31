using CreditsAplication.Api.Enums;
using CreditsAplication.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Dtos
{
    public class PersonDto
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public Gender? Gender { get; set; }

        public int? Age { get; set; }

        public string Identification { get; set; } = null!;

        public string? Address { get; set; }

        public bool Status { get; set; } = true;

        public string? PhoneNumber { get; set; }

        public virtual ClientDto? Client { get; set; }
    }
}
