using System.Reflection;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Common.Behaviors;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
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
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        /*
        services.AddScoped<
            IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>,
            ValidationBehavior>();
        */

        // services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}