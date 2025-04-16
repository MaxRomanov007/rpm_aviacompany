using System.Security;
using Database.Database.Models;
using Database.Storage;

namespace Database.Services;

public static class UserService
{
    public static async Task<User> Login(string email, string password)
    {
        var user = await UserStorage.GetUserByEmailAsync(email);
        if (user is null || user.Password != password)
        {
            throw new UnauthorizedAccessException();
        }

        if (user.Active is not null && !(bool)user.Active)
        {
            throw new SecurityException();
        }

        return user;
    }
}