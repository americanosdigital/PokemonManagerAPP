using PokemonManagerAPP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonManagerAPP.WorkerService.Jobs
{
    public class PokemonDataJob
    {
        private readonly IPokemonRepository _repository;

        public PokemonDataJob(IPokemonRepository repository)
        {
            _repository = repository;
        }

        public async Task FetchAndUpdatePokemonDataAsync()
        {
            // Fetch data from API and update DB
        }
    }
}
