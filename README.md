
- [Sample project](#sample-project)
  - [Introduction](#introduction)
  - [Azure Resources](#azure-resources)
  - [Project's cloud solution architecture](#projects-cloud-solution-architecture)
  - [Project's application architecture](#projects-application-architecture)
  - [Project' application technologies](#project-application-technologies)

# Sample project

## Introduction
This sample project is a quick start for developing a cloud based solution.

**Scenario**: Assume we have a very simple online product stock. As soon as a product is added to the online stock a message is sent to a storage queue and a function will be triggered because of the pushed message to the queue. This function generate a thumbnail form the original image. 

![alt](drawio/Asynchronous-request-reply-pattern.png)

## Azure Resources
The following Azure resources are used in this sample project.

- Blob Storage
- Queue Storage
- Azure Function App
- Cosmos DB

## Project's cloud solution architecture
Solution Architecture is a follows:
![alt](drawio/Sample-software-and-solution-architecture-Solution.png)


## Project's application architecture
- Microservices project
- Asynchronous Request-Reply
- Database per service
- Circuit Breaker
- Domain-Drive Development (DDD)
- Database per Service Design Pattern : as the project is developed DDD, each service can have a database for itself. It's important that based on the service requirements change, the database can be changed flexible.
- Circuit Breaker Design Pattern (via Microsoft.Extensions.Http.Polly) 

## Project' application technologies
- EntiryFrameworkCore
- Azure Storage Model
- Swaschbuckle.AspNetCore -> enhance the API project with Swagger API Documenation & Swagger UI
- Microsoft.Extensions.Http.Polly -> for Circuit Breaker Design Pattern for more resiliency
- Microsoft.Azure.Cosmos.Table


