using System.Net.Mail;
using System.Text;
using App.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Domain.Validations;

public static class UserValidation
{
    public static string Validate(User user)
    {
        var sb = new StringBuilder();

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            sb.AppendLine("Enter Email;");
        }
        else if (!IsValidEmail(user.Email))
        {
            sb.AppendLine("Invalid Email;");
        }

        if (string.IsNullOrWhiteSpace(user.Password))
        {
            sb.AppendLine("Enter Password;");
        }

        if (string.IsNullOrWhiteSpace(user.FirstName))
        {
            sb.AppendLine("Enter First Name;");
        }

        if (string.IsNullOrWhiteSpace(user.LastName))
        {
            sb.AppendLine("Enter Last Name;");
        }

        return sb.ToString();
    }

    private static bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith('.'))
        {
            return false;
        }

        try
        {
            var addr = new MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}