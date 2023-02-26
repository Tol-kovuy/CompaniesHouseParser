using Newtonsoft.Json;

namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCreatedDateCompany : IApplicationStorageCreatedDateCompany
{
    private string pathToCreatedDates = @"ParsingSettings\\ModifiedSettings.json";
    private DateTime _lastIncorporatedDate;
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
        _lastIncorporatedDate = jsonToObj.Companies.LastIncorporatedFrom;

        return _lastIncorporatedDate;
    }

    public void ReWriteIncorporatedFrom(IList<DateTime> dates)
    {
        var lastDate = GetLastDate(dates);
        ApplicationParsingState parsingState = new ApplicationParsingState
        {
            Companies = new ApplicationCompaniesParsingState { LastIncorporatedFrom = lastDate }
        };

        string json = JsonConvert.SerializeObject(parsingState);
        //File.WriteAllText(pathToCreatedDates, json);
        using (StreamWriter write = new StreamWriter(pathToCreatedDates, false))
        {
            write.WriteLine(json);
        }
    }

    private DateTime GetLastDate(IList<DateTime> dates)
    {
        if (dates == null || dates.Count == 0)
        {
            return _lastIncorporatedDate = GetDate();
        }
        else
        {
            _lastIncorporatedDate = GetDate();
            for (int i = 1; i < dates.Count; i++)
            {
                if (dates[i] > _lastIncorporatedDate)
                {
                    _lastIncorporatedDate = dates[i];
                }
            }

            return _lastIncorporatedDate;
        }
    }
}