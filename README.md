# Sample project

## Introduction
This is a sample project

### Azure Resources
The following Azure resources are used in this project

- Bolb Storage
- Queue Storage
- Azure Function App
- Cosmos DB

### Project's cloud solution architecture
![](./drawio/.png)

### Project's application architecture
- Microservices project
- Domain-Drive Development (DDD)
- Datbase per Service (Microservice Design Pattern) : as the project is developed DDD, each service can have a database for itself. It's important that based on the service requiremenst the database can be changed flexible.

### Project' application technologies
- EntiryFrameworkCore
- Azure Storage Model
- Swaschbuckle.AspNetCore -> enhance the API project with Swagger API Documenation & Swagger UI


## Create new project via commandline
dotnet new webapi --name Sample.Api.Products

