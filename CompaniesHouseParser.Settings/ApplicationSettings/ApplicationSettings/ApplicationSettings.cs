namespace CompaniesHouseParser.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public CompaniesHouseApiSettings CompaniesHouseApi { get; set; }
        ICompaniesHouseApiSettings IApplicationSettings.CompaniesHouseApi { get => CompaniesHouseApi; }

        public Smtp Smtp { get; set; }
        ISmtp IApplicationSettings.Smtp { get => Smtp; }

        public ApplicationCompanyFilter Filters { get; set; }
        IApplicationCompanyFilter IApplicationSettings.Filters  { get => Filters; }
        public NotificationFor Email { get; set; }
        INotificationFor IApplicationSettings.Email => Email;
    }
}
