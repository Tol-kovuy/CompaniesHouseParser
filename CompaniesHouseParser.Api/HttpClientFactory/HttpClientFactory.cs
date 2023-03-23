using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace CompaniesHouseParser.Api;

public class HttpClientFactory : IHttpClientFactory
{
    private HttpClient _httpClient;
    public ILogger Logger { get; set; }

    public HttpClientFactory(ILogger<HttpClientFactory> logger)
    {
        Logger = logger;
    }

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
            Logger.LogError("\nException Caught!");
            Logger.LogError(ex.Message);
        }
        return _httpClient;
    }
}
