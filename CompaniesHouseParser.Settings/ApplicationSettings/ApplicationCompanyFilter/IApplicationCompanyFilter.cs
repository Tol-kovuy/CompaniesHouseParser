using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings;

public interface IApplicationCompanyFilter : ITransientDependency
{
    IApplicationCompanyOfficerFilter Officer { get; }
}