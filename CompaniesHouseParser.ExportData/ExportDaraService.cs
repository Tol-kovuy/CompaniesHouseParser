using CompaniesHouseParser.ExportData.FileDataServise;
using CompaniesHouseParser.ExportData.ParserFileData.Csv;

namespace CompaniesHouseParser.ExportData
{
    public class ExportDaraService : IExportDaraService
    {
        private readonly IParserCsvDataService _csvDataService;

        public ExportDaraService(IParserCsvDataService csvDataService)
        {
            _csvDataService = csvDataService;
        }

        public async Task<FileSaveResult> SaveToCsvAsync(object value, string fileName)
        {
            return await _csvDataService.SaveAsync(value, fileName);
        }
    }
}
