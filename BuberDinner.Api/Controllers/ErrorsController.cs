using System.Security.Cryptography;
using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = ex switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "Uexpected error occurred."),
        };

        return Problem(statusCode: statusCode, title: message);
    }
}