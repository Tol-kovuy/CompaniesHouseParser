using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings;

public interface IApplicationSettings : ITransientDependency
{
    ICompaniesHouseApiSettings CompaniesHouseApi { get; }
    IApplicationCompanyFilter Filters { get; }
    ISmtp Smtp { get; }
    INotificationFor Email { get; }
    string Subject { get; }
}