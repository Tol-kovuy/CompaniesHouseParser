using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser
{
    public interface ICompaniesHouseApiSettings
    {
        string BaseUrl { get; set; }
        ICompaniesHouseApiRequestLimit RequestLimit { get; set; }
        int SearchCompaniesPerRequest { get; set; }
        string Token { get; set; }
    }
}