namespace BuberDinner.Contracts.Authentication;

public record LoginRquest
(
    string Email,
    string Password
);