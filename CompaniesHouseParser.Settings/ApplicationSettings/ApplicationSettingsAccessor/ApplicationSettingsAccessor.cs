using CompaniesHouseParser.Common;

namespace CompaniesHouseParser.Settings;
   
public class ApplicationSettingsAccessor
   : AccessorBase<ApplicationSettings, IApplicationSettings>,
    IApplicationSettingsAccessor
{
    public ApplicationSettingsAccessor()
        : base("StaticSettings.json")
    {
    }
}
