namespace Database.Database.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public const string Administrator = "Administrator";

    public const string User = "User";
}
