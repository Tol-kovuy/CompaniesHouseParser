namespace CompaniesHouseParser.Storage
{
    public interface IApplicationStorageCreatedDateCompany
    {
        DateTime GetDate();
        void ReWriteIncorporatedFrom(IList<DateTime> dates);
    }
}
