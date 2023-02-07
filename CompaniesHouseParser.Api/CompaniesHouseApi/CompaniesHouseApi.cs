using CompaniesHouseParser.Settings;
using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class CompaniesHouseApi : ICompaniesHouseApi
    {
        private readonly IApplicationSettingsAccessor _appSettings = new ApplicationSettingsAccessor();
        private readonly ICompanyHouseParsingStateAccessor _parsSettings = new CompanyHouseParsingStateAccessor();
        private HttpClientFactory _client = new HttpClientFactory();

        public async Task<IList<CompanyDto>> GetAllCompanies()
        {
            var incorporatedFrom = _parsSettings.Get().Companies.ILastIncorporatedFrom;
            var incorporatedTo = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
            var getSettings = _appSettings.Get();
            var countCompanies = getSettings.CompaniesHouseApi.SearchCompaniesPerRequest;
            var apiBaseUrl = _appSettings.Get().CompaniesHouseApi.BaseUrl;
            var url = $"{apiBaseUrl}advanced-search/companies?incorporated_from={incorporatedFrom}&incorporated_to={incorporatedTo}&countCompanies={countCompanies}";

            var createHttpClient = _client.CreateHttpClient();
            var response = new HttpResponseMessage();
            try
            {
                response = await createHttpClient.GetAsync(url);
                Console.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message : {ex}");
            }

            var request = await response.Content.ReadAsStringAsync();
            Console.WriteLine(request);

            var companies = new List<CompanyDto>();
            try
            {
                var companyList = JsonConvert.DeserializeObject<CompaniesListDto>(request);
                companies.AddRange(companyList.CompaniesDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message : {ex}");
            }

            return companies;
        }

        public async Task<IList<OfficerDto>> GetOfficers(string idCompany)
        {
            var apiBaseUrl = _appSettings.Get().CompaniesHouseApi.BaseUrl;
            var companyUrl = $"{apiBaseUrl}company/{idCompany}";
            var officersUrl = $"{companyUrl}/officers";

            var createHttpClient = _client.CreateHttpClient();

            var officers = new List<OfficerDto>();

            var response = new HttpResponseMessage();
            try
            {
                response = await createHttpClient.GetAsync(officersUrl);
                Console.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message : {ex}");
            }

            await Task.Delay(_appSettings.Get().CompaniesHouseApi.RequestLimit.Interval);

            try
            {
                var request = await response.Content.ReadAsStringAsync();
                Console.WriteLine(request);

                var officerList = JsonConvert.DeserializeObject<OfficerListDto>(request);
                officers.AddRange(officerList.Officers);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message : {ex}");
            }

            return officers;
        }
    }
}
