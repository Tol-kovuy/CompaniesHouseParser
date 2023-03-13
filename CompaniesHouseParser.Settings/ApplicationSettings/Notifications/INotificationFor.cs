using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings
{
    public interface INotificationFor : ITransientDependency
    {
        IResultMailingAddress EmailAddresses { get; }
    }
}