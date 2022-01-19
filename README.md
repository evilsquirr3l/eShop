# eShop

[![.NET](https://github.com/evilsquirr3l/eShop/actions/workflows/dotnet.yml/badge.svg)](https://github.com/evilsquirr3l/eShop/actions/workflows/dotnet.yml) [![Coverage Status](https://coveralls.io/repos/github/evilsquirr3l/eShop/badge.svg?branch=master)](https://coveralls.io/github/evilsquirr3l/eShop?branch=master)

# Client (front-end)

- [Angular 13.1](https://angular.io/) 
- [Jasmine](https://jasmine.github.io/) for automated testing
- [Angular Material](https://material.angular.io/)

# Service (back-end)

- [ASP.NET Core 6.0](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) handling Entity-to-DTO mapping
- [FluentValidation](https://fluentvalidation.net/) for building strongly-typed validation rules
- Unit and integration tests using [Moq](https://github.com/moq/moq4) and [nUnit](https://nunit.org/) with [FluentAssertions](https://fluentassertions.com/)
- Authentication via [ASP NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0)
- [Swagger UI](https://github.com/swagger-api/swagger-ui)
- [PostgreSQL](https://www.postgresql.org/) as object-relational database system

# DevOps (back-end)

- [GitHub Actions](https://docs.github.com/en/actions/learn-github-actions)
- [Docker](https://www.docker.com/)

# How to run locally

1. [Download and install the .NET 6 SDK](https://dotnet.microsoft.com/download)
2. [Download and install the Node.Js](https://nodejs.org/en/)
3. Open a terminal such as **PowerShell**, **Command Prompt**, or **bash** and navigate to the root folder
4. Run the following `dotnet` commands:
```sh
dotnet build
dotnet run --project WebApi
```
5. Open your browser to: `https://localhost:5001/swagger`.
6. Launch configuration automatically runs `ng serve --port 44468` and prepares SPA development server.
7. When SPA development server is ready, your browser will open: `http://localhost:44468`
