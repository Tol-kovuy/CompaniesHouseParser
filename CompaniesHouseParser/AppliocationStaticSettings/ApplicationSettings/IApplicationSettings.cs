namespace CompaniesHouseParser.Settings
{
    public interface IApplicationSettings
    {
        ICompaniesHouseApiSettings CompaniesHouseApi { get; set; }
        IApplicationCompanyFilter[] Filters { get; set; }
        ISmtp Smtp { get; set; }
    }
}