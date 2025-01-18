using Microsoft.EntityFrameworkCore;
using PokemonManagerAPP.Domain.Entities;
using PokemonManagerAPP.Domain.Interfaces;
using PokemonManagerAPP.Infrastructure.Data;
using PokemonManagerAPP.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonManagerAPP.Infrastructure.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pokemon>> GetAllAsync()
        {
            return await _context.Pokemons.ToListAsync();
        }

        public async Task UpsertAsync(IEnumerable<Pokemon> pokemons)
        {
            foreach (var pokemon in pokemons)
            {
                var existingPokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Name == pokemon.Name);
                if (existingPokemon == null)
                {
                    _context.Pokemons.Add(pokemon);
                }
                else
                {
                    existingPokemon.Url = pokemon.Url;
                }
            }

            await _context.SaveChangesAsync();
        }

    }

}
