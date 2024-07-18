using System.Text;

namespace CompaniesHouseParser.Api;

public static class StringExtension
{
    public static string Base64Encode(this string apiToken)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(apiToken));
    }
   
    public static void RemoveDuplicatesAndSave(this string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            string[] lines = File.ReadAllLines(filePath);

            List<string> uniqueLines = lines.Distinct().ToList();

            if (uniqueLines.Count == lines.Length)
            {
                return;
            }

            File.WriteAllLines(filePath, uniqueLines);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}
