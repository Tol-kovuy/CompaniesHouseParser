namespace CompaniesHouseParser.Settings
{
    public class ApplicationParsingState : IApplicationParsingState
    {
        public ApplicationCompaniesParsingState Companies { get; set; }
        IApplicationCompaniesParsingState IApplicationParsingState.Companies => Companies;
        public NotificationFor Email { get; set; }
        INotificationFor IApplicationParsingState.Email => Email;
    }
}
