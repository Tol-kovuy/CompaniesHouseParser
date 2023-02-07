using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.Api
{
    public class HttpClientFactory
    {
        public HttpClient CreateHttpClient()
        {
            var accessorAppSettings = new ApplicationSettingsAccessor();
            var getApiSettings = accessorAppSettings.Get();

            var companiesHouseAuthorizationHandler = new CompaniesHouseAuthorizationHandler
            {
                InnerHandler = getApiSettings.CompaniesHouseApi.HttpMessageHandlerCreator()
            };

            var httpClient = new HttpClient(companiesHouseAuthorizationHandler)
            {
                BaseAddress = getApiSettings.CompaniesHouseApi.BaseUrl
            };

            return httpClient;
        }
    }
}
