using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;

namespace WebJobProcessor
{
    public class Functions
    {
        public static void ProcessQueueMessage([ServiceBusTrigger("dataverse", "account-export", Connection = "AzureServiceBus")] RemoteExecutionContext message, ILogger log)
        {
            log.LogInformation(message.MessageName);
        }
    }
}
