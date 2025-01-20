using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonManagerAPP.Infrastructure.Data;

namespace PokemonManagerAPP.Tests.TestHelpers
{
    public static class InMemoryDbContextFactory
    {
        public static PokemonDbContext Create()
        {
            var options = new DbContextOptionsBuilder<PokemonDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            return new PokemonDbContext(options);
        }
    }
}
