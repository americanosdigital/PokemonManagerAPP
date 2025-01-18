using Hangfire;
using PokemonManagerAPP.Application.Interfaces;

namespace PokemonManagerAPP.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IPokemonService _pokemonService;

        public Worker(ILogger<Worker> logger, IPokemonService pokemonService)
        {
            _logger = logger;
            _pokemonService = pokemonService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running.");

            // Schedule the Hangfire job to sync Pokémon data every hour
            RecurringJob.AddOrUpdate(
                "SyncPokemonData",
                () => _pokemonService.SyncPokemonDataAsync(),
                Cron.Hourly);

            await Task.CompletedTask;
        }
    }
}
