namespace CompaniesHouseParser.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public CompaniesHouseApiSettings CompaniesHouseApi { get; set; }
        public Smtp Smtp { get; set; }
        public ApplicationCompanyFilter[] Filters { get; set; }
        ICompaniesHouseApiSettings IApplicationSettings.CompaniesHouseApi { get => CompaniesHouseApi; }

        IApplicationCompanyFilter[] IApplicationSettings.Filters  { get => Filters; }

        ISmtp IApplicationSettings.Smtp { get => Smtp; }
    }
}
