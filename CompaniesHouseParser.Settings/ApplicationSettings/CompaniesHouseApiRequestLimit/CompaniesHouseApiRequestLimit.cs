namespace CompaniesHouseParser.Settings;

public class CompaniesHouseApiRequestLimit : ICompaniesHouseApiRequestLimit
{
    public int Count { get; set; } 
    public TimeSpan Interval { get; set; } 
    public TimeSpan WaitingTime { get; set; }
}
