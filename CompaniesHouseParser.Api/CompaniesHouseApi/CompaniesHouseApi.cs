using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class CompaniesHouseApi : ICompaniesHouseApi
    {
        private const string _apiBaseUrl = "https://api.company-information.service.gov.uk";

        private readonly HttpClientFactory _clientFactory = new HttpClientFactory();

        public async Task<IList<CompanyDto>> GetCompaniesAsync(IGetCompaniesRequest requestApi)
        {
            var incorporatedFrom = GetDate(requestApi.IncorporatedFrom);
            var incorporatedTo = GetDate(DateTime.UtcNow.AddDays(1)); 
            var countCompanies = requestApi.CompaniesCount;
            var url = $"{_apiBaseUrl}/advanced-search/companies?incorporated_from={incorporatedFrom}" +
                             $"&incorporated_to={incorporatedTo}&size={countCompanies}";

            var apiToken = requestApi.ApiToken;
            var response = await GetResponse<CompaniesListDto>(url, apiToken);


            return response.Companies;
        }

        public async Task<IList<OfficerDto>> GetOfficers(IGetOfficerRequest requestApi)
        {
            var url = $"{_apiBaseUrl}/company/{requestApi.CompanyId}/officers";

            var response = await GetResponse<OfficersListDto>(url, requestApi.ApiToken);


            return response.Officers;
        }

        private async Task<TClass> GetResponse<TClass>(string url, string token)
        {
            var httpClient = _clientFactory.GetHttpClient(token);

            using var response = await httpClient.GetAsync(url);

            var request = await response.Content.ReadAsStringAsync();
            Console.WriteLine(request);

            TClass? jsonToObj = default(TClass);
            try
            {
                jsonToObj = JsonConvert.DeserializeObject<TClass>(request);
                if (jsonToObj == null)
                    throw new Exception("Sorry, but json file can not be deserialize to object");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message : {ex}");
                throw;
            }

            
            return jsonToObj;
        }
        private string GetDate(DateTime date) // maybe is this extension method also? to do like Base64Encode?
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
