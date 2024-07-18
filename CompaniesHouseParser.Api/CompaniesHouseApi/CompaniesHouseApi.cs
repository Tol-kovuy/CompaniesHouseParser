using CompaniesHouseParser.Settings;
using CompaniesHouseParser.Shared;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using System.Net;

namespace CompaniesHouseParser.Api;

public class CompaniesHouseApi : ICompaniesHouseApi
{
    private readonly IApplicationSettingsAccessor _requestLimit;
    private DateTime _lastResponseDate = DateTime.MinValue;
    private readonly IHttpClientFactory _clientFactory;
    public ILogger Logger { get; set; }
    private List<string> _lines = new List<string>();

    public CompaniesHouseApi(
        ILogger<CompaniesHouseApi> logger,
        IHttpClientFactory clientFactory,
        IApplicationSettingsAccessor requestLimit
        )
    {
        Logger = logger;
        _clientFactory = clientFactory;
        _requestLimit = requestLimit;
    }

    private void SetLastResponseDate()
    {
        _lastResponseDate = DateTime.UtcNow;
    }

    private TimeSpan GetTimeElapsedSinceLastRequest()
    {
        var difference = DateTime.UtcNow - _lastResponseDate;
        return TimeSpan.FromMilliseconds(difference.TotalMilliseconds);
    }

    private async Task DelayBetweenRequest()
    {
        var delaybetweenRequests = _requestLimit.Get().CompaniesHouseApi.RequestLimit.Interval;
        var timeElapsedSinceLastRequest = GetTimeElapsedSinceLastRequest();
        var delayInterval = delaybetweenRequests - timeElapsedSinceLastRequest;
        if (delayInterval.TotalMilliseconds > 0)
        {
            await Task.Delay(delayInterval);
        }
    }

    public async Task<IList<CompanyDto>> GetCompaniesAsync(IGetCompaniesRequest requestApi)
    {
        var incorporatedFrom = GetDate(requestApi.SearchIncorporatedFrom);
        var incorporatedTo = incorporatedFrom;
        var countCompanies = requestApi.CompaniesCount;
        var searchingByNationality = _requestLimit.Get().Filters.Officer.Nationality;

        var startIndex = 0;
        var result = new List<CompanyDto>(countCompanies);
        var apiToken = requestApi.ApiToken;

        Logger.LogInformation($"Get Companies From: {incorporatedFrom} To: {incorporatedFrom} peroid.\n");
        Logger.LogInformation($"Searching by nationality: {searchingByNationality}.\n");
        await Task.Delay(5000);

        while (true)
        {
            var url = $"{FilePaths.ApiBaseUrl}/advanced-search/companies?incorporated_from={incorporatedFrom}" +
            $"&incorporated_to={incorporatedTo}" +
            $"&size={countCompanies}" +
            $"&start_index={startIndex}";

            var response = await GetResponse<CompaniesListDto>(url, apiToken);
            if (response == null)
            {
                break;
            }

            var responseCompanies = response.Companies;
            if (responseCompanies == null)
            {
                break;
            }

            result.AddRange(responseCompanies);

            if (responseCompanies.Count < countCompanies)
            {
                break;
            }

            startIndex += responseCompanies.Count;
        }

        result = result
            .DistinctBy(x => x.Id)
            .ToList();

        return result;
    }

    public async Task<IList<OfficerDto>> GetOfficers(IGetOfficerRequest requestApi)
    {
        var startIndex = 0;
        var result = new List<OfficerDto>();

        while (true)
        {
            string url = $"{FilePaths.ApiBaseUrl}/company/{requestApi.CompanyId}/officers?start_index={startIndex}";

            var response = await GetResponse<OfficersListDto>(url, requestApi.ApiToken);
            if (response == null)
            {
                break;
            }

            var responseOfficers = response.Officers;
            if (responseOfficers == null)
            {
                break;
            }

            if (responseOfficers.Count == 0)
            {
                break;
            }

            Logger.LogInformation($"Get Officers by id company - {requestApi.CompanyId}");

            WriteSuccessfulParsedIdsToFile(requestApi.CompanyId);

            result.AddRange(responseOfficers);

            startIndex += responseOfficers.Count;
        }

        var activeOfficers = result
            .Where(o => o.ResignedOn == null)
            .ToList();
        return activeOfficers;
    }

    public async Task<CompanyDto> GetCompanyById(IGetOfficerRequest requestApi)
    {
        var url = $"{FilePaths.ApiBaseUrl}/company/{requestApi.CompanyId}";
        var apiToken = requestApi.ApiToken;

        var response = await GetResponse<CompanyDto>(url, apiToken);
        Logger.LogInformation($"Get Company cache file. Now id company is - {requestApi.CompanyId}");

        return response;
    }

    private async Task<TClass> GetResponse<TClass>(string url, string token)
    {
        await DelayBetweenRequest();

        var httpClient = _clientFactory.GetHttpClient(token);

        string request;
        try
        {
            using var response = await httpClient.GetAsync(url);
            Logger.LogInformation(response.ToString()); 

            if (response.StatusCode == HttpStatusCode.BadGateway)
            {
                request = await RetryRequestsAsync(url, token);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Logger.LogWarning($"The resource at '{url}' was not found.");
            }
             
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                request = await DelayedRetryRequestsAsync(url, token);
            }

            request = await response.Content.ReadAsStringAsync();
            Logger.LogInformation(request);
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex.Message, "An error occurred while processing the request.");
            request = await DelayedRetryRequestsAsync(url, token);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An error occurred while processing the request.");
            throw;
        }

        SetLastResponseDate();

        TClass? jsonToObj = default;
        try
        {
            if (string.IsNullOrWhiteSpace(request))
            {
                Logger.LogWarning("Empty JSON data received.");
                return jsonToObj;
            }

            jsonToObj = JsonConvert.DeserializeObject<TClass>(request);
            if (jsonToObj == null)
            {
                Logger.LogWarning("Sorry, but json file could not be deserialized to object: " + request);
                return jsonToObj;
            }
        }
        catch (JsonReaderException ex)
        {
            string message = $"JsonReaderException caught while deserializing JSON: {ex.Message}";
            Logger.LogError(message);
            return jsonToObj;
        }
        catch (Exception ex)
        {
            string message = $"An error occurred while deserializing JSON: {ex.Message}";
            Logger.LogError(message);
            throw;
        }

        return jsonToObj;
    }

    private async Task<string> DelayedRetryRequestsAsync(string url, string token)
    {
        Logger.LogWarning($"{HttpStatusCode.TooManyRequests}. Weit 5 minuts. Dont't close the window!");
        await Task.Delay(_requestLimit.Get().CompaniesHouseApi.RequestLimit.WaitingTime);
        var request = await RetryRequestsAsync(url, token);

        return request;
    }

    private string GetDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }

    private async Task<string> RetryRequestsAsync(string url, string token)
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttemp => _requestLimit.Get().CompaniesHouseApi.RequestLimit.Interval);

        var httpClient = _clientFactory.GetHttpClient(token);
        using var response = await retryPolicy.ExecuteAsync(async () => await httpClient.GetAsync(url));
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }

    private void WriteSuccessfulParsedIdsToFile(string companyId)
    {
        var filePath = FilePaths.AbsolutePathToParsedOfficers;

        string directoryPath = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (HasCompanyIdInFile(filePath, companyId))
        {
            Logger.LogWarning($"This company id {companyId} was parsed");
            return;
        }

        File.AppendAllText(filePath, companyId + Environment.NewLine);
        _lines.Add(companyId);
    }

    private bool HasCompanyIdInFile(string filePath, string id)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }

        if (_lines.Count == 0)
        {
            _lines = new List<string>(File.ReadAllLines(filePath));
        }

        foreach (string line in _lines)
        {
            if (line.Trim().Equals(id.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
