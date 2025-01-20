using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PokemonManagerAPP.Application.ExternalModels;
using PokemonManagerAPP.Application.Interfaces;
using PokemonManagerAPP.Domain.Interfaces;
using PokemonManagerAPP.WorkerService;
using Microsoft.Extensions.Hosting;
using PokemonManagerAPP.Tests.TestHelpers;


namespace PokemonManagerAPP.Tests.IntegrationTests
{
    public class WorkerServiceTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public WorkerServiceTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Worker_ShouldProcessPokemonDataCorrectly()
        {
            // Arrange
            var services = new ServiceCollection();

            // Mock do repositório IPokemonRepository
            var pokemonRepositoryMock = new Mock<IPokemonRepository>();
            services.AddSingleton(pokemonRepositoryMock.Object);

            // Mock dos resultados da API
            var mockPokemonResults = new List<PokemonResult>
            {
                new PokemonResult { Name = "Pikachu", Url = "https://pokeapi.co/api/v2/pokemon/1/" },
                new PokemonResult { Name = "Charmander", Url = "https://pokeapi.co/api/v2/pokemon/2/" }
            };

            var mockApiResponse = new PokemonApiResponse
            {
                Results = mockPokemonResults
            };

            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(mockApiResponse))
            };

            // Mock do HttpClient com MockHttpMessageHandler
            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);
            var httpClient = new HttpClient(mockHttpMessageHandler);
            services.AddSingleton(httpClient);

            // Configura Hangfire para usar armazenamento em memória
            services.AddHangfire(config => config.UseMemoryStorage());
            services.AddHangfireServer();

            // Mock do IHttpClientFactory para retornar o HttpClient mockado
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);
            services.AddSingleton(httpClientFactoryMock.Object);

            // Registra o serviço de logging
            services.AddLogging();

            // Registra o serviço IPokemonService
            services.AddScoped<IPokemonService, PokemonManagerAPP.Application.Services.PokemonService>();

            // Registra o Worker como IHostedService
            services.AddSingleton<IHostedService, PokemonManagerAPP.WorkerService.Worker>();

            var serviceProvider = services.BuildServiceProvider();

            var worker = serviceProvider.GetRequiredService<IHostedService>();

            // Act
            await worker.StartAsync(default);

            // Assert
            pokemonRepositoryMock.Verify(
                repo => repo.UpsertAsync(It.IsAny<IEnumerable<PokemonManagerAPP.Domain.Entities.Pokemon>>()),
                Times.Once
            );
        }
    }

}
