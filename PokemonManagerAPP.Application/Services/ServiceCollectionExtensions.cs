using Microsoft.Extensions.DependencyInjection;
using PokemonManagerAPP.Application.Interfaces;
using PokemonManagerAPP.Application.Services;

namespace PokemonManagerAPP.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Adiciona os serviços da camada de aplicação
            services.AddScoped<IPokemonService, PokemonService>();
            return services;
        }
    }
}
