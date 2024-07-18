using CompaniesHouseParser.ExportData.FileDataServise;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.ExportData.FileData.Base
{
    public interface IParserFileDataService : ISingletonDependency
    {
        Task<FileSaveResult> SaveAsync(object value, string fileName);
    }
}
