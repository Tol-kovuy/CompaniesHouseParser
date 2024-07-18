using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings;

public interface ICompaniesHouseApiRequestLimit : ITransientDependency
{
    int Count { get; }
    TimeSpan Interval { get; }
    TimeSpan WaitingTime { get; set; }
}