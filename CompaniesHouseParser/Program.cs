using CompaniesHouseParser.Settings;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace CompaniesHouseParser
{
    //todo: CompaniesHouseParser.Settings -> 
    //todo: CompaniesHouseParser.Api
    //todo: CompaniesHouseParser.App
    class Program
    {
        static void Main()
        {
            #region Deserialize

            //var accessorAppSettings = new ApplicationSettingsAccessor();
            //var getAppSettings = accessorAppSettings.Get();

            //var accessorParsSettings = new CompanyHouseParsingStateAccessor();
            //var getParsSettings = accessorParsSettings.Get();

            #endregion

            #region Serialize

            ApplicationSettings staticSettings = new ApplicationSettings
            {
                CompaniesHouseApi = new CompaniesHouseApiSettings
                {
                    BaseUrl = "https://api.company-information.service.gov.uk",
                    SearchCompaniesPerRequest = 5,
                    Token = "ndfkdn-f889e4tuhdfv-fdllfdn",
                    RequestLimit = new CompaniesHouseApiRequestLimit { Count = 600, Interval = TimeSpan.FromMilliseconds(700) }
                },
                Filters = new[] { new ApplicationCompanyFilter { Officer = new ApplicationCompanyOfficerFilter { Nationality = "British" }, },
                      new ApplicationCompanyFilter { Officer = new ApplicationCompanyOfficerFilter { Nationality = "Israeli" }, } },

                Smtp = new Smtp
                {
                    Email = "simple@gmail.com",
                    UserName = "Fox",
                    Password = "password",
                    Host = "gmail.com",
                    Port = 333
                }
            };

            var jsonStringStaticSettings = JsonConvert.SerializeObject(staticSettings);
            WriteToFile("StaticSettings.json", jsonStringStaticSettings);

            //ApplicationParsingState modifiedSettings = new ApplicationParsingState
            //{
            //    Companies = new ApplicationCompaniesParsingState
            //    {
            //        ILastIncorporatedFrom = DateTime.Now
            //    }
            //};

            //var jsonStringModifiedSettings = JsonConvert.SerializeObject(modifiedSettings);
            //WriteToFile("ModifiedSettings.json", jsonStringModifiedSettings);

            void WriteToFile(string filePath, string text)
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine(text);
                }
            }
            
            #endregion
        }
    }
}