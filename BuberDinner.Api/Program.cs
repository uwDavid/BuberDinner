using BuberDinner.Api.Errors;
using BuberDinner.Api.Filters;
// using BuberDinner.Api.Middleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// option 2: add filter attribute to controller
// we can do it here to add it globally
// or add [ErrorHandlingFilter] tag to the controllers we want
// builder.Services.AddControllers(
//     options => options.Filters.Add<ErrorHandlingFilterAttribute>()
// );

builder.Services.AddControllers();

// builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
// we moved this line to Applicaiton Layer, by using the DependencyInjection.Abstractions package
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// Opt 3 step 2: custom ProblemDetails factory w additional properties
// custom Problem() to add custom properties
// builder.Services.AddSingleton<ProblemDetailsFactory, BDProblemDetailsFactory>();

var app = builder.Build();

// Option 1: ErrorHandlingMiddleware.cs - returns a simple error message on all errors
// app.UseMiddleware<ErrorHandlingMiddleware>();

// Option 3: Exception Endpoint
// This adds a middleware to the pipeline => catch Exception, log, reset req path, re-execute
app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext httpContext) =>
{
    Exception? ex = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Results.Problem();
    // in source => there's a dictionary of extensions for additional properties
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

