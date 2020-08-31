using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Persistency.Controllers
{
    /// <summary>
    /// Using dataabase per microservice
    /// https://docs.microsoft.com/en-us/dotnet/architecture/cloud-native/distributed-data
    /// 
    /// for production environments, running a database server in a container is not recommended, because you usually do not get high availability with that approach
    /// 
    /// 
    /// Pay attention to high available services
    /// For a production environment in Azure, it is recommended that you use Azure SQL DB or any other database technology that can provide high availability and high scalability. For example, for a NoSQL approach, you might choose CosmosDB.
    /// 
    /// Entity Framework (EF) Core is a lightweight, extensible, and cross-platform version of the popular Entity Framework data access technology. EF Core is an object-relational mapper (ORM) that enables .NET developers to work with a database using .NET objects.
    /// 
    /// 
    /// </summary>
    public interface IPersistencyService
    {
        Task SaveDataAsync();

    }
}
