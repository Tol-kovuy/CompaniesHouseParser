namespace CompaniesHouseParser.Settings
{
    public interface INotificationFor
    {
        IResultMailingAddress EmailAddresses { get; }
    }
}