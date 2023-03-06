using CompaniesHouseParser.Api;
using CompaniesHouseParser.Settings;
using Newtonsoft.Json;
using System.Net;
using CompaniesHouseParser.Email;
using CompaniesHouseParser.Storage;
using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.Search;
using System.Text.RegularExpressions;
using CompaniesHouseParser.DomainShared;
using CompaniesHousseParser.DomainSearchFilter;
using CompaniesHouseParser.DomainParser;

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
        IApplicationStorageCreatedDateCompany _applicationStorageCreatedDate = new ApplicationStorageCreatedDateCompany();

        ApplicationStorageCreatedDateCompany dateCompany = new ApplicationStorageCreatedDateCompany();
        DomainSearch domain = new DomainSearch(_domainCompaniesApi, _applicationStorageCompanyIds, _applicationStorageCreatedDate);
        IEmailMessageBuilder emailMessageBuilder = new EmailMessageBuilder();

        IDomainCompaniesApi domainCompaniesApi = new DomainCompaniesApi(companiesHouseApi,
            applicationSettingsAccessor);

        var d = new DomainSearch(domainCompaniesApi,
            applicationStorageCompanyIds, _applicationStorageCreatedDate);

        //IGetOfficerRequest request = new GetOfficerRequest
        //{
        //    ApiToken = "d7f521f7-9e49-48d3-8225-c89cc860f9ad",
        //    CompanyId = "OE028097"
        //};
        //var error = await companiesHouseApi.GetOfficers(request);

        var domainFilteredSearch = new DomainFilteredSearch(domain, applicationSettingsAccessor);

        var parsingAndSend100companies =
            new Parser(domainFilteredSearch, emailMessageBuilder, applicationSettingsAccessor);
        await parsingAndSend100companies.SendResult();




        //var getCompaniesWithOfficers = await domainFilteredSearch.GetFilteredCompaniesAsync();
        //var getOfficersByNation = await domainFilteredSearch.GetNewlyIncorporatedCompaniesAsync();



        //var companies = await d.GetNewlyIncorporatedCompaniesAsync();
        //var companies = new List<ICompany>(); 
        //var companyy = new Company(companiesHouseApi, applicationSettingsAccessor)
        //{
        //    Id = "OE028097",// id with out officers
        //    Name = "Name",
        //    CreatedDate = new DateTime(2023, 2, 12)
        //};
        //companies.Add(companyy);


        //var listOfficers = new List<IOfficer>();
        //foreach (var company in companies)
        //{
        //    var requestInterval = applicationSettingsAccessor.Get().CompaniesHouseApi.RequestLimit.Interval;
        //    await Task.Delay(requestInterval);
        //    var officer = await company.GetOfficersAsync();
        //    listOfficers.AddRange(officer);
        //}

        //IList<DateTime> list = new List<DateTime>();
        //list.Add(new DateTime(2023, 1, 1));
        //list.Add(new DateTime(2023, 1, 3));
        //list.Add(new DateTime(2023, 1, 5));
        //list.Add(new DateTime(2023, 1, 7));
        //list.Add(new DateTime(2023, 2, 25));

        //GetLastDate(list);

        //DateTime GetLastDate(IList<DateTime> dates)
        //{
        //    var date = dates.Max();
        //    return date;
        //}
        var ids = File.ReadAllLines("ExistingCompanyNumbers.txt");
        var listIds = new List<string>();
        listIds.AddRange(ids);
        var repeatsIdsInList = listIds.GroupBy(group => group)
                                      .Where(group => group.Count() > 1)
                                      .Select(group => group.Key);


        Console.WriteLine(string.Join(Environment.NewLine, repeatsIdsInList));



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
        //    officers.AddNewIds(getOfficer);
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


        //storage.AddNewIds(list);

        //var arrayOfIds = storage.GetIds();

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
        //        $"advanced-domainFilteredSearch/getCompanies?incorporated_from={DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")}" +
        //        $"&incorporated_to={DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")}&countCompanies={5000}";

        //    var response = await createHttpClient.GetAsync(url);
        //    Console.WriteLine(response.ToString());

        //    var request = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine(request);

        //    var getCompanies = new List<CompanyDto>();
        //    var companyList = JsonConvert.DeserializeObject<CompaniesListDto>(request);
        //    getCompanies.AddNewIds(companyList.Companies);
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
        //    officers.AddNewIds(officerList._officers);

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
        //        EmailAddresses = new[] { new ResultMailingAddress { EmailAddressFrom = "recipient1@gmail.com"}, 
        //                                 new ResultMailingAddress { EmailAddressFrom = "recipient2@gmail.com"},
        //                                 new ResultMailingAddress { EmailAddressFrom = "recipient3@gmail.com"}},
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
