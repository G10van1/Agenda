# Agenda

This repository contains a complete application for managing a contact and task calendar, consisting of three main projects:

- **Agenda-UI**: User interface in Vue.js.
- **WebAPI**: API RESTful in C# .NET 7.
- **TestAgenda**: Unit tests using XUnit.

## Requirements

Before you begin, make sure you have the following tools installed in your environment:

- [Node.js](https://nodejs.org/) (version 14 or higher)
- [Vue CLI](https://cli.vuejs.org/)
- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or any IDE of your choice for C# development
- [XUnit](https://xunit.net/)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

## Solution Projects

Agenda-UI: Contains the Vue.js project for the user interface.

WebAPI: Contains the RESTful API project developed in C# .NET 7.

TestAgenda: Contains the unit testing project using XUnit.

Infrastructure: Contains database access classes, migrations and validators.

## Main Features and Technologies
- Entity Framevork;
- Entity Framevork Migrations;
- SQL Server database;
- JWT authentication;
- Swagger;
- Unit and integration tests in XUnit;
- FluentValidation;
- Dependency Injection;
- Vue.js components;
- RESTFul web API.
  
## Installation

## Cloning the Repository

Clone this repository to your local machine using the command:

```
git clone https://github.com/G10van1/Agenda.git
```
## Backend Configuration (WebAPI)

Navigate to the WebAPI project directory:

```
cd ../WebAPI
```
Restore project dependencies:

```
dotnet restore
```
## Frontend Configuration (Agenda-UI)

Navigate to the Agenda-UI project directory:

```
cd Agenda-UI
```
Install project dependencies:

```
npm install
```
## SQL Server Database Configuration

Navigate to the Infrastructure project directory:

```
cd ../Infrastructure
```
Make sure the SQL Server service is active, Microsoft SQL Server Management Studio can be used.
Mount the database through Entity Framework Migrations:

```
dotnet-ef database update
```
If you prefer, you can import the database script located in the Database folder.

## Execution

## Running the API (WebAPI)

Navigate to the WebAPI project directory:

```
cd ../WebAPI
```

Start the API:

```
dotnet run
```

Another option is to run through Visual Studio.

The API will be available at http://localhost:5223 or https://localhost:7266.

You can use the Swagger interface to test endpoints via the link:
http://localhost:5223/swagger/index.html ou
https://localhost:7266/swagger/index.html

## Running the Web Application (Agenda-UI)

Navigate to the Agenda-UI project directory:

```
cd Agenda-UI
```

Start the development server:

```
npm run serve
```

Access the application in your browser at http://localhost:8080.

When clicking on any of the items, if you are not already logged in, the login screen will appear.

To log in use the following credentials:

User: admin

Password: admin

## Running the Tests (TestAgenda)

Navigate to the TestAgenda project directory:

```
cd ../TestAgenda
```
Run the tests:
```
dotnet test
```
If you prefer, you can use the Visual Studio test manager.

The test project is configured for base URL "https://localhost:7266".

If the WebAPI project is running on another URL, it will be necessary to change the base URL configuration in the test class constructor (UnitTestAgenda), as shown in the code below:

```
_httpClient.BaseAddress = new Uri("https://localhost:7266"); //Configure for the WebAPI project URL
```


