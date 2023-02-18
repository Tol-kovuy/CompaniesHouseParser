namespace CompaniesHouseParser.Storage
{
    public class ApplicationParsingState : IApplicationParsingState
    {
        public ApplicationCompaniesParsingState Companies { get; set; }
        IApplicationCompaniesParsingState IApplicationParsingState.Companies => Companies;
    }
}
