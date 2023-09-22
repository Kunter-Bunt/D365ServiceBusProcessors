using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var hostBuilder = new HostBuilder();
hostBuilder.ConfigureWebJobs((context, builder) => {
    builder.AddServiceBus(options => options.MaxConcurrentCalls = 1);
});
hostBuilder.ConfigureLogging((context, b) =>
{
    b.AddConsole();
});
var host = hostBuilder.Build();
using (host)
{
    await host.RunAsync();
}