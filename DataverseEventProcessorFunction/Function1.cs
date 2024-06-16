using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;

namespace DataverseEventProcessorFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function1))]
        public async Task Run(
            [ServiceBusTrigger("dataverse", "account-export", Connection = "AzureServiceBus")]
            RemoteExecutionContext message)
        {
            _logger.LogInformation(message.MessageName);
        }
    }
}
