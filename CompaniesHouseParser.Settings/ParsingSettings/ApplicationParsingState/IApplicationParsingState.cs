namespace CompaniesHouseParser.Settings
{
    public interface IApplicationParsingState
    {
        IApplicationCompaniesParsingState Companies { get; }
        INotificationFor Email { get; }
    }
}