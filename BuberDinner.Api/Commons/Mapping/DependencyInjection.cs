using System.Reflection;
using Mapster;
using MapsterMapper;

namespace BuberDinner.Api.Commons.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        // scan assembly
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        // add mapping config
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        // Service Mapper = Mapper w DI features
        return services;
    }
    // we still need to call AddMapping() in program.cs
}