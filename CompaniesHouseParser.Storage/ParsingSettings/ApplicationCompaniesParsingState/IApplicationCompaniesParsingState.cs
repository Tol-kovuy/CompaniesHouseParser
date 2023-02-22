namespace CompaniesHouseParser.Storage;

public interface IApplicationCompaniesParsingState
{
    DateTime LastIncorporatedFrom { get; set; }
}