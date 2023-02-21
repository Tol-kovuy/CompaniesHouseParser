namespace CompaniesHouseParser.DomainApi
{
    public interface IDomainGetCompaniesRequest
    {
        int CompaniesCount { get; set; }
        DateTime IncorporatedFrom { get; set; }
    }
}