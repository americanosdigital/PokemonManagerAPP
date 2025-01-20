using System;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;
using PokemonManagerAPP.Application.Interfaces;
using PokemonManagerAPP.Application.Services;
using PokemonManagerAPP.Domain.Entities;
using PokemonManagerAPP.Domain.Interfaces;
using PokemonManagerAPP.Tests.TestHelpers;
using PokemonManagerAPP.Application.ExternalModels;



namespace PokemonManagerAPP.Tests.UnitTests.Application
{
    public class PokemonServiceTests
    {
        private readonly Mock<IPokemonRepository> _pokemonRepositoryMock;

        public PokemonServiceTests()
        {
            _pokemonRepositoryMock = new Mock<IPokemonRepository>();
        }

        [Fact]
        public async Task SyncPokemonDataAsync_ShouldSaveDataToRepository()
        {
            // Arrange
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

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);
            var httpClient = new HttpClient(mockHttpMessageHandler);

            var pokemonService = new PokemonService(_pokemonRepositoryMock.Object, httpClient);

            // Act
            await pokemonService.SyncPokemonDataAsync();

            // Assert
            _pokemonRepositoryMock.Verify(repo => repo.UpsertAsync(It.IsAny<IEnumerable<Pokemon>>()), Times.Once);
        }
    }

}
