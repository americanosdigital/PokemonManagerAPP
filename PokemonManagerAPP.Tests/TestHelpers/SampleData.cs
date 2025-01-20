using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonManagerAPP.Application.ExternalModels;
using PokemonManagerAPP.Domain.Entities;
using PokemonManagerAPP.Infrastructure.Data;

namespace PokemonManagerAPP.Tests.TestHelpers
{

    public static class SampleData
    {
        public static List<Pokemon> GetPokemonList() =>
            new()
            {
                new Pokemon { Name = "Bulbasaur", Url = "https://example.com/bulbasaur" },
                new Pokemon { Name = "Charmander", Url = "https://example.com/charmander" },
                new Pokemon { Name = "Squirtle", Url = "https://example.com/squirtle" }
            };

        public static PokemonApiResponse GetPokemonApiResponse() =>
            new()
            {
                Results = new List<PokemonResult>
                {
                    new PokemonResult { Name = "Bulbasaur", Url = "https://example.com/bulbasaur" },
                    new PokemonResult { Name = "Charmander", Url = "https://example.com/charmander" },
                    new PokemonResult { Name = "Squirtle", Url = "https://example.com/squirtle" }
                }
            };
    }

}
