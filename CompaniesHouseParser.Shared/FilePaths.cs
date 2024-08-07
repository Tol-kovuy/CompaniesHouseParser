﻿namespace CompaniesHouseParser.Shared
{
    public static class FilePaths
    {
        public static string ApiBaseUrl { get; } = "https://api.company-information.service.gov.uk";
        
        public static string AllCompaniesDirectoryName { get; } = "AllCompanyIds";
        public static string AllCompaniesFileName { get; } = "allCompanyIds.txt";
        public static string AbsoluteAllCompaniesPath { get; } = @"AllCompanyIds//allCompanyIds.txt";

        public static string AbsoluteExistingCompanyNumbersFilePath { get; } = @"ExistingActiveCompanyIds//ExistingCompanyNumbers.txt";
        public static string ExistingActiveCompaniesDirectoryName { get; } = @"ExistingActiveCompanyIds";
        public static string ExistingCompaniesFileName { get; } = "ExistingCompanyNumbers.txt";

        public static string AbsoluteSuccessfulCompanyIDsFilePath { get; } = @"SuccessfulParsedOfficersByCompanyID//SuccessfulCompanyIDs.txt";

        public static string ParsingResultsDirectoryName { get; } = "ParsingResults";

        public static string ApplicationSettingsJsonFilePath { get; } = @"ApplicationSettings\\StaticSettings.json";

        public static string ParsingSettingsJsonPath { get; } = @"ParsingSettings\\ModifiedSettings.json";
    }
}
