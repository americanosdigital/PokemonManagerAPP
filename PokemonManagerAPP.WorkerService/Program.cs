using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokemonManagerAPP.Application;
using PokemonManagerAPP.Infrastructure;

namespace PokemonManagerAPP.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplicationServices(); // Add Application Layer
                    services.AddInfrastructureServices(hostContext.Configuration); // Add Infrastructure Layer

                    // Add Hangfire services
                    services.AddHangfire(config => config.UseMemoryStorage());
                    services.AddHangfireServer();

                    // Add the Worker
                    services.AddHostedService<Worker>();
                });
    }
}
