using System;
using System.Collections.Generic;

namespace Database.Database.Models;

public partial class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public int? OfficeId { get; set; }

    public DateTime? Birthdate { get; set; }

    public bool? Active { get; set; }

    public virtual Office? Office { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    
    public int? Age
    {
        get
        {
            if (!Birthdate.HasValue)
                return null;
                
            var today = DateTime.Today;
            var age = today.Year - Birthdate.Value.Year;
            
            if (Birthdate.Value > today.AddYears(-age))
                age--;
                
            return age;
        }
    }

    public string OfficeString => Office?.Title ?? string.Empty;
}