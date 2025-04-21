using Database.Database;
using Database.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Storage;

public static class UserStorage
{
    public static async Task<User?> GetUserByEmailAsync(string email)
    {
        await using var ctx = new AviaContext();

        return await ctx.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
    }

    public static List<User> GetUsersByOfficeId(int officeId)
    {
        using var ctx = new AviaContext();

        var users = ctx.Users
            .Include(u => u.Role)
            .Include(u => u.Office);

        return officeId == 0 ? users.ToList() : users.Where(u => u.OfficeId == officeId).ToList();
    }

    public static async Task SaveUserAsync(User user)
    {
        await using var ctx = new AviaContext();

        var role = ctx.Roles.First(r => r.Title == Role.User);

        user.Role = role;
        user.OfficeId = user.Office?.Id;
        user.Office = null;

        if (await ctx.Users.AnyAsync(u => u.Email == user.Email))
        {
            throw new InvalidOperationException();
        }

        ctx.Users.Add(user);
        await ctx.SaveChangesAsync();
    }
}