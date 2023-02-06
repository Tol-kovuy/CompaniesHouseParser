namespace CompaniesHouseParser.Settings
{
    public class ApplicationCompanyFilter : IApplicationCompanyFilter
    {
        public IApplicationCompanyOfficerFilter Officer { get; set; }
    }
}
