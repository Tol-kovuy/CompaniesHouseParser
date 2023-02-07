namespace CompaniesHouseParser.Settings
{
    public interface IApplicationSettings
    {
        ICompaniesHouseApiSettings CompaniesHouseApi { get; }
        IApplicationCompanyFilter[] Filters { get; }
        ISmtp Smtp { get; }
        INotificationFor Email { get; }
    }
}