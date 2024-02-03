
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

// This allows the Application layer to manage its own dependencies 
// We move the Authenticaiton Service declaration from Api to here
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        return services;
    }
}