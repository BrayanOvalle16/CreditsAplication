using CreditsAplication.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Dtos
{
    public class ClientCreationDto
    {
        [Required]
        public string Password { get; set; } = null!;
        public virtual PersonCreationDto? Person { get; set; }
    }
}
