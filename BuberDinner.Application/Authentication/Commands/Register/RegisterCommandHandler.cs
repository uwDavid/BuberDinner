using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register;

// IRequestHandler<Request Type, Result Type> 
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // 1. Check if user exists
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            // throw new DuplicateEmailException();
            // throw new Exception("Email already registered");
            // return Result.Fail<AuthenticationResult>(new DuplicateEmailError());
            // return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
            return Domain.Common.Errors.Errors.User.DuplicateEmail;
        }
        // 2. Create user (generate guid)
        var user = new Domain.Entities.User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        // persist to db
        _userRepository.Add(user);

        // 3. Create JWT 
        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}