﻿namespace Database.Database.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public int AircraftId { get; set; }

    public int RouteId { get; set; }

    public decimal EconomyPrice { get; set; }

    public bool Confirmed { get; set; }

    public string? FlightNumber { get; set; }

    public virtual Aircraft Aircraft { get; set; } = null!;

    public virtual Route Route { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
