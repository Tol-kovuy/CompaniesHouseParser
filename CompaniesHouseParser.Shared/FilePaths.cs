namespace CompaniesHouseParser.Shared
{
    public static class FilePaths
    {
        public static string ApiBaseUrl { get; } = "https://api.company-information.service.gov.uk";
        public static string AbsolutePathToParsedOfficers { get; } = @"SuccessfulParsedOfficersByCompanyID//SuccessfulCompanyIDs.txt";
        public static string AllCompaniesDirectoryName { get; } = "AllCompanyIds";
        public static string AllCompaniesFileName { get; } = "allCompanyIds.txt";
        public static string ExistingCompanyNumbersFilePath { get; } = @"ExistingActiveCompanyIds//ExistingCompanyNumbers.txt";
        public static string SuccessfulCompanyIDsFilePath { get; } = @"SuccessfulParsedOfficersByCompanyID//SuccessfulCompanyIDs.txt";
        public static string ParsingResultsDirectoryName { get; } = "ParsingResults";
        public static string ApplicationSettingsJsonFilePath { get; } = @"ApplicationSettings\\StaticSettings.json";
        public static string ExistingActiveCompaniesDirectoryName { get; } = @"ExistingActiveCompanyIds";
        public static string ExistingCompaniesFileName { get; } = "ExistingCompanyNumbers.txt";
        public static string ParsingSettingsJsonPath { get; } = @"ParsingSettings\\ModifiedSettings.json";
    }
}
