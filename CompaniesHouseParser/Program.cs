using CompaniesHouseParser.Settings;
using Newtonsoft.Json;


var accessorAppSettings = new ApplicationSettingsAccessor();
var getAppSettings = accessorAppSettings.Get();

var accessorParsSettings = new CompanyHouseParsingStateAccessor();
var getParsSettings = accessorParsSettings.Get();

#region Serialize

ApplicationSettings settings = new ApplicationSettings
{
    CompaniesHouseApi = new CompaniesHouseApiSettings
    {
        BaseUrl = "https://api.company-information.service.gov.uk",
        SearchCompaniesPerRequest = 5,
        Token = "ndfkdn-f889e4tuhdfv-fdllfdn",
        RequestLimit = new CompaniesHouseApiRequestLimit { Count = 600, Interval = TimeSpan.FromMilliseconds(700) }
    },
    Filters = new[] { new ApplicationCompanyFilter { Officer = new ApplicationCompanyOfficerFilter { Nationality = "British" }, },
                      new ApplicationCompanyFilter { Officer = new ApplicationCompanyOfficerFilter { Nationality = "Israeli" }, } },

    Smtp = new Smtp
    {
        Email = "simple@gmail.com",
        UserName = "Fox",
        Password = "password",
        Host = "gmail.com",
        Port = 333
    }
};

var jsonStringAppSettings = JsonConvert.SerializeObject(settings);

ApplicationParsingState sett = new ApplicationParsingState
{
    Companies = new ApplicationCompaniesParsingState
    {
        ILastIncorporatedFrom = new DateTime(2023 - 1 - 1)
    }
};

 void WriteToFile(string filePath)
{

}

#endregion