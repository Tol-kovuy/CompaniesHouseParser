using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Settings;

public interface ISmtp : ITransientDependency
{
    string Email { get; set; }
    string Host { get; set; }
    string Password { get; set; }
    int Port { get; set; }
    string UserName { get; set; }
}