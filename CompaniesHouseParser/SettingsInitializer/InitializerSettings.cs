﻿using CompaniesHouseParser.Settings;
using CompaniesHouseParser.Storage;

namespace CompaniesHouseParser.Api
{
    public class InitializerSettings : IInitializerSettings
    {
        private IApplicationSettingsAccessor _settingsAccessor;
        private ICompanyHouseParsingStateAccessor _stateAccessor;

        public InitializerSettings()
        {
            _settingsAccessor = new ApplicationSettingsAccessor();
            _stateAccessor = new CompanyHouseParsingStateAccessor();
        }

        public IGetCompaniesRequest InitializeSettingsForCompanies()
        {
            var settings = new GetAllCompaniesRequest()
            {
                CompaniesCount = _settingsAccessor.Get().CompaniesHouseApi.SearchCompaniesPerRequest,
                IncorporatedFrom = _stateAccessor.Get().Companies.LastIncorporatedFrom,
                ApiToken = _settingsAccessor.Get().CompaniesHouseApi.Token
            };

            return settings;
        }
         public IGetOfficerRequest InitializerSettingsForOfficer()
        {
            var settings = new GetOfficerRequest()
            {
                ApiToken = _settingsAccessor.Get().CompaniesHouseApi.Token
            };

            return settings;
        }
    }
}
