namespace CompaniesHouseParser.Storage
{
    public interface IApplicationStorageCompanyIds
    {
        void AddNewIds(IList<string> ids);
        IList<string> GetIds();
    }
}