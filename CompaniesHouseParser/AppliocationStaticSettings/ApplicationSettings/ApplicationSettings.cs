namespace CompaniesHouseParser.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public ICompaniesHouseApiSettings CompaniesHouseApi { get; set; }
        public ISmtp Smtp { get; set; }
        public IApplicationCompanyFilter[] Filters { get; set; }
    }
}
