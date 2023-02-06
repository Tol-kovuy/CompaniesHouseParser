namespace CompaniesHouseParser.Settings
{
    public interface IApplicationCompanyFilter
    {
        IApplicationCompanyOfficerFilter Officer { get; }
    }
}