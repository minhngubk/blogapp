# BlogApp Excercise

BlogApp is a robust blog application developed with C#, ASP.NET Core MVC (.NET 8), MSSQL, and other technologies. 

## Features
The system offers users the following functions:

- User login (as Editor or Admin)
- Blog posts management
   + Editors can create/update/list own blog posts
   + Admins can create/update their own blog posts, They can view all blogs and Approve/Reject blog posts for other users
- User management capabilities for Admins

## Architecture
The system follows Onion Architecture / Clean Architecture principles, it solves problem of separation of concern and tightly coupled components from N-layered

Below is a detailed description for the main layers
1. Domain Layer:
	  This layer does not depend on any other layer. This layer contains entities, enums, specifications etc.
	  Add repository and unit of work contracts in this layer.

2. Application Layer:
	  This layer contains business logic, services, service interfaces, request and response models.
	  Third party service interfaces are also defined in this layer.
	  This layer depends on domain layer.

3. Infrastructure Layer:
	  This layer contains database related logic (Repositories and DbContext), and third party library implementation (like logger and email service).
	  This implementation is based on domain and application layer.

4. Presentation Layer:
	 This layer contains Webapi or UI.

## Prerequisites

1. .NET 8 SDK
2. Visual Studio 2022 (v17.10) (This SDK is only compatible with Visual Studio 2022 (v17.10))
3. SQL Server


## Getting Started

Follow these steps to kickstart system:
1. Open solution
2. Open Package Manager Console
3. Restore the NuGet packages 
	dotnet restore.
4. Set up a database and configure the connection string in the `appsettings.json` file.
5. Build and run the project:
	dotnet build
	dotnet run
6. Init database & seed data:
	cd src
	dotnet ef database update --context BlogDbContext --project VinodNair.Blog.Infrastructure --startup-project VinodNair.Blog.Web 
	
7. Launch the application using Visual Studio.
8. See the results

## Technologies

- C#
- HTML
- CSS
- JavaScript
- ASP.NET Core MVC
- MSSQL
- Entity Framework Core
- Bootstrap
