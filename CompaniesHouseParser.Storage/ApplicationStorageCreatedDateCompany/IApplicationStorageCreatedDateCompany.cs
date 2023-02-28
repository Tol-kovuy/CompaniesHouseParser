namespace CompaniesHouseParser.Storage
{
    public interface IApplicationStorageCreatedDateCompany
    {
        DateTime GetLastIncorporatedFromDate();
        void SetLastIncorporatedFromDate(DateTime dates);
    }
}
