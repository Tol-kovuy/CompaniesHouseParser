namespace CompaniesHouseParser.Settings;

public class ApplicationCompanyFilter : IApplicationCompanyFilter
{
    public ApplicationCompanyOfficerFilter Officer { get; set; }
    IApplicationCompanyOfficerFilter IApplicationCompanyFilter.Officer { get => Officer; }
}
