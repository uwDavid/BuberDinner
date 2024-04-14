using System.Net;

namespace BuberDinner.Application.Common.Errors;

public interface IErrorForOneOf
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}