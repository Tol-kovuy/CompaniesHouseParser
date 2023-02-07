namespace CompaniesHouseParser.Api
{
    public interface ICompaniesHouseApi
    {
        Task<IList<CompanyDto>> GetAllCompanies();
        Task<IList<OfficerDto>> GetOfficers(string idCompany);
    }
}