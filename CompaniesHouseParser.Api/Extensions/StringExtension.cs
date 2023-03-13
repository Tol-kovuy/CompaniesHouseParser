using System.Text;

namespace CompaniesHouseParser.Api;

public static class StringExtension
{
    public static string Base64Encode(this string apiToken)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(apiToken));
    }
}
