using System.Net;

namespace CompaniesHouseParser.Settings
{
    public class CompaniesHouseApiSettings : ICompaniesHouseApiSettings
    {
        public string Token { get; set; }
        public Uri BaseUrl { get; set; }
        Uri ICompaniesHouseApiSettings.BaseUrl => BaseUrl;
        public int SearchCompaniesPerRequest { get; set; }
        public CompaniesHouseApiRequestLimit RequestLimit { get; set; }
        ICompaniesHouseApiRequestLimit ICompaniesHouseApiSettings.RequestLimit => RequestLimit;
        public Func<HttpMessageHandler> HttpMessageHandlerCreator = () => new HttpClientHandler();
        Func<HttpMessageHandler> ICompaniesHouseApiSettings.HttpMessageHandlerCreator => HttpMessageHandlerCreator;
    }
}
