using CompaniesHouseParser.CleanCacheFileMng;
using CompaniesHouseParser.ParsingRestore;
using CompaniesHouseParser.Shared;
using Microsoft.Extensions.Logging;

namespace CompaniesHouseParser.DomainParser;

public class Parser : IParser
{
    private bool _isAppStartsFirstTime;
    public ILogger Logger { get; set; }

    private ILastParsingRestore _lastParsing;
    private readonly ICleanFileManager _cleanFile;

    public Parser(
        ILogger<Parser> logger,
        ILastParsingRestore lastParsing,
        ICleanFileManager cleanFile
        )
    {
        Logger = logger;
        _lastParsing = lastParsing;
        _cleanFile = cleanFile;
    }

    public async Task ExecuteAsync()
    {
        if (!_isAppStartsFirstTime)
        {
            await _lastParsing.WriteNorParsedCompaniesAsync();
            _isAppStartsFirstTime = true;
        }

        var response = await _lastParsing.GetFilteredCompaniesAsync();

        if (!response.CanFetchMoreCompanies)
        {
            Logger.LogInformation($"No newly created companies for at this time {DateTime.Now}");

            return;
        }

        await _lastParsing.WriteParsedOfficersToResultAsync(response.Companies);

        CleanCacheFiles();

        await ExecuteAsync();
    }

    private void CleanCacheFiles()
    {
        var pathList = new List<string>
        {
            FilePaths.AbsoluteAllCompaniesPath,
            FilePaths.AbsoluteExistingCompanyNumbersFilePath,
            FilePaths.AbsoluteSuccessfulCompanyIDsFilePath
        };

        _cleanFile.CleanFiles(pathList);
    }
}