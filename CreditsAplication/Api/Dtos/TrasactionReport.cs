using CreditsAplication.Api.Enums;

namespace CreditsAplication.Api.Dtos
{
    public class TrasactionReport
    {
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
        public Guid AccountNumber { get; set; }
        public AccountTypeEnum Type { get; set; }
        public decimal InicialAmount { get; set; }
        public decimal MovementAmount { get; set;}
        public bool State { get; set; }
        public bool AvailableTotal { get; set; }
    }
}
