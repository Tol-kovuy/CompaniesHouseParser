namespace CompaniesHouseParser.Settings
{
    public interface ICompaniesHouseApiSettings
    {
        string Token { get; }
        int SearchCompaniesPerRequest { get; }
        ICompaniesHouseApiRequestLimit RequestLimit { get; }
    }
}