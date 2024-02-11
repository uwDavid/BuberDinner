# BuberDinner Project

A follow along project to document .NET workflows.

## Starting Projects

```
dotnet new sln -o BuberDinner
cd BuberDinner
dotnet new webapi -o BuberDinner.Api
dotnet new classlib -o BuberDinner.Contracts
dotnet new classlib -o BuberDinner.Infrastructure
dotnet new classlib -o BuberDinner.Application
dotnet new classlib -o BuberDinner.Domain
```

In order for us to build the solution, we need to add the projects to the solution.<br>
On Windows:

```
dotnet sln add (ls -r **\*.csproj)
```

On Linux:

```
find ./ -name *.csproj | xargs dotnet sln add
ls -r **/*.csproj | xargs dotnet sln add
```

Now `dotnet bulid` will work properly.

*Linking Projects*<br>
`.Api` needs to reference `.Contracts` and `.Application`

```
dotnet add ./BuberDinner.Api/ reference ./BuberDinner.Contracts/ ./BuberDinner.Application/
dotnet add ./BuberDinner.Infrastructure/ reference BuberDinner.Application
dotnet add ./BuberDinner.Application/ reference BuberDinner.Domain
dotnet add ./BuberDinner.Api/ reference BuberDinner.Infrastructure
```

After all the linking is done, we can check the references we added in `.sln` and `.csproj` files.

## Installing Additional Packages

```
dotnet add BuberDinner.Application/ package Microsoft.Extensions.DependencyInjection.Abstractions
dotnet add BuberDinner.Infrastructure/ package Microsoft.Extensions.DependencyInjection.Abstractions
```

## 1 - Project Setup

Purpose of `Clean Architecture` is to encapsulate domain of concerns within respective layers.
In the linking section above, we can see how each layer of the project will interact with each other.

A noteworthy technique to point out in this section is that we made both the `Application` and `Infrastrucure` layer to manage its own dependency injections.
Normally, we would inject the `Authentication` service implementation in the `Api` layer.
However, we utilized the `DenpendencyInjection.Abstractions` package to migrate dependency injection to `Application` and `Infrastructure` layer.
See code changes and comments on the `DependencyInjection.cs` files and also in the `Program.cs` in `BuberDinner.Api`.

## 2 - JWT Token Generation

`Application Layer` will define the interface, while `Infrastructure Layer` will define the implementation.
The wiring of implementation is kept within the `Infrastructure Layer`.

*Additional Packages*

```
dotnet add BuberDinner.Infrastructure/ package System.IdentityModel.Tokens.Jwt
dotnet add BuberDinner.Infrastructure/ package Microsoft.Extensions.Configuration
dotnet add BuberDinner.Infrastructure/ package Microsoft.Extensions.Options.ConfigurationExtensions
```

*Noteable Techniques*
We chained `Infrastructure` dependency injection in `Api` layer. 
We used the `Configuration Manager` in `Infrastructure` layer to obtain JwtSettings from the `Api` layer. 
Find the JwtSetting in `appsettings.json` files in `Api` layer.

## 3 - Repository Pattern
Now we implement the repository pattern for our Authentication Service. 

**Step 1: Create User Model**
We create the `User` model in the `Domain` project.
VS Code shortcut to write out properties in Entity Domain.
Type `prop` and hit tab to generate:
```cs
public TYPE Type {get; set;}
```

```cs
public Guid Id { get; set; } = Guid.NewGuid();
// If we don't specify a Guid, then generate a new Guid
public string FirstName { get; set; } = null!;
// Ctrl + . Use null-forgiving operator
// Tells compiler that this value can be null, to suppress error
```

**Step 2: Create Respository Interface**
We create the interface `IUserRepository` at the `Application` layer, and then inject this interface into the `AuthenticationService.cs`.
Then we need to code the `AuthenticationService.cs` on how the `_userRepository` is to be used. 

**Step 3: Implement Repository**
We then implement the `UserRepository` at the `Infrastructure` layer. 

**Step 4: Dependency Injection**
Inject the dependency at the `Infrastructure` layer, in `DependencyInjection.cs`.
Note: this is done in the previous sections on how we made this possible via the `DependencyInjection.Abstractions` package. 

**Note**
If we add the service as `AddScoped` it will create a new list for every request.
Thus, we have to make the List of `Users` static. 

**Refactoring**
We change the `AuthenticationResult` to use `User` entity model, instead of the individual string fields. 
Then we make the appropriate refactoring in `AuthenticationService.cs` in `Application` and `AuthenticationController.cs` in `Api`. 

We also want the `JWT Token Generator` to use `User` entity model as well. 
We need to refactor:
- `IJwtGenerator.cs` in `Application` 
- `JwtGenerator.cs` in `Infrastructure`
- `AuthenticationService` in `Application`
