using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PokemonManagerAPP.Application.ExternalModels
{
    public class PokemonApiResponse
    {
        public IEnumerable<PokemonResult> Results { get; set; }
    }

    public class PokemonResult
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

}
