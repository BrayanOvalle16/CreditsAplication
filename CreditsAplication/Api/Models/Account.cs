using CreditsAplication.Api.Enums;
using System;
using System.Collections.Generic;

namespace CreditsAplication.Api.Models;

public partial class Account
{
    public int Id { get; set; }

    public Guid AccountNumber { get; set; }

    public AccountTypeEnum AccountType { get; set; }

    public decimal InitialBalance { get; set; }

    public bool Status { get; set; } = true!;

    public int? ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
