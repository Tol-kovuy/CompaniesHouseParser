using CompaniesHouseParser.Shared;

namespace CompaniesHouseParser.ExportData.FileDataServise.Base
{
    public abstract class FileDataService : IFileDataService
    {
        private readonly string _fileExtension;

        protected string RootDirectory
        {
            get
            {
                return Path.Combine(FilePaths.ParsingResultsDirectoryName) + Path.DirectorySeparatorChar;
            }
        }

        protected FileDataService(
            string fileExtension
            )
        {
            _fileExtension = fileExtension;
        }

        protected abstract Task WriteToAsync(FileSaveRequest request, FileSaveResult result);

        public async Task<FileSaveResult> SaveAsync(FileSaveRequest request)
        {
            var result = CreateResult(request);
            await WriteToAsync(request, result);

            return result;
        }

        protected virtual FileSaveResult CreateResult(FileSaveRequest request)
        {
            var absoluteFilePath = Path.Combine(RootDirectory, request.FileName) + _fileExtension;
            var result = new FileSaveResult(absoluteFilePath);
            EnsureDirectoryExists(result.FileDirectoryPath);

            return result;
        }

        protected static void EnsureDirectoryExists(string pathToDirecttory)
        {
            if (!Directory.Exists(pathToDirecttory))
            {
                Directory.CreateDirectory(pathToDirecttory);
            }
        }
    }
}
