using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1. Check if user exists
        // Note syntax sugar used here for user
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
            // throw new Exception("User does not exist.");
            // Temporary implementation, this is not a secure practice
        }
        // 2. Validate password is correct
        if (user.Password != password)
        {
            // return Errors.Authentication.InvalidCredentials;
            return new[] { Errors.Authentication.InvalidCredentials };

            // throw new Exception("Invalid password.");
        }
        // 3. Create JWT token
        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
        // we modify AuthenticationResult to user, instead of individual data fields.
        // note we have to make the change on the Controller as well.
        /*
        return new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            email,
            token);
        */
    }
}