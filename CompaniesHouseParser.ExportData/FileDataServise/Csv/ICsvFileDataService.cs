using CompaniesHouseParser.ExportData.FileDataServise.Base;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.ExportData.FileDataServise.Csv
{
    public interface ICsvFileDataService 
        : IFileDataService
        , ISingletonDependency
    {
    }
}
