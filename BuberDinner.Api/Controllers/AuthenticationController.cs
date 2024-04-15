namespace BuberDinner.Api.Controllers;

using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
// [ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    // private readonly IMediator _mediator;
    // Mediator interface has ISender & IPublisher interface - let's use ISender
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        /* manual mapping
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        */


        //Register Service 
        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

        return registerResult.Match(
            registerResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult)),
            //registerResult => Ok(MapAuthResult(registerResult)),
            errors => Problem(errors)
        // MatchFirst() returns the first error
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
    /*
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
    */

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        // manually map
        // var query = new LoginQuery(request.Email, request.Password);

        //Register Service 
        var authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            //authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }
}