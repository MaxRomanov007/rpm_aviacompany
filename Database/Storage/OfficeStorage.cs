using Database.Database;
using Database.Database.Models;

namespace Database.Storage;

public static class OfficeStorage
{
    public static List<Office> GetOffices()
    {
        return new AviaContext().Offices.ToList();
    }
}