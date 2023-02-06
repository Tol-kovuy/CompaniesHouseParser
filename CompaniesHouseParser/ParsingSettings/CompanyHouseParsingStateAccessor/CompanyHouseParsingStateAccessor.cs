namespace CompaniesHouseParser.Settings
{
    public class CompanyHouseParsingStateAccessor
       : AccessorBase<ApplicationParsingState, IApplicationParsingState>,
       ICompanyHouseParsingStateAccessor
    {
        public CompanyHouseParsingStateAccessor()
            : base("ModifiedSettings.json")
        {
        }
    }
}
