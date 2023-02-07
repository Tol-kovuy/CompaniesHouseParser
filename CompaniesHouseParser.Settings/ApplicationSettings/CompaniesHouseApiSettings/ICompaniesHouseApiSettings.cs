namespace CompaniesHouseParser.Settings
{
    public interface ICompaniesHouseApiSettings
    {
        Uri BaseUrl { get; }
        string Token { get; }
        int SearchCompaniesPerRequest { get; }
        ICompaniesHouseApiRequestLimit RequestLimit { get; }
        Func<HttpMessageHandler> HttpMessageHandlerCreator { get; }
    }
}