﻿using CompaniesHouseParser.Logging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using System.Net;

namespace CompaniesHouseParser.Api;

public class CompaniesHouseApi : ICompaniesHouseApi
{
    private const string _apiBaseUrl = "https://api.company-information.service.gov.uk";
    private DateTime _lastResponseDate = DateTime.MinValue;
    private const double _delayFromMileseconds = 0.5;
    private readonly ILogging _logger;
    private readonly IHttpClientFactory _clientFactory;

    public CompaniesHouseApi(ILogging logger,
        IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _clientFactory = clientFactory;
    }

    private void SetLastResponseDate()
    {
        _lastResponseDate = DateTime.UtcNow;
    }

    private TimeSpan GetTimeElapsedSinceLastRequest()
    {
        var difference = DateTime.UtcNow - _lastResponseDate;
        return difference;
    }

    private async Task DelayBetweenRequest()
    {
        var delaybetweenRequests = TimeSpan.FromSeconds(_delayFromMileseconds);// from config;
        var timeElapsedSinceLastRequest = GetTimeElapsedSinceLastRequest();
        var delayInterval = delaybetweenRequests - timeElapsedSinceLastRequest;
        if (delayInterval.TotalMilliseconds > 0)
        {
            await Task.Delay(delayInterval);
        }
    }

    public async Task<IList<CompanyDto>> GetCompaniesAsync(IGetCompaniesRequest requestApi)
    {
        await DelayBetweenRequest();
        var incorporatedFrom = GetDate(requestApi.IncorporatedFrom);
        var incorporatedTo = GetDate(DateTime.UtcNow.AddDays(1));
        var countCompanies = requestApi.CompaniesCount;
        var url = $"{_apiBaseUrl}/advanced-search/companies?incorporated_from={incorporatedFrom}" +
                         $"&incorporated_to={incorporatedTo}&size={countCompanies}";

        var apiToken = requestApi.ApiToken;
        var response = await GetResponse<CompaniesListDto>(url, apiToken);
        SetLastResponseDate();
        return response.Companies;
    }

    public async Task<IList<OfficerDto>> GetOfficers(IGetOfficerRequest requestApi)
    {
        await DelayBetweenRequest();
        var url = $"{_apiBaseUrl}/company/{requestApi.CompanyId}/officers";
        var response = await GetResponse<OfficersListDto>(url, requestApi.ApiToken);
        SetLastResponseDate();
        if (response.Officers != null)
        {
            return response.Officers;
        }
        return new List<OfficerDto>();
    }

    private async Task<TClass> GetResponse<TClass>(string url, string token)
    {
        var httpClient = _clientFactory.GetHttpClient(token);

        string request;
        try
        {
            using var response = await httpClient.GetAsync(url);
            _logger.GetLogger<CompaniesHouseApi>(LogLevel.Information, response.ToString());

            if (response.StatusCode == HttpStatusCode.BadGateway)
            {
                request = await RetryRequestsAsync(url, token);
            }
            else
            {
                request = await response.Content.ReadAsStringAsync();
                _logger.GetLogger<CompaniesHouseApi>(LogLevel.Information, new string('-', 100));
                _logger.GetLogger<CompaniesHouseApi>(LogLevel.Information, request);
            }
        }
        catch (Exception)
        {
            throw;
        }

        TClass? jsonToObj = default(TClass);
        try
        {
            jsonToObj = JsonConvert.DeserializeObject<TClass>(request);
            if (jsonToObj == null)
            {
                throw new Exception("Sorry, but json file can not be deserialize to object");
            }
        }
        catch (Exception ex)
        {
            _logger.GetLogger<CompaniesHouseApi>(LogLevel.Error, "\nException Caught!");
            _logger.GetLogger<CompaniesHouseApi>(LogLevel.Error, ex.Message);
            throw;
        }
        return jsonToObj;
    }
    private string GetDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }
    
    private async Task<string> RetryRequestsAsync(string url, string token)
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttemp => TimeSpan.FromMilliseconds(_delayFromMileseconds));

        var httpClient = _clientFactory.GetHttpClient(token);
        using var response = await retryPolicy.ExecuteAsync(async () => await httpClient.GetAsync(url));
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}
