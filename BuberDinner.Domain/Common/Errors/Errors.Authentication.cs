using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

// partial class => splits up definitions in multiple files
public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials = Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "Invalid credentials.");
    }
}