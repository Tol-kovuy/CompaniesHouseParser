using CompaniesHouseParser.Common;
using Newtonsoft.Json;

namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCreatedDateCompany
    : AccessorBase<ApplicationParsingState, IApplicationParsingState>
    , IApplicationStorageCreatedDateCompany
{
    private static string pathToCreatedDates = @"ParsingSettings\\ModifiedSettings.json";
    private ApplicationParsingState _state;

    public ApplicationStorageCreatedDateCompany()
        : base(pathToCreatedDates)
    {
    }

    private void EnsureFileLoaded()
    {
        if (_state != null)
        {
            return;
        }
        _state = Deserialize();
    }

    public DateTime GetSearchIncorporatedFromDate()
    {
        EnsureFileLoaded();
        return _state.Companies.SearchIncorporatedFrom;
    }

    public void SetSearchIncorporatedFromDate(DateTime dates)
    {
        EnsureFileLoaded();
        _state.Companies.SearchIncorporatedFrom = dates;
        SaveLastDate();
    }

    private void SaveLastDate()
    {
        string json = JsonConvert.SerializeObject(_state);
        File.WriteAllText(pathToCreatedDates, json);
    }
}