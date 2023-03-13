using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Api
{
    public interface IHttpClientFactory : ISingletonDependency
    {
        HttpClient GetHttpClient(string token);
    }
}