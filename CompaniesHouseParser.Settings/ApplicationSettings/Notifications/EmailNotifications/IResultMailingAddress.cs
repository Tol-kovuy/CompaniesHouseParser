using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings
{
    public interface IResultMailingAddress : ITransientDependency
    {
        string EmailAddressFrom { get; }
        string EmailAddressTo { get; }
    }
}