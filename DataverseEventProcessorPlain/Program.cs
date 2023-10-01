using Azure.Identity;
using Azure.Messaging.ServiceBus;
using DataverseEventProcessorPlain;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Starting");

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var sbNamespace = config.GetSection("AzureServiceBus")["fullyQualifiedNamespace"];
//var sbConnectionString = config.GetSection("AzureServiceBus")["connectionString"];

var client = new ServiceBusClient(sbNamespace,new DefaultAzureCredential(), new ServiceBusClientOptions());
//var client = new ServiceBusClient(sbConnectionString);

var functions = new Functions();

var processor = client.CreateProcessor("dataverse", "account-export", new ServiceBusProcessorOptions());
processor.ProcessMessageAsync += functions.ProcessQueueMessage;
processor.ProcessErrorAsync += functions.ProcessError;

await processor.StartProcessingAsync();
Console.WriteLine("Processing");

Console.ReadKey();