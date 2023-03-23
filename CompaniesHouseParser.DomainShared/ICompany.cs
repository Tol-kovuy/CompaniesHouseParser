using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.DomainShared;

public interface ICompany : ITransientDependency
{
    string Id { get; }
    string Name { get; set; }
    string FullAddress { get; }
    string City { get; }
    string PostalCode { get; }
    string Country { get; }
    int SicCodes { get; }
    DateTime CreatedDate { get; }
    Task<IList<IOfficer>> GetOfficersAsync();
    Task<bool> HasOfficerWithNationalityAsync(string nationality);
}