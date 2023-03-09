namespace CompaniesHouseParser.Settings
{
    public interface IResultMailingAddress
    {
        string EmailAddressFrom { get; }
        string EmailAddressTo { get; }
    }
}