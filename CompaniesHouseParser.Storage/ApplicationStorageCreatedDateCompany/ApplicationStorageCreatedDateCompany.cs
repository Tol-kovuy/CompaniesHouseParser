using CompaniesHouseParser.Common;
using Newtonsoft.Json;

namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCreatedDateCompany
    : AccessorBase<ApplicationParsingState, IApplicationParsingState>
    , IApplicationStorageCreatedDateCompany
{
    private static string pathToCreatedDates = @"ParsingSettings\\ModifiedSettings.json";
    private IApplicationParsingState _state;

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

        _state = Get();
        //var _createdDate = Get().Companies.LastIncorporatedFrom; 
    }

    public DateTime GetLastIncorporatedFromDate()
    {
        EnsureFileLoaded();
        return _state.Companies.LastIncorporatedFrom;
    }

    public void SetLastIncorporatedFromDate(DateTime dates)
    {
        EnsureFileLoaded();
        _state.Companies.LastIncorporatedFrom = dates;
        SaveLastDate();
    }

    private void SaveLastDate()
    {
        string json = JsonConvert.SerializeObject(_state);
        File.WriteAllText(pathToCreatedDates, json);
    }
}