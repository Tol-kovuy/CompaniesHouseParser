﻿using NetCore.AutoRegisterDi;
using System.Net;

namespace CompaniesHouseParser.Settings;

public class CompaniesHouseApiSettings : ICompaniesHouseApiSettings
{
    public string Token { get; set; }
    public int SearchCompaniesPerRequest { get; set; }
    public CompaniesHouseApiRequestLimit RequestLimit { get; set; }

    public DateTime SearchIncorporatedFrom { get; set; }

    ICompaniesHouseApiRequestLimit ICompaniesHouseApiSettings.RequestLimit => RequestLimit;
}
