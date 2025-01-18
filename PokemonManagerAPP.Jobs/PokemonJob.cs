using PokemonManagerAPP.Application.Interfaces;
using PokemonManagerAPP.Jobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonManagerAPP.Jobs
{
    public class PokemonJob : IPokemonJob
    {
        private readonly IPokemonService _pokemonService;

        public PokemonJob(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        public async Task SyncPokemonDataAsync()
        {
            await _pokemonService.SyncPokemonDataAsync();
        }
    }
}
