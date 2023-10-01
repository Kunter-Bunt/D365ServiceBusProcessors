using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using System.Runtime.Serialization.Json;

namespace DataverseEventProcessorPlain
{
    public class Functions
    {
        private ILogger log;

        public Functions()
        {
            log = LoggerFactory.Create(_ => _.AddConsole()).CreateLogger("User");
        }

        public Task ProcessQueueMessage(ProcessMessageEventArgs args)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RemoteExecutionContext));
            var message = serializer.ReadObject(args.Message.Body.ToStream()) as RemoteExecutionContext;

            log.LogInformation(message?.MessageName);

            return Task.CompletedTask;
        }

        public Task ProcessError(ProcessErrorEventArgs args)
        {
            log.LogError(args.Exception.Message);

            return Task.CompletedTask;
        }
    }
}