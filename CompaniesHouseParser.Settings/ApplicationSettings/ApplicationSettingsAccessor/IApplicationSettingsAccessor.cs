using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings;

public interface IApplicationSettingsAccessor : ITransientDependency
{
    IApplicationSettings Get();
}