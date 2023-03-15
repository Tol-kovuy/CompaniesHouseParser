namespace CompaniesHouseParser.DomainShared
{
    public interface ICompany
    {
        string Id { get; set; }
        string Name { get; set; }
        DateTime CreatedDate { get; set; }
        Task<IList<IOfficer>> GetOfficersAsync();
        Task<bool> HasOfficerWithNationalityAsync(string nationality);
    }
}