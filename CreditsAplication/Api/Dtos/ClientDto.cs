using CreditsAplication.Api.Models;

namespace CreditsAplication.Api.Dtos
{
    public class ClientDto
    {
        public int Id { get; set; }

        public bool Status { get; set; } = true!;

        public int? PersonId { get; set; }

        public virtual ICollection<AccountDto> Accounts { get; set; } = new List<AccountDto>();

        public virtual PersonDto? Person { get; set; }
    }
}
