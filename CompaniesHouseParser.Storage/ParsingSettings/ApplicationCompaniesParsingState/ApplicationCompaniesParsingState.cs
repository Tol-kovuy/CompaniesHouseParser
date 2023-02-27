namespace CompaniesHouseParser.Storage;

public class ApplicationCompaniesParsingState : IApplicationCompaniesParsingState
{
    public DateTime LastIncorporatedFrom { get; set; }
    public DateTime LastIncorporatedTo { get; set; }
}
