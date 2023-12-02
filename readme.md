# Buber Dinner Tutorial

Following tutorial by @Amichai

This is a dotnet demo project to showcase:

-   Clean Architecture
-   Domain Driven Design
-   CQRS
-   Use `REST Client` VS Code extension to make HTTP Requests

## Notes

### Dotnet CLI Commands

**1. Create Project Structure**

```
dotnet new sln -o BuberDinner
cd BuberDiner
dotnet new webapi -o BuberDinner.Api
dotnet new classlib -o BuberDinner.Contracts
dotnet new classlib -o BuberDinner.Infrastructure
dotnet new classlib -o BuberDinner.Application
dotnet new classlib -o BunerDinner.Domain
```

**2. Add .csproj to .sln**
To add all csproj to sln in Windows:

```
dotnet sln add (ls -r **\*.csproj)
```

To add all csproj to sln in Linux:

```
ls -r **/*.csproj | xargs dotnet sln add
```

**3. Add References between projects**
Now we have to add references between the projects:

```
dotnet add BuberDinner.Api reference BuberDinner.Contracts BuberDinner.Application
dotnet add BuberDinner.Infrastructure reference BuberDinner.Application
dotnet add BuberDinner.Application reference BuberDinner.Domain
dotnet add BuberDinner.Api reference BuberDinner.Infrastructure
```

**4. Add Packages**

```
dotnet add BuberDinner.Application package Microsoft.Extensions.DependencyInjection.Abstractions
dotnet add BuberDinner.Infrastructure package System.IdentityModel.Tokens.Jwt
dotnet add BuberDinner.Infrastructure package Microsoft.Extensions.Configuration
dotnet add BuberDinner.Infrastructure package Microsoft.Extensions.Options.ConfigurationExtensions
```
