using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings
{
    public interface IApplicationCompanyOfficerFilter : ITransientDependency
    {
        string Nationality { get; }
    }
}