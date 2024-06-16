using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddTransient<IOrganizationService>(_ => {
            IConfiguration configuration = _.GetService<IConfiguration>();
            var dataverseUrl = configuration["DataverseUrl"];
            return new ServiceClient(
                new Uri(dataverseUrl),
                async _ =>
                {
#if DEBUG
                    var cred = new VisualStudioCredential(); // DefaultAzureCredential is painfully slow in debugging.
#else
                    var cred = new DefaultAzureCredential();
#endif
                    var token = await cred.GetTokenAsync(new TokenRequestContext([$"{dataverseUrl}/.default"]), CancellationToken.None);
                    return token.Token;
                });
            });
        })
    .Build();

host.Run();
