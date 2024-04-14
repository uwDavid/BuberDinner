using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

// This allows the Application layer to manage its own dependencies 
// We move the Authenticaiton Service declaration from Api to here
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        return services;
    }
}