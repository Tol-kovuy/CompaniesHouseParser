namespace CompaniesHouseParser.Settings
{
    public class NotificationFor : INotificationFor
    {
        public ResultMailingAddress[] EmailAddresses { get; set; }

        IResultMailingAddress[] INotificationFor.EmailAddresses { get => EmailAddresses; }
    }
}
