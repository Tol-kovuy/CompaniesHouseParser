using AutoMapper;
using CompaniesHouseParser.Api;
using CompaniesHouseParser.DomainParser;
using CompaniesHouseParser.IoC;
using CompaniesHouseParser.Mapping;
using CompaniesHouseParser.MappingHelper;
using CompaniesHouseParser.Profile;
using CompaniesHouseParser.Settings;
using CompaniesHouseParser.SharedHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using System;

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
        services.AddAutoMapper(typeof(Program));
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


        //var arg2 = serviceProvider.GetService<ICompaniesHouseApi>();
        //var arg3 = serviceProvider.GetService<IApplicationSettingsAccessor>();
        //var arg1 = serviceProvider.GetService<IMapper>();
        //var mappingProfile = new MappingProfile(arg1, arg2, arg3);

        //var configuration = new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile(mappingProfile);
        //});
        //var mapper = new Mapper(configuration);

        //services.AddSingleton<IMapper>(mapper);
        //services.AddAutoMapper(typeof(MappingProfile));
        //services.AddAutoMapper();

        var app = serviceProvider.GetRequiredService<IParser>();
        await app.ExecuteAsync();
    }
}

