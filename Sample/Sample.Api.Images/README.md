
# Azure Blob Storage
Azure Blob Storage are general-purpose, durable, scalabile, and high-performing storage.


Blob storage events are available in:
- general-purpose v2 storage accounts
- Blob storage accounts only.

Whenever a file is added or deleted from a Blob storage container, event grid uses event subscriptions to route event messages to subscribers.

This project use Azure Functions as subscriber of the event coming from Blob via Event Grid.



Cosmosdb : https://docs.microsoft.com/en-us/azure/cosmos-db/sql-api-get-started