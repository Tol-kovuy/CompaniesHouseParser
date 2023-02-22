namespace CompaniesHouseParser.DomainApi
{
    public interface IDomainCompaniesApi
    {
        Task<IList<ICompany>> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi);
    }
}
