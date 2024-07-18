using CompaniesHouseParser.Common;
using CompaniesHouseParser.Shared;
using Newtonsoft.Json;

namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCreatedDateCompany
    : AccessorBase<ApplicationParsingState, IApplicationParsingState>
    , IApplicationStorageCreatedDateCompany
{
    private ApplicationParsingState _state;

    public ApplicationStorageCreatedDateCompany()
        : base(FilePaths.ParsingSettingsJsonPath)
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
        if (_state.Companies.SearchIncorporatedFrom.Date <= DateTime.Now.Date)
        {
            string json = JsonConvert.SerializeObject(_state);
            File.WriteAllText(FilePaths.ParsingSettingsJsonPath, json);
        }
    }
}