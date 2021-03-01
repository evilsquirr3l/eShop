# eShop

[![Build Status](https://travis-ci.com/evilsquirr3l/eShop.svg?branch=master)](https://travis-ci.com/evilsquirr3l/eShop) ![.NET Core](https://github.com/evilsquirr3l/eShop/workflows/.NET%20Core/badge.svg?branch=master) [![React Build](https://github.com/evilsquirr3l/eShop/actions/workflows/react-build.yml/badge.svg)](https://github.com/evilsquirr3l/eShop/actions/workflows/react-build.yml)

Web application is currently under development by Team7.

# Client (front-end)

- [React](https://reactjs.org/docs/getting-started.html) with [Redux](https://redux.js.org/introduction/getting-started) and [TypeScript](https://www.typescriptlang.org/docs)
- [Jest](https://jestjs.io/docs/ru/tutorial-react) for automated testing
- [Material-UI](https://material-ui.com/)

# Service (back-end)

- [ASP.NET Core 5.0](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-5.0)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) handling Entity-to-DTO mapping
- [FluentValidation](https://fluentvalidation.net/) for building strongly-typed validation rules
- Unit and integration tests using [Moq](https://github.com/moq/moq4) and [nUnit](https://nunit.org/) with [FluentAssertions](https://fluentassertions.com/)
- Authentication via [ASP NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio) 
- [Swagger UI](https://github.com/swagger-api/swagger-ui)
- [PostgreSQL](https://www.postgresql.org/) as object-relational database system

# DevOps (back-end)

- [GitHub Actions](https://docs.github.com/en/actions/learn-github-actions)
- [Travis CI](https://travis-ci.org/)
- [Docker](https://www.docker.com/)

# How to run locally

1. [Download and install the .NET 5 SDK](https://dotnet.microsoft.com/download)
2. [Download and install the Node.Js](https://nodejs.org/en/)
3. Open a terminal such as **PowerShell**, **Command Prompt**, or **bash** and navigate to the root folder
4. Run the following `dotnet` commands:
```sh
dotnet build
dotnet run --project eShop.API
```
5. Open your browser to: `https://localhost:5001/swagger`.
6. In another terminal, navigate to the `eShop.API/clientapp` folder and run the following `npm` commands:
```sh
npm install
npm start
```
7. The webpack dev server hosts the front-end and your browser will open to: `http://localhost:3000`
