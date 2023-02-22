using CompaniesHouseParser.Common;

namespace CompaniesHouseParser.Storage;

public class CompanyHouseParsingStateAccessor
    : AccessorBase<ApplicationParsingState, IApplicationParsingState>
    , ICompanyHouseParsingStateAccessor
{
    public CompanyHouseParsingStateAccessor()
        : base("ModifiedSettings.json")
    {
    }
}
