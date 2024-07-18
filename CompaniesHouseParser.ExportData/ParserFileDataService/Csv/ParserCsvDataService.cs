using CompaniesHouseParser.ExportData.FileData.Base;
using CompaniesHouseParser.ExportData.FileDataServise.Base;
using CompaniesHouseParser.Storage;

namespace CompaniesHouseParser.ExportData.ParserFileData.Csv
{
    public class ParserCsvDataService
        : ParserFileDataService,
        IParserCsvDataService
    {
        public ParserCsvDataService(
            IFileDataService fileDataService,
            IApplicationStorageCreatedDateCompany applicationStorageCreatedDate
            ) : base(fileDataService, applicationStorageCreatedDate)
        {
        }
    }
}
