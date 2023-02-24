namespace CompaniesHouseParser.Storage
{
    public interface IApplicationStorageCreatedDateCompany
    {
        void AddRange(IList<DateTime> dates);
        IList<DateTime> GetValuesExistCompanies();
    }
}
