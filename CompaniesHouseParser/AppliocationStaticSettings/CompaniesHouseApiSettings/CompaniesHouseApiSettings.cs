﻿using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser
{
    public class CompaniesHouseApiSettings : ICompaniesHouseApiSettings
    {
        public string Token { get; set; }
        public string BaseUrl { get; set; }
        public int SearchCompaniesPerRequest { get; set; }
        public ICompaniesHouseApiRequestLimit RequestLimit { get; set; }
    }
}
