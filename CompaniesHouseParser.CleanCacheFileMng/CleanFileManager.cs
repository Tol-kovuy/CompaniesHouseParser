
using Microsoft.Extensions.Logging;

namespace CompaniesHouseParser.CleanCacheFileMng
{
    public class CleanFileManager : ICleanFileManager
    {
        public ILogger Logger { get; set; }

        public CleanFileManager(ILogger logger)
        {
            Logger = logger;
        }

        public void CleanFiles(IList<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                try
                {
                    if (!File.Exists(filePath))
                    {
                        Logger.LogInformation($"File not found: {filePath}");
                        continue;
                    }

                    File.Delete(filePath);
                    Logger.LogInformation($"File cleaned successfully: {filePath}");
                }
                catch (Exception ex)
                {
                    Logger.LogInformation($"Error cleaning cache file {filePath}: {ex.Message}");
                }
            }
        }
    }
}
