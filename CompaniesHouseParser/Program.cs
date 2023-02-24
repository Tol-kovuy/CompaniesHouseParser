using CompaniesHouseParser.Api;
using CompaniesHouseParser.Settings;
using Newtonsoft.Json;
using System.Net;
using CompaniesHouseParser.Email;
using CompaniesHouseParser.Storage;
using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.Search;

namespace CompaniesHouseParser;


class Program
{
    static async Task Main()
    {
        ICompaniesHouseApi companiesHouseApi = new CompaniesHouseApi();
        ICompanyHouseParsingStateAccessor companyHouseParsingStateAccessor = new CompanyHouseParsingStateAccessor();
        IApplicationSettingsAccessor applicationSettingsAccessor = new ApplicationSettingsAccessor();
        IApplicationSettings applicationSettings = new ApplicationSettings();
        IApplicationStorageCompanyIds applicationStorageCompanyIds = new ApplicationStorageCompanyIds();

        IDomainCompaniesApi _domainCompaniesApi = new DomainCompaniesApi(companiesHouseApi, applicationSettingsAccessor);
        ICompanyHouseParsingStateAccessor _parsingStateAccessor = new CompanyHouseParsingStateAccessor();
        IApplicationStorageCompanyIds _applicationStorageCompanyIds = new ApplicationStorageCompanyIds();
        ApplicationStorageCreatedDateCompany _applicationStorageCreatedDate = new ApplicationStorageCreatedDateCompany();

        ApplicationStorageCreatedDateCompany dateCompany = new ApplicationStorageCreatedDateCompany();
        DomainSearch domain = new DomainSearch(_domainCompaniesApi, _parsingStateAccessor, _applicationStorageCompanyIds, _applicationStorageCreatedDate);

        // IList<DateTime> dateTimes = new List<DateTime>
        // {
        //     new DateTime(2023, 1, 1),
        //     new DateTime(2023, 1, 2),
        //     new DateTime(2023, 1, 3),
        //     new DateTime(2023, 1, 4),
        //     new DateTime(2023, 1, 5)
        // };

        // //dateCompany.AddRange(dateTimes);
        //var date = dateCompany.GetDates();

        // var lastDate = domain.GetLastDate(dateTimes);



        IDomainCompaniesApi domainCompaniesApi = new DomainCompaniesApi(companiesHouseApi,
            applicationSettingsAccessor);

        var d = new DomainSearch(domainCompaniesApi, companyHouseParsingStateAccessor,
            applicationStorageCompanyIds, _applicationStorageCreatedDate);
        var companies = await d.GetCompanies(d.GetIncorporatedDate());//(DateTime.UtcNow.AddDays(-1));
        d.SaveNewCompanyValuesToFile(companies);

        using (StreamReader read = new StreamReader("CreatedDate.txt"))
        {
            var str = read.ReadToEnd();
        }




        #region Testing Domain




        //var domainRequest = new DomainGetCompaniesRequest
        //{
        //    IncorporatedFrom = companyHouseParsingStateAccessor.Get().Companies.LastIncorporatedFrom
        //};
        //var domain = new DomainCompaniesApi(companiesHouseApi, applicationSettingsAccessor);
        //var getCompanies = await domain.GetCompaniesAsync(domainRequest);

        //var companies = new List<Company>();
        //foreach (ICompany company in getCompanies)
        //{
        //    companies.Add(new Company(companiesHouseApi, applicationSettingsAccessor.Get())
        //    {
        //        Id = company.Id,
        //        Name = company.Name,
        //        CreatedDate = company.CreatedDate
        //    });
        //}

        //var officers = new List<IOfficer>();
        //foreach (ICompany company in companies)
        //{
        //    var getOfficer = await company.GetOfficersAsync();
        //    officers.AddRange(getOfficer);
        //}

        #endregion

        #region Testing to save/read file

        //var list = new List<string>(); 
        //list.Add("00000A");
        //list.Add("00000B");
        //list.Add("00000C");
        //list.Add("00000D");
        //list.Add("00000E"); 
        //list.Add("00000F");
        //list.Add("00000G");


        //var storage = new ApplicationStorageCompanyIds();  


        //storage.AddRange(list);

        //var arrayOfIds = storage.GetValuesExistCompanies();

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

        #region Getting 5 companies from Api

        //HttpClientFactory client = new HttpClientFactory();
        //var createHttpClient = client.CreateHttpClient();

        //await GetAllCompaniesFromDto();

        //async Task GetAllCompaniesFromDto()
        //{
        //    var url = $"https://api.company-information.service.gov.uk/" +
        //        $"advanced-search/getCompanies?incorporated_from={DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")}" +
        //        $"&incorporated_to={DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")}&countCompanies={5000}";

        //    var response = await createHttpClient.GetAsync(url);
        //    Console.WriteLine(response.ToString());

        //    var request = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine(request);

        //    var getCompanies = new List<CompanyDto>();
        //    var companyList = JsonConvert.DeserializeObject<CompaniesListDto>(request);
        //    getCompanies.AddRange(companyList.Companies);
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
