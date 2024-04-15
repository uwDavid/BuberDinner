using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Commons.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api;

// This allows the Application layer to manage its own dependencies 
// We move the Authenticaiton Service declaration from Api to here
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();  //in Mapping/DependencyInjection.cs
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, BDProblemDetailsFactory>();

        return services;
    }
}