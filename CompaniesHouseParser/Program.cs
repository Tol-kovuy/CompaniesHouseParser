using CompaniesHouseParser.Api;
using CompaniesHouseParser.Settings;
using Newtonsoft.Json;

namespace CompaniesHouseParser
{
    //todo: CompaniesHouseParser.Settings -> Done.
    //todo: CompaniesHouseParser.Api
    //todo: CompaniesHouseParser.App
    class Program
    {
        static async Task Main()
        {
            HttpClientFactory authorizeClient = new HttpClientFactory();
            var forSeeThatVariable = authorizeClient.CreateHttpClient();
            await GetAllCompaniesFromDto();
             async Task<IList<CompanyModelDto>> GetAllCompaniesFromDto()
            {
                var incorporatedFrom = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
                var incorporatedTo = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd");
                // TODO use takeCountm
                var countCompanies = 5;
                var url = $"https://api.company-information.service.gov.uk/advanced-search/companies?incorporated_from={incorporatedFrom}&incorporated_to={incorporatedTo}&countCompanies={countCompanies}";

                var companies = new List<CompanyModelDto>();

                var response = new HttpResponseMessage();
                try
                {
                    response = await forSeeThatVariable.GetAsync(url);
                    Console.WriteLine(response.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine($"Message : {ex}");
                }

                var request = await response.Content.ReadAsStringAsync();
                Console.WriteLine(request);

                try
                {
                    var companyList = JsonConvert.DeserializeObject<CompaniesListModelDto>(request);
                    companies.AddRange(companyList.CompaniesDto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine($"Message : {ex}");
                }

                return companies;
            }

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
            //        ILastIncorporatedFrom = DateTime.Now
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
}