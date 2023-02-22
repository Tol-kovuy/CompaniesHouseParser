namespace CompaniesHouseParser.Api
{
    public interface ICompaniesHouseApi
    {
        Task<IList<CompanyDto>> GetCompaniesAsync(IGetCompaniesRequest request);
        Task<IList<OfficerDto>> GetOfficers(IGetOfficerRequest request);
    }
}