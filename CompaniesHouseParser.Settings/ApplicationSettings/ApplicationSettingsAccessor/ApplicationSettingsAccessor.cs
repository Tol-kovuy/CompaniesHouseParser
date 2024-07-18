using CompaniesHouseParser.Common;
using CompaniesHouseParser.Shared;

namespace CompaniesHouseParser.Settings;

public class ApplicationSettingsAccessor
   : AccessorBase<ApplicationSettings, IApplicationSettings>,
    IApplicationSettingsAccessor
{
    public ApplicationSettingsAccessor()
        : base(FilePaths.ApplicationSettingsJsonFilePath)
    {
    }
}
