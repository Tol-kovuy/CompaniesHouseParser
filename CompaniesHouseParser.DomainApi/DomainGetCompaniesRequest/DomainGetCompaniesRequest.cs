namespace CompaniesHouseParser.DomainApi;

public class DomainGetCompaniesRequest : IDomainGetCompaniesRequest
{
    public DateTime IncorporatedFrom { get; set; }
    public DateTime SearchIncorporatedFrom { get; set; }
}
