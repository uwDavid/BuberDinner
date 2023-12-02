using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authService)
    {
        _authenticationService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRquest req)
    {
        var authResult = _authenticationService.Register(req.FirstName, req.LastName, req.Email, req.Password);

        // map to response
        var res = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );

        return Ok(res);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRquest req)
    {
        var authResult = _authenticationService.Login(req.Email, req.Password);

        // map to response
        var res = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );

        return Ok(res);
    }
}