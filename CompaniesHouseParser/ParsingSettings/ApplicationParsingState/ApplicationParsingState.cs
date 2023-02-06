namespace CompaniesHouseParser.Settings
{
    public class ApplicationParsingState : IApplicationParsingState
    {
        public ApplicationCompaniesParsingState Companies { get; set; }
        IApplicationCompaniesParsingState IApplicationParsingState.Companies
        {
            get => Companies;
        }
    }
}
