---
page_type: sample
languages:
- csharp
products:
- azure
description: "Azure Cosmos DB is Microsoft's globally distributed multi-model database service"
urlFragment: azure-cosmos-db-cassandra-dotnet-getting-started
---

# Developing a Dotnet app with Cassandra API using Azure Cosmos DB
Azure Cosmos DB is Microsoft's globally distributed multi-model database service. You can quickly create and query document, table, key-value, and graph databases, all of which benefit from the global distribution and horizontal scale capabilities at the core of Azure Cosmos DB. 
This quick start demonstrates how to create an Azure Cosmos DB account for the Cassandra API by using the Azure portal. You'll then build a user profile console app, output as shown in the following image, with sample data.

## Running this sample
* Before you can run this sample, you must have the following perquisites:
	* An active Azure Cassandra API account - If you don't have an account, refer to the [Create Cassandra API account](https://docs.microsoft.com/en-us/azure/cosmos-db/create-cassandra-dotnet). 
	* [Microsoft Visual Studio](https://www.visualstudio.com).
	* [Git](http://git-scm.com/).

1. Clone this repository using `git clone https://github.com/Azure-Samples/azure-cosmos-db-cassandra-dotnet-getting-started.git`

2. Open the CassandraQuickStartSample.sln solution and install the Cassandra .NET driver. Use the .NET Driver's NuGet package. From the Package Manager Console window in Visual Studio:

```bash
PM> Install-Package CassandraCSharpDriver
```

3. Next, configure the endpoints in **Program.cs**

```
private const string UserName = "<FILLME>"; 
private const string Password = "<FILLME>";
private const string CassandraContactPoint = "<FILLME>"; //  DnsName
```
4. Compile and Run the project.

5. Output Image: 

![User Data](/img.PNG?raw=true "user data")

## About the code
The code included in this sample is intended to get you quickly started with a C# application using Cassandra C# driver that connects to Azure Cosmos DB with the Cassandra API.

## More information

- [Azure Cosmos DB](https://docs.microsoft.com/azure/cosmos-db/introduction)
- [Cassandra DB](http://cassandra.apache.org/)
