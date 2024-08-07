﻿using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Api;

public interface IGetCompaniesRequest : ITransientDependency
{
    int CompaniesCount { get; set; }
    string ApiToken { get; set; }
    DateTime SearchIncorporatedFrom { get; set; }
}