namespace BuberDinner.Api.Controllers;

using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
// [ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        //Register Service 
        ErrorOr<AuthenticationResult> registerResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        // MatchFirst() returns the first error
        return registerResult.Match(
            registerResult => Ok(MapAuthResult(registerResult)),
            errors => Problem(errors)
        // pass errors to ApiController's Problem()
        // firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );

        /* Match() returns a list of errors 
        return registerResult.Match(
            registerResult => Ok(MapAuthResult(registerResult)),
            _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "User already exists.")
        );
        */
    }

    // mapping method 
    // shortcut to create the method => select all the properties => extract method
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        //Register Service 
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password
        );

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }
}