namespace CompaniesHouseParser.DomainApi
{
    public interface ICompany
    {
        string Id { get; }
        string Name { get; }
        string CreatedDate { get; }
        Task<IList<IOfficer>> GetOfficersAsync();
    }
}