namespace CompaniesHouseParser.Storage
{
    public interface IApplicationStorageCreatedDateCompany
    {
        DateTime GetDate();
        void ReWriteIncorporatedDateFrom(DateTime dates);
    }
}
