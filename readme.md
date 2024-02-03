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

In order for us to build the solution, we need to add the projects to the solution.

```
dotnet sln add (ls -r **\*.csproj)
find ./ -name *.csproj | xargs dotnet sln add
```

Now `dotnet bulid` will work properly.

_Linking Projects_
`.Api` needs to reference `.Contracts` and `.Application`

```
dotnet add ./BuberDinner.Api/ reference ./BuberDinner.Contracts/ ./BuberDinner.Application/
dotnet add ./BuberDinner.Infrastructure/ reference BuberDinner.Application
dotnet add ./BuberDinner.Application/ reference BuberDinner.Domain
dotnet add ./BuberDinner.Api/ reference BuberDinner.Infrastructure
```

After all the linking is done, we can check the references we added in `.sln` and `.csproj`

## Installing Additional Packages

```
dotnet add BuberDinner.Application/ package Microsoft.Extensions.DependencyInjection.Abstractions
dotnet add BuberDinner.Infrastructure/ package Microsoft.Extensions.DependencyInjection.Abstractions
```
