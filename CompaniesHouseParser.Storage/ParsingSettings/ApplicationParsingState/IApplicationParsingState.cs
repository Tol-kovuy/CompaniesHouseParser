namespace CompaniesHouseParser.Storage
{
    public interface IApplicationParsingState
    {
        IApplicationCompaniesParsingState Companies { get; }
    }
}