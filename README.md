# Alza REST uService

The DDD oriented modular monolith includes implementation of the publish-subscribe pattern.
The backend uses InMemory Database so no configuration needed.

============

## Used libraries
CSharpFunctionalExtensions v2.42.5
https://github.com/vkhorikov/CSharpFunctionalExtensions

Swashbuckle.AspNetCore v6.7.1
https://github.com/domaindrivendev/Swashbuckle.AspNetCore

============

## Test the DDD layers

Run the tests using the command
```
dotnet test Alza.UService.Tests\Alza.UService.Tests.csproj --configuration Release
```

## Test the REST services

1. Run the backend services
```
dotnet run --project Alza.UService.Backend\Alza.UService.Backend.csproj --config
uration Release --launch-profile https
```

2. Open the service Swagger page to see available API
https://localhost:7038/swagger/index.html

The backend project contains samples file **Alza.UService.Backend.http**