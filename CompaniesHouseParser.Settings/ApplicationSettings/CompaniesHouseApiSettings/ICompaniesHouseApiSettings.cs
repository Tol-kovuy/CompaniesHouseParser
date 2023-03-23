using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings;

public interface ICompaniesHouseApiSettings : ITransientDependency
{
    string Token { get; }
    int SearchCompaniesPerRequest { get; }
    ICompaniesHouseApiRequestLimit RequestLimit { get; }
}