namespace CompaniesHouseParser.Storage
{
    public interface IApplicationStorageCompanyIds
    {
        void AddRange(IList<string> ids);
        IList<string> GetIdsExistCompanies();
    }
}