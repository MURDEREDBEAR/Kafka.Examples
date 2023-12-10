using Kafka.Demo.Consumer.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kafka.Demo.Consumer
{
    public static class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables(prefix: "PREFIX_");
                    config.AddCommandLine(args);
                })
                .ConfigureServices((host, services) =>
                {
                    services.AddOptions()
                        .AddOptions<KafkaOptions>()
                        .BindConfiguration(nameof(KafkaOptions))
                        .Services
                        .AddHostedService<ConsumerService>();
                });
    }
}
