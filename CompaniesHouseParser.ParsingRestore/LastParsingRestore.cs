using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.ExportData;
using CompaniesHouseParser.Settings;
using CompaniesHouseParser.Shared;
using CompaniesHousseParser.DomainSearchFilter;
using Microsoft.Extensions.Logging;

namespace CompaniesHouseParser.ParsingRestore
{
    public class LastParsingRestore : ILastParsingRestore
    {
        private List<CompanyTableModel> _сompanyModels = new List<CompanyTableModel>();

        private IDomainFilteredSearch _domainFilteredSearch;
        private IExportDaraService _exportData;
        private IApplicationSettingsAccessor _applicationSettingsAccessor;
        public ILogger Logger { get; set; }

        public LastParsingRestore(
            ILogger<LastParsingRestore> logger,
            IDomainFilteredSearch domainFilteredSearch,
            IExportDaraService exportData,
            IApplicationSettingsAccessor applicationSettingsAccessor
            )
        {
            Logger = logger;
            _domainFilteredSearch = domainFilteredSearch;
            _exportData = exportData;
            _applicationSettingsAccessor = applicationSettingsAccessor;
        }

        public async Task WriteNorParsedCompaniesAsync()
        {
            var notParsedIds = GetNotParsedIds();
            if (notParsedIds.Count == 0)
            {
                return;
            }

            var notParsedCompanies = new List<ICompany>();
            foreach (var company in notParsedIds)
            {
                var notParsedCompany = await _domainFilteredSearch.GetNotParsedCompanies(company.CompanyId);
                notParsedCompanies.Add(notParsedCompany);
            }

            await WriteParsedOfficersToResultAsync(notParsedCompanies);
        }

        public async Task<IDomainGetCompaniesResponse> GetFilteredCompaniesAsync()
        {
            return await _domainFilteredSearch.GetFilteredCompaniesAsync();
        }

        public async Task WriteParsedOfficersToResultAsync(IList<ICompany> notParsedCompanies)
        {
            if (notParsedCompanies.Count == 0)
            {
                return;
            }

            var companies = new List<CompanyTableModel>(2048);
            var nationalityFilter = _applicationSettingsAccessor.Get().Filters.Officer.Nationality;

            foreach (var company in notParsedCompanies)
            {
                var result = await company.GetOfficersAsync();
                var filteredResult = result;

                if (!string.IsNullOrEmpty(nationalityFilter))
                {
                    filteredResult = filteredResult.Where(officer => officer.Nationality == nationalityFilter).ToList();
                }

                var outputDto = filteredResult
                    .Select(officer => new CompanyTableModel()
                    {
                        CompanyId = company.Id,
                        CompanyName = company.Name,
                        CompanyCreateDate = company.CreatedDate.ToString("yyyy-MM-dd"),
                        OfficerName = officer.Name,
                        YearOfBirthday = officer.YearOfBirthday,
                        MonthOfBirthday = officer.MonthOfBirthday,
                        Position = officer.Position,
                        Role = officer.Role,
                        CountryOfResidence = officer.CountryOfResidence,
                        Country = officer.Country,
                        City = officer.City,
                        Apartments = officer.Apartments,
                        AddresLine1 = officer.AddresLine1,
                        AddresLine2 = officer.AddresLine2,
                        PostalCode = officer.PostalCode,
                        Appointments = officer.OfficerAppointments,
                    }).ToList();

                if (outputDto != null)
                {
                    companies.AddRange(outputDto);
                }
            }
            _сompanyModels.AddRange(companies);

            await WriteResultToCsvFile();
        }

        private List<CompanyTableModel> GetNotParsedIds()
        {
            if (!File.Exists(FilePaths.SuccessfulCompanyIDsFilePath))
            {
                return new List<CompanyTableModel>();
            }

            var existingCompanyNumbers = File
                .ReadAllLines(FilePaths.ExistingCompanyNumbersFilePath)
                .Distinct()
                .ToList();
            var successfulCompanyIDs = File
                .ReadAllLines(FilePaths.SuccessfulCompanyIDsFilePath)
                .Distinct()
                .ToList();

            var companiesFromTxtFile = new List<CompanyTableModel>();

            foreach (var companyNumber in existingCompanyNumbers)
            {
                if (!successfulCompanyIDs.Contains(companyNumber))
                {
                    companiesFromTxtFile.Add(new CompanyTableModel()
                    {
                        CompanyId = companyNumber
                    });
                }
            }

            return companiesFromTxtFile;
        }

        private async Task WriteResultToCsvFile()
        {
            if (_сompanyModels.Count == 0)
            {
                return;
            }

            var companiesGroupedByDate = _сompanyModels
                .GroupBy(c => c.CompanyCreateDate)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var group in companiesGroupedByDate)
            {
                var fileName = $"{group.Key}";
                var filePath = Path.Combine(FilePaths.ParsingResultsDirectoryName, fileName);
                var companiesToSave = group.Value;

                var resultToCsvTable = await _exportData.SaveToCsvAsync(companiesToSave, fileName);

                Logger.LogInformation($"File {fileName} was created/updated. \n" +
                               $"<{filePath}>");
            }
        }
    }
}
