namespace Database.Database.Models;

public partial class CabinType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
