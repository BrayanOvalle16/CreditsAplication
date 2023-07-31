using CreditsAplication.Api.Enums;
using System;
using System.Collections.Generic;

namespace CreditsAplication.Api.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Value { get; set; }

    public decimal Balance { get; set; }

    public bool Status { get; set; } = true;

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }

}
