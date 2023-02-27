using CompaniesHouseParser.Common;
using Newtonsoft.Json;

namespace CompaniesHouseParser.Storage;

public class ApplicationStorageCreatedDateCompany
    : AccessorBase<ApplicationParsingState, IApplicationParsingState>
    , IApplicationStorageCreatedDateCompany
{
    private static string pathToCreatedDates = @"ParsingSettings\\ModifiedSettings.json";
    private DateTime _lastIncorporatedDate;
    public ApplicationStorageCreatedDateCompany()
        : base(pathToCreatedDates)
    {
    }

    private void CheckIsFileUploaded()
    {
        _lastIncorporatedDate = Get().Companies.LastIncorporatedFrom;
    }

    public DateTime GetDate()
    {
        CheckIsFileUploaded();
        return _lastIncorporatedDate;
    }

    public void ReWriteIncorporatedDateFrom(DateTime dates)
    {
        _lastIncorporatedDate = Get().Companies.LastIncorporatedFrom;
        ApplicationParsingState parsingState = new ApplicationParsingState
        {
            Companies = new ApplicationCompaniesParsingState { LastIncorporatedFrom = _lastIncorporatedDate }
        };

        //_parstingState.Companies.LastIncorporatedFrom = lastDate;
        //Save();


    }

    private void Save(ApplicationParsingState parsingState)
    {
        string json = JsonConvert.SerializeObject(parsingState);
        File.WriteAllText(pathToCreatedDates, json);
    }
}