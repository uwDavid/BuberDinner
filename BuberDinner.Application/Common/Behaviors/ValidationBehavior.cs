using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors;

// <Commad/Request, Result Type> 
public class ValidateRegisterCommandBehavior :
    IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    // validator property 
    private readonly IValidator<RegisterCommand> _validator;

    // constructor
    public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator)
    {
        _validator = validator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterCommand request,
        RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
        CancellationToken cancellationToken)
    {
        // logic - before the handler
        // validation 
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            var result = await next();
        }

        // on error => we want to convert result to a list of errors 
        var errors = validationResult.Errors
            .Select(ValidationFailure => Error.Validation(
                ValidationFailure.PropertyName,
                ValidationFailure.ErrorMessage))
            .ToList();
        // .CovertAll() = the same as Select().ToList()
        return errors;

        // logic - after the handler
    }
}