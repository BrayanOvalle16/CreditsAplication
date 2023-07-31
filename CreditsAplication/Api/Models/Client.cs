using System;
using System.Collections.Generic;
using CreditsAplication.Api.Dtos;

namespace CreditsAplication.Api.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public bool Status { get; set; } = true;

    public int? PersonId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Person? Person { get; set; }

}
