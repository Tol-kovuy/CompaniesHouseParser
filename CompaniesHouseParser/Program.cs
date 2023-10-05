using CompaniesHouseParser.DomainParser;
using CompaniesHouseParser.IoC;
using CompaniesHouseParser.Profile;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using NetEscapades.Extensions.Logging.RollingFile;

namespace CompaniesHouseParser;

class Program
{
    public static IServiceCollection ServicesRegistration()
    {
        var allAssemblies = SolutionAssemblies.GetAllAssemblies();
        var filtredAssemblies = SolutionAssemblies.GetFiltredAssemlies(allAssemblies);

        IServiceCollection services = new ServiceCollection();

        services.RegisterAssemblyPublicNonGenericClasses(filtredAssemblies.ToArray())
            .Where(typeOfClass => typeof(ITransientDependency).IsAssignableFrom(typeOfClass))
            .AsPublicImplementedInterfaces(ServiceLifetime.Transient);

        services.RegisterAssemblyPublicNonGenericClasses(filtredAssemblies.ToArray())
            .Where(typeOfClass => typeof(ISingletonDependency).IsAssignableFrom(typeOfClass))
            .AsPublicImplementedInterfaces(ServiceLifetime.Singleton);

        services.AddAutoMapper(typeof(MapperProfile));


        services.AddLogging(builder =>
            {
                builder.AddSimpleConsole(options =>
                    {
                        options.TimestampFormat = "HH:mm:ss   ";
                    });
                //Uncommet for loggin in txt file

               builder.AddFile(options =>
               {
                   options.FileName = "parserLogs";
                   options.LogDirectory = AppContext.BaseDirectory;
                   options.FileSizeLimit = 20 * 1024 * 1024;
                   options.FilesPerPeriodicityLimit = 10;
                   options.Extension = "txt";
                   options.Periodicity = PeriodicityOptions.Daily;
               });
            });
       
        return services;
    }
    static async Task Main()
    {
        var services = ServicesRegistration();
        var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<IParser>();
        await app.ExecuteAsync();
    }
}

