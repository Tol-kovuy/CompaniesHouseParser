using Newtonsoft.Json;

namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCreatedDateCompany
{
    private string pathToCreatedDates = @"ModifiedSettings.json";

    private IList<DateTime> _dateCreation;

    public DateTime GetDate()
    {
        var settings = File.ReadAllText(pathToCreatedDates);

        IApplicationParsingState? jsonToObj = null;
        try
        {
            jsonToObj = JsonConvert.DeserializeObject<ApplicationParsingState>(settings);
            if (jsonToObj == null)
                throw new Exception("Sorry, but json file can not be deserialize to object");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine($"Message : {ex}");
            throw;
        }

        return jsonToObj.Companies.LastIncorporatedFrom;
    }

    public void ReWriteIncorporatedFrom(IList<DateTime> dates)
    {
        var lastDate = GetLastDate(dates);
        ApplicationParsingState parsingState = new ApplicationParsingState
        {
            Companies = new ApplicationCompaniesParsingState { LastIncorporatedFrom = lastDate }
        };

        string json = JsonConvert.SerializeObject(parsingState);
        File.WriteAllText(pathToCreatedDates, json);
    }
    private DateTime GetLastDate(IList<DateTime> dates)
    {
        var datesList = new List<DateTime>();
        foreach (var date in dates)
        {
            datesList.Add(date);
        }
        var nowDate = DateTime.Now;
        var lastDate = datesList.OrderBy(date => Math.Abs((nowDate - date).TotalSeconds)).First();

        return lastDate;
    }
}