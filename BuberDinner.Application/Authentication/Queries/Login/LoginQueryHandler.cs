using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask; // get rid of Async warnning 

        // 1. Check if user exists
        // Note syntax sugar used here for user
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
        }
        // 2. Validate password is correct
        if (user.Password != query.Password)
        {
            return new[] { Domain.Common.Errors.Errors.Authentication.InvalidCredentials };
        }
        // 3. Create JWT token
        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}