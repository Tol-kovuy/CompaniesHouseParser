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
        if (_lastIncorporatedDate != DateTime.MinValue)
        {
            return;
        }
        _lastIncorporatedDate = Get().Companies.LastIncorporatedFrom;
    }

    public DateTime GetDate()
    {
        CheckIsFileUploaded();
        return _lastIncorporatedDate;
    }

    public void ReWriteIncorporatedDateFrom(DateTime dates)
    {
        CheckIsFileUploaded();
        var parsingState = new ApplicationParsingState();
        parsingState.Companies = new ApplicationCompaniesParsingState();
        parsingState.Companies.LastIncorporatedFrom = dates;
        SaveLastDate(parsingState);
    }

    private void SaveLastDate(ApplicationParsingState parsingState)
    {
        string json = JsonConvert.SerializeObject(parsingState);
        File.WriteAllText(pathToCreatedDates, json);
    }
}