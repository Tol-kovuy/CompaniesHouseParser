namespace CompaniesHouseParser.Settings
{
    public class ApplicationParsingState : IApplicationParsingState
    {
        public IApplicationCompaniesParsingState Companies { get; set; }
        IApplicationCompaniesParsingState IApplicationParsingState.Companies
        {
            get => Companies;
        }
    }
}
