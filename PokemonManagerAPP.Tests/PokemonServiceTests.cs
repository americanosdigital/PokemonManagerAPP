using Moq;
using Xunit;
using FluentAssertions;
using PokemonManagerAPP.Application.Interfaces;
using PokemonManagerAPP.Application.Services;
using PokemonManagerAPP.Domain.Entities;
using PokemonManagerAPP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonManagerAPP.Tests
{
    public class PokemonServiceTests
    {
        private readonly Mock<IPokemonRepository> _pokemonRepositoryMock;
        private readonly PokemonService _pokemonService;

        public PokemonServiceTests()
        {
            _pokemonRepositoryMock = new Mock<IPokemonRepository>();
            _pokemonService = new PokemonService(_pokemonRepositoryMock.Object, new HttpClient());
        }

        [Fact]
        public async Task GetAllPokemonAsync_ReturnsPokemonList()
        {
            // Arrange
            _pokemonRepositoryMock.Setup(repo => repo.GetAllAsync())
                                   .ReturnsAsync(new List<Pokemon>());

            // Act
            var result = await _pokemonService.GetAllPokemonAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}
