using NetCore.AutoRegisterDi;

namespace CompaniesHouseParser.Settings;

public class CompaniesHouseApiRequestLimit : ICompaniesHouseApiRequestLimit
{
    public int Count { get; set; } // The maximum number of results matching the search term(s) to return with a range of 1 to 5000.
    public TimeSpan Interval { get; set; } // 5 min
}
