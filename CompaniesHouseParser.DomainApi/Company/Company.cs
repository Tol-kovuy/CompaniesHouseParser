using CompaniesHouseParser.Api;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.DomainApi
{
    public class Company : ICompany
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        private readonly ICompaniesHouseApi _companiesHouseApi;
        private readonly IApplicationSettings _applicationSettings;
        private IList<IOfficer> _officers;

        public Company(
            ICompaniesHouseApi companiesHouseApi,
            IApplicationSettings applicationSettings
            )
        {
            _companiesHouseApi = companiesHouseApi;
            _applicationSettings = applicationSettings;
        }

        public async Task<IList<IOfficer>> GetOfficersAsync()
        {
            if (_officers != null)
            {
                return _officers;
            }

            var officerRequest = new GetOfficerRequest
            {
                ApiToken = _applicationSettings.CompaniesHouseApi.Token,
                CompanyId = Id
            };

            _officers = new List<IOfficer>();
            var officersFromDto = await _companiesHouseApi.GetOfficers(officerRequest);
            foreach (var officerFromDto in officersFromDto)
            {
                var officer = new Officer()
                {
                    Name = officerFromDto.Name,
                    Role = officerFromDto.Role,
                    Nationality = officerFromDto.Nationality,
                    City = officerFromDto.Address.City,
                    Country = officerFromDto.Address.Country
                };
                _officers.Add(officer);
            }

            return _officers;
        }
    }
}
