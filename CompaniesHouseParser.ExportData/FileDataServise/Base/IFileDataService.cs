using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.ExportData.FileDataServise.Base
{
    public interface IFileDataService : ISingletonDependency
    {
        Task<FileSaveResult> SaveAsync(FileSaveRequest request);
    }
}
