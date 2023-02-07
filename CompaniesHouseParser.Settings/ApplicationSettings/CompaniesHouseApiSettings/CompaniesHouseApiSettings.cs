namespace CompaniesHouseParser.Settings
{
    public class CompaniesHouseApiSettings : ICompaniesHouseApiSettings
    {
        public string Token { get; set; }
        Uri BaseUrl { get; set; }
        Uri ICompaniesHouseApiSettings.BaseUrl => BaseUrl;
        public int SearchCompaniesPerRequest { get; set; }
        public CompaniesHouseApiRequestLimit RequestLimit { get; set; }
        ICompaniesHouseApiRequestLimit ICompaniesHouseApiSettings.RequestLimit
        {
            get => RequestLimit;
        }
        public Func<HttpMessageHandler> HttpMessageHandlerCreator { get; }
    }
}
