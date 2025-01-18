using Microsoft.AspNetCore.Mvc;
using PokemonManagerAPP.Application.DTOs;
using PokemonManagerAPP.Application.Interfaces;

namespace PokemonManagerAPP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetPokemons()
        {
            var pokemons = await _pokemonService.GetAllPokemonAsync();
            return Ok(pokemons);
        }
    }
}
