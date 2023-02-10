using System.Net.Http.Headers;
using System.Text;

namespace CompaniesHouseParser.Api
{
    public class HttpClientFactory
    {
        private HttpClient _httpClient;

        public HttpClient GetHttpClient(string token)
        {
            if (_httpClient != null)
            {
                return _httpClient;
            }

            _httpClient = new HttpClient();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic", token.Base64Encode());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message : {ex}");
            }
            return _httpClient;
        }
    }
}
