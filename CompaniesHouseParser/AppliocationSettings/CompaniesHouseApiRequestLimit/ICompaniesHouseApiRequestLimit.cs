namespace CompaniesHouseParser.Settings
{
    public interface ICompaniesHouseApiRequestLimit
    {
        int Count { get; set; }
        TimeSpan Interval { get; set; }
    }
}