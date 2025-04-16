using System;
using Database.Database.Models;

namespace App.Domain.Models;

public sealed class User
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public Office? Office { get; set; }

    public DateTimeOffset? Birthdate { get; set; }
}