using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Check if user exists
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            // throw new DuplicateEmailException();
            // throw new Exception("Email already registered");
            // return Result.Fail<AuthenticationResult>(new DuplicateEmailError());
            // return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
            return Errors.User.DuplicateEmail;
        }
        // 2. Create user (generate guid)
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        // persist to db
        _userRepository.Add(user);

        // 3. Create JWT 
        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
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