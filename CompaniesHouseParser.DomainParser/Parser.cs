using CompaniesHouseParser.DomainShared;
using CompaniesHousseParser.DomainSearchFilter;

namespace CompaniesHouseParser.DomainParser
{
    public class Parser : IParser
    {
        private IDomainFilteredSearch _domainFilteredSearch;
        private IDomainCompanyEmailSender _emailSender;

        public Parser(
            IDomainFilteredSearch domainFilteredSearch,
            IDomainCompanyEmailSender emailSender
            )
        {
            _domainFilteredSearch = domainFilteredSearch;
            _emailSender = emailSender;
        }

        public async Task ExecuteAsync()
        {
            var companies = await GetFilteredCompaniesAsync();
            foreach (var company in companies)
            {
                await _emailSender.SendAsync(company);
            }
        }

        private async Task<IList<ICompany>> GetFilteredCompaniesAsync()
        {
            return await _domainFilteredSearch.GetFilteredCompaniesAsync();
        }
    }
}
