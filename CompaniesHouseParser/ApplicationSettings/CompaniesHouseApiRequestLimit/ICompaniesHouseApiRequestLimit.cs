namespace CompaniesHouseParser.Settings
{
    public interface ICompaniesHouseApiRequestLimit
    {
        int Count { get; }
        TimeSpan Interval { get; }
    }
}