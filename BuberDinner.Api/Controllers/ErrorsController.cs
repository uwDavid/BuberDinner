using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(title: ex?.Message, statusCode: 400);
    }
}