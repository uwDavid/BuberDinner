using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors;

// <Commad/Request, Result Type> 

// public class ValidateRegisterCommandBehavior :
//     IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    // add additional constraints - TResponse s/b of type 
    where TResponse : IErrorOr
    // then we change all Types to TRequest or TResponse
{
    // validator property 
    private readonly IValidator<TRequest>? _validator;

    // constructor
    // we may have 0 OR 1 validator in our request => make it nullable
    // note the syntax here, we also provide default null value
    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // logic - before the handler
        // validation 
        if (_validator is null)
        {
            return await (dynamic)next();
        }
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
        return (dynamic)errors;
        // in case of runtime error => we have global error handling in ErrorController

        // logic - after the handler
    }

}