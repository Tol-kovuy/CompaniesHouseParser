using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Api
{
    public interface IGetCompaniesRequest : ITransientDependency
    {
        int CompaniesCount { get; set; }
        DateTime IncorporatedFrom { get; set; }
        string ApiToken { get; set; }
    }
}