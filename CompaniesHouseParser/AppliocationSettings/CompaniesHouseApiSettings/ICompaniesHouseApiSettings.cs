namespace CompaniesHouseParser.Settings
{
    public interface ICompaniesHouseApiSettings
    {
        string BaseUrl { get; }
        string Token { get; }
        int SearchCompaniesPerRequest { get; }
        ICompaniesHouseApiRequestLimit RequestLimit { get; }
    }
}