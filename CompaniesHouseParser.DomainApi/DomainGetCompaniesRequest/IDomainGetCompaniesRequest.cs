using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.DomainApi;

public interface IDomainGetCompaniesRequest : ITransientDependency
{
    DateTime IncorporatedFrom { get; set; }
    DateTime SearchIncorporatedFrom { get; set; }
}