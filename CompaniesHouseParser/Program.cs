using CompaniesHouseParser.DomainParser;
using CompaniesHouseParser.IoC;
using CompaniesHouseParser.Profile;
using CompaniesHouseParser.SharedHelpers;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;


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

        services.AddSingleton<IMappingHelper, MappingHelper.MappingHelper>();
        //services.AddAutoMapper(typeof(Program));
        services.AddAutoMapper(typeof(MapperProfile));

        return services;
    }
    /// <summary>
    /// TODO: 
    /// 1. request retry <----DONE
    /// 2. Serilog ILogger and replace Console.Writeline -> ILogger.Log... <----DONE
    /// 3.AutoMaper
    /// </summary>
    /// <returns></returns>
    static async Task Main()
    {
        var services = ServicesRegistration();
        var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<IParser>();
        await app.ExecuteAsync();
    }
}

