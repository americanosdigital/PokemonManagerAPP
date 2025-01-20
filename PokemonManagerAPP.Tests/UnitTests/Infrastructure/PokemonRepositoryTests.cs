using System;
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
using PokemonManagerAPP.Infrastructure.Repositories;
using PokemonManagerAPP.Infrastructure.Data;
using PokemonManagerAPP.Tests.TestHelpers;

namespace PokemonManagerAPP.Tests.UnitTests.Infrastructure
{
    public class PokemonRepositoryTests
    {
        private readonly PokemonDbContext _context;
        private readonly PokemonRepository _repository;

        public PokemonRepositoryTests()
        {
            _context = InMemoryDbContextFactory.Create();
            _repository = new PokemonRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPokemons()
        {
            // Arrange
            _context.Pokemons.AddRange(SampleData.GetPokemonList());
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(3, result.Count());
        }
    }
}
