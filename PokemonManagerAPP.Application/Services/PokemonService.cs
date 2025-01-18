using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using PokemonManagerAPP.Application.DTOs;
using PokemonManagerAPP.Application.Interfaces;
using PokemonManagerAPP.Domain.Entities;
using PokemonManagerAPP.Domain.Interfaces;
using PokemonManagerAPP.Application.ExternalModels;


namespace PokemonManagerAPP.Application.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly HttpClient _httpClient;

        public PokemonService(IPokemonRepository pokemonRepository, HttpClient httpClient)
        {
            _pokemonRepository = pokemonRepository;
            _httpClient = httpClient;
        }

        public async Task SyncPokemonDataAsync()
        {
            var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=100");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<PokemonApiResponse>(jsonString);

            var pokemons = apiResponse.Results.Select(p => new Pokemon
            {
                Name = p.Name,
                Url = p.Url
            });

            await _pokemonRepository.UpsertAsync(pokemons);
        }

        public async Task<IEnumerable<PokemonDto>> GetAllPokemonAsync()
        {
            var pokemons = await _pokemonRepository.GetAllAsync();
            return pokemons.Select(p => new PokemonDto
            {
                Name = p.Name,
                Url = p.Url
            });
        }
    }

}
