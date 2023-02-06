namespace CompaniesHouseParser.Settings
{
    public class CompaniesHouseApiRequestLimit : ICompaniesHouseApiRequestLimit
    {
        public int Count { get; set; } // 40
        public TimeSpan Interval { get; set; } // 5 min
    }
}
