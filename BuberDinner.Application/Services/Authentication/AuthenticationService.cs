using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator generator)
    {
        _jwtTokenGenerator = generator;
    }

    public AuthenticationResult Login(string email, string password)
    {


        return new AuthenticationResult(Guid.NewGuid(), "firstName", "lastName", email, "token");
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if user exists

        // Create user (generate unique ID)
        Guid userId = Guid.NewGuid();
        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

        return new AuthenticationResult(userId, firstName, lastName, email, token);
    }
}