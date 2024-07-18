using CompaniesHouseParser.ExportData.FileDataServise;
using CompaniesHouseParser.ExportData.FileDataServise.Base;
using CompaniesHouseParser.Storage;
using System.Text.RegularExpressions;

namespace CompaniesHouseParser.ExportData.FileData.Base
{
    public class ParserFileDataService : IParserFileDataService
    {
        private readonly IApplicationStorageCreatedDateCompany _applicationStorageCreatedDate;
        private readonly IFileDataService _fileDataService;

        public ParserFileDataService(
            IFileDataService fileDataService,
            IApplicationStorageCreatedDateCompany applicationStorageCreatedDate
            )
        {
            _fileDataService = fileDataService;
            _applicationStorageCreatedDate = applicationStorageCreatedDate;
        }

        public virtual async Task<FileSaveResult> SaveAsync(object value, string fileName)
        {
            var request = new FileSaveRequest(fileName, value);

            return await _fileDataService.SaveAsync(request);
        }
    }
}
