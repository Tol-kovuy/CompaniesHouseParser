using CompaniesHouseParser.ExportData.FileDataServise;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.ExportData
{
    public interface IExportDaraService : ISingletonDependency
    {
        Task<FileSaveResult> SaveToCsvAsync(object value, string fileName);
    }
}
