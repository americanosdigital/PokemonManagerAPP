using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using PokemonManagerAPP.Application.Interfaces;
using PokemonManagerAPP.Tests.TestHelpers;
using System.Linq;
using System.Net.Http;

namespace PokemonManagerAPP.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Configurar Hangfire com armazenamento em memória
                services.AddHangfire(config => config.UseMemoryStorage());
                services.AddHangfireServer();

                // Remover serviços duplicados ou existentes antes de adicionar mocks
                RemoveServiceIfExists<IHttpClientFactory>(services);
                RemoveServiceIfExists<IHostedService>(services);
                RemoveServiceIfExists<IPokemonService>(services);

                // Mock de HttpClient
                var mockHandler = new MockHttpMessageHandler(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("{\"results\": [{\"name\": \"Pikachu\", \"url\": \"https://pokeapi.co/api/v2/pokemon/1/\"}]}")
                });
                var httpClient = new HttpClient(mockHandler);

                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                                     .Returns(httpClient);

                services.AddSingleton(httpClientFactoryMock.Object);

                // Mock do serviço de Pokémon
                var pokemonServiceMock = new Mock<IPokemonService>();
                services.AddScoped(_ => pokemonServiceMock.Object);
            });
        }

        /// <summary>
        /// Remove um serviço do contêiner de dependência, se ele já estiver registrado.
        /// </summary>
        private static void RemoveServiceIfExists<TService>(IServiceCollection services)
        {
            var descriptors = services.Where(d => d.ServiceType == typeof(TService)).ToList();
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }
        }
    }
}
