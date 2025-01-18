using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonManagerAPP.Domain.Interfaces;
using PokemonManagerAPP.Infrastructure.Data;
using PokemonManagerAPP.Infrastructure.Repositories;

namespace PokemonManagerAPP.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configura o DbContext
            services.AddDbContext<PokemonDbContext>(options =>
                options.UseInMemoryDatabase("PokemonManagerDB")); // Ou UseSqlServer para bancos reais

            // Adiciona os repositórios da camada de infraestrutura
            services.AddScoped<IPokemonRepository, PokemonRepository>();

            return services;
        }
    }
}
