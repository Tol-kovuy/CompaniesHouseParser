using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.DomainShared;

public interface ICompany : ITransientDependency
{
    string Id { get; }
    string Name { get; }
    DateTime CreatedDate { get; }
    Task<IList<IOfficer>> GetOfficersAsync();
    Task<bool> HasOfficerWithNationalityAsync(string nationality);
}