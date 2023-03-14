using CompaniesHouseParser.DomainParser;
using CompaniesHouseParser.IoC;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace CompaniesHouseParser;

class Program
{
    static async Task Main()
    {
        //var allAssemblies = AppDomain.CurrentDomain.GetAllAssemblies(); <---- eto xernya)))
        var allAssemblies = MyAssemblies.GetAllAssemblies();
        var filtredAssemblies = MyAssemblies.GetFiltredAssemlies(allAssemblies);

        IServiceCollection services = new ServiceCollection();

        services.RegisterAssemblyPublicNonGenericClasses(filtredAssemblies.ToArray())
            .Where(typeOfClass => typeof(ITransientDependency).IsAssignableFrom(typeOfClass))
            .AsPublicImplementedInterfaces(ServiceLifetime.Transient);

        services.RegisterAssemblyPublicNonGenericClasses(filtredAssemblies.ToArray())
            .Where(typeOfClass => typeof(ISingletonDependency).IsAssignableFrom(typeOfClass))
            .AsPublicImplementedInterfaces(ServiceLifetime.Singleton);

        var serviceProvider = services.BuildServiceProvider();
        var app = serviceProvider.GetRequiredService<IParser>();
        await app.ExecuteAsync();
    }
}

