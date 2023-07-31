using CreditsAplication.Api.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditsAplication.Api.Models;

public partial class Person
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

    public virtual Client? Client { get; set; }
}
