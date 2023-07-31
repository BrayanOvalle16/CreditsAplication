

using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Dtos
{
    public class ClientUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public virtual PersonUpdateDto? Person { get; set; }

    }
}
