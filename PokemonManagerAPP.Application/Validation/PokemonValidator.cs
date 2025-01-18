using FluentValidation;
using PokemonManagerAPP.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonManagerAPP.Application.Validation
{
    public class PokemonValidator : AbstractValidator<PokemonDto>
    {
        public PokemonValidator()
        {

            RuleFor(p => p.Name).NotEmpty().MaximumLength(100);

        }
    }

}
