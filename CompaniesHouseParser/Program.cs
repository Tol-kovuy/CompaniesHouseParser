using CompaniesHouseParser.Api;
using CompaniesHouseParser.Settings;
using Newtonsoft.Json;
using System.Net;
using CompaniesHouseParser.Email;
using CompaniesHouseParser.Storage;

namespace CompaniesHouseParser;

//todo: CompaniesHouseParser.Settings -> Done.
//todo: CompaniesHouseParser.Api -> 
//todo: CompaniesHouseParser.App
class Program
{
    static async Task Main()
    {
        #region Testing to save/read file

        var list = new List<string>(); 
        list.Add("00000A");
        list.Add("00000B");
        list.Add("00000C");
        list.Add("00000D");
        list.Add("00000E");
        list.Add("00000F");
        list.Add("00000G");

        var storage = new ApplicationStorageCompanyIds();  

        storage.AddRange(list);

        var arrayOfIds = storage.GetAll();

        #endregion

        #region Test Sending Email

        //var emailBiulder = new EmailMessageBuilder(); 
        //var message = emailBiulder
        //    .WithText("Hello World")
        //    .WithSubject("READ MOTHER FUCKER")
        //    .From("krotkrotowskij@gmail.com")
        //    .ToRcepient("smarty.maks13@gmail.com")
        //    .Build();

        //var emailSmtpFactory = new EmailSmtpClientFactory();
        //var emailSmtpClient = emailSmtpFactory.Create("smtp.gmail.com", 587,
        //    new NetworkCredential 
        //    { 
        //        UserName = "krotkrotowskij@gmail.com", 
        //        Password = "cwztcchhlltrzskg" 
        //    }, 
        //    true);

        //emailSmtpClient.Send(message);

        #endregion

        //IInitializerSettings initSettings = new InitializerSettings();
        //var apiSettings = initSettings.InitializeSettingsForCompanies();

        //var companiesDto = new CompaniesHouseApi();
        //var companies = await companiesDto.GetCompanies(apiSettings);

        #region Getting 5 companies from Api

        //HttpClientFactory client = new HttpClientFactory();
        //var createHttpClient = client.CreateHttpClient();

        //await GetAllCompaniesFromDto();

        //async Task GetAllCompaniesFromDto()
        //{
        //    var url = $"https://api.company-information.service.gov.uk/" +
        //        $"advanced-search/companies?incorporated_from={DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")}" +
        //        $"&incorporated_to={DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")}&countCompanies={5000}";

        //    var response = await createHttpClient.GetAsync(url);
        //    Console.WriteLine(response.ToString());

        //    var request = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine(request);

        //    var companies = new List<CompanyDto>();
        //    var companyList = JsonConvert.DeserializeObject<CompaniesListDto>(request);
        //    companies.AddRange(companyList.Companies);
        //}

        #endregion

        #region Get Officer with one company id

        //HttpClientFactory client = new HttpClientFactory();
        //var createHttpClient = client.GetHttpClient();

        //var officer = await GetOfficers("14564685");

        //async Task<IList<OfficerDto>> GetOfficers(string idCompany)
        //{
        //    var companyUrl = $"https://api.company-information.service.gov.uk/company/{idCompany}";
        //    var officersUrl = $"{companyUrl}/officers";

        //    var response = await createHttpClient.GetAsync(officersUrl);
        //    Console.WriteLine(response.ToString());

        //    var request = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine(request);

        //    var officerList = JsonConvert.DeserializeObject<OfficersListDto>(request);
        //    var officers = new List<OfficerDto>();
        //    officers.AddRange(officerList.Officers);

        //    return officers;
        //}

        #endregion

        #region Deserialize

        //var accessorAppSettings = new ApplicationSettingsAccessor();
        //var getAppSettings = accessorAppSettings.Get();

        //var accessorParsSettings = new CompanyHouseParsingStateAccessor();
        //var getParsSettings = accessorParsSettings.Get();

        #endregion

        #region Serialize

        //ApplicationSettings staticSettings = new ApplicationSettings
        //{
        //    CompaniesHouseApi = new CompaniesHouseApiSettings
        //    {
        //        BaseUrl = "https://api.company-information.service.gov.uk",
        //        SearchCompaniesPerRequest = 5,
        //        Token = "ndfkdn-f889e4tuhdfv-fdllfdn",
        //        RequestLimit = new CompaniesHouseApiRequestLimit { Count = 600, Interval = TimeSpan.FromMilliseconds(700) }
        //    },
        //    Filters = new[] { new ApplicationCompanyFilter { Officer = new ApplicationCompanyOfficerFilter { Nationality = "British" }, },
        //          new ApplicationCompanyFilter { Officer = new ApplicationCompanyOfficerFilter { Nationality = "Israeli" }, } },

        //    Smtp = new Smtp
        //    {
        //        Email = "simple@gmail.com",
        //        UserName = "Fox",
        //        Password = "password",
        //        Host = "gmail.com",
        //        Port = 333
        //    }
        //};

        //var jsonStringStaticSettings = JsonConvert.SerializeObject(staticSettings);
        //WriteToFile("StaticSettings.json", jsonStringStaticSettings);

        //ApplicationParsingState modifiedSettings = new ApplicationParsingState
        //{
        //    Companies = new ApplicationCompaniesParsingState
        //    {
        //        LastIncorporatedFrom = DateTime.Now
        //    },
        //    Email = new NotificationFor
        //    {
        //        EmailAddresses = new[] { new ResultMailingAddress { EmailAddress = "recipient1@gmail.com"}, 
        //                                 new ResultMailingAddress { EmailAddress = "recipient2@gmail.com"},
        //                                 new ResultMailingAddress { EmailAddress = "recipient3@gmail.com"}},
        //    }
        //};

        //var jsonStringModifiedSettings = JsonConvert.SerializeObject(modifiedSettings);
        //WriteToFile("ModifiedSettings.json", jsonStringModifiedSettings);

        //void WriteToFile(string filePath, string text)
        //{
        //    using (StreamWriter writer = new StreamWriter(filePath, false))
        //    {
        //        writer.WriteLine(text);
        //    }
        //}

        #endregion
    }
}
