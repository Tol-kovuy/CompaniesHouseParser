# CompaniesHouseParser
Console application which use CompaniesHouse Advanced Search API to get all companies and officers within range.

## This application is designed to automatically search for an employee by nationality at https://www.gov.uk/government/organisations/companies-house.

### To set up this application, you will need:
- Set up an smtp mail server from which the found employees will be sent
- Get api token key from https://www.gov.uk/government/organisations/companies-house.
- Enter these settings into the application and configure it, for this you need only two files with configurations in the root directory
  - StaticSettings
  - ParsingSettings\ModifiedSettings