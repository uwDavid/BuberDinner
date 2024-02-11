using System.Runtime.CompilerServices;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

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

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // 1. Check if user exists
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User already registered with this email");
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

    public AuthenticationResult Login(string email, string password)
    {
        // 1. Check if user exists
        // Note syntax sugar used here for user
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User does not exist.");
            // Temporary implementation, this is not a secure practice
        }
        // 2. Validate password is correct
        if (user.Password != password)
        {
            throw new Exception("Invalid password.");
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