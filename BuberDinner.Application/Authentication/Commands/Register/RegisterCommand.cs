using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register;

// Query / Command implements IRequest<ResultType> 
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
// shows the Return Type of the Command