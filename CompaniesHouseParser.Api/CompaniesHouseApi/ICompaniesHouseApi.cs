namespace CompaniesHouseParser.Api
{
    public interface ICompaniesHouseApi
    {
        Task<IList<CompanyDto>> GetCompanies(IGetCompaniesRequest request);
        Task<IList<OfficerDto>> GetOfficers(IGetOfficerRequest request);
    }
}