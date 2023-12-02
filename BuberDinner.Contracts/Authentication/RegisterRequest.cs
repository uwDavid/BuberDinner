namespace BuberDinner.Contracts.Authentication;

public record RegisterRquest
(
    string FirstName,
    string LastName,
    string Email,
    string Password
);