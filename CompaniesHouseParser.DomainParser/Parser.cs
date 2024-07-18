using CompaniesHouseParser.ParsingRestore;
using Microsoft.Extensions.Logging;

namespace CompaniesHouseParser.DomainParser;

public class Parser : IParser
{
    private bool _isAppStartsFirstTime;
    public ILogger Logger { get; set; }

    private ILastParsingRestore _lastParsing;

    public Parser(
        ILogger<Parser> logger,
        ILastParsingRestore lastParsing)
    {
        Logger = logger;
        _lastParsing = lastParsing;
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

        await ExecuteAsync();
    }
}