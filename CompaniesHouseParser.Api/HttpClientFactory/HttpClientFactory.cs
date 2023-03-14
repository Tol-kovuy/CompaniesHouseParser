using CompaniesHouseParser.Logging;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace CompaniesHouseParser.Api;

public class HttpClientFactory : IHttpClientFactory
{
    private HttpClient _httpClient;
    private readonly ILogging _logger;

    public HttpClientFactory(ILogging logger)
    {
        _logger = logger;
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
            _logger.GetLogger<HttpClientFactory>(LogLevel.Error, "\nException Caught!");
            _logger.GetLogger<HttpClientFactory>(LogLevel.Error, ex.Message);
        }
        return _httpClient;
    }
}
