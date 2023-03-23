# CompaniesHouseParser
Console application which use CompaniesHouse Advanced Search API to get all companies and officers within range.

## This application is designed to automatically search for an employee by nationality at https://www.gov.uk/government/organisations/companies-house.

### To set up this application, you will need:
- Set up an smtp mail server from which the found employees will be sent
- Get api token key from https://www.gov.uk/government/organisations/companies-house.
- Enter these settings into the application and configure it, for this you need only two files with configurations in the root directory:
  - ParsingSettings\ModifiedSettings
  - StaticSettings
  
##### In the file "ModifiedSettings" you can set the date from which you want to start the search. In the LastIncorporatedFrom column, specify the year, month, and day from which you want to start the search. Please note that the default date is 2023-02-01T00:00:00. 
##### {"Companies":{"LastIncorporatedFrom":"2023-02-01T00:00:00"}} 2023-02-01 change 2023-02-01 for example 2023-03-01.
This configuration is only required the first time the application is launched. For further launches, the application itself will remember the date of the last search.

##### In the "StaticSettings" file, you need to set the following: 
- API token key, which you will generate directly on the site.
-- "CompaniesHouseApi": {
	"Token": "YOUR API token key",
    "SearchCompaniesPerRequest": 5000,
    "RequestLimit": {
      "Count": 600,
      "Interval": "00:00:00.7000000"
    }
- Smtp settings for the mailbox from which the search results will be sent.
-- "Smtp": {
    "Email": "email_sender@gmail.com", <---for example
    "UserName": "email_sender@gmail.com",
    "Password": "The password that you will receive when setting up the smtp server",
    "Host": "smtp.gmail.com", <---for example
    "Port": 587 <---for example
- Specify the email address from which messages will be sent and specify which email address messages will be sent to
-- "Email": {
    "EmailAddresses": {
      "EmailAddressFrom": "email_sender@gmail.com", <---for example
      "EmailAddressTo": "email_recipient@gmail.com" <---for example
    }