namespace CompaniesHouseParser.Settings
{
    public class CompaniesHouseApiSettings : ICompaniesHouseApiSettings
    {
        public string Token { get; set; }
        public string BaseUrl { get; set; }
        public int SearchCompaniesPerRequest { get; set; }
        public CompaniesHouseApiRequestLimit RequestLimit { get; set; }
        ICompaniesHouseApiRequestLimit ICompaniesHouseApiSettings.RequestLimit
        {
            get => RequestLimit;
        }
    }
}
