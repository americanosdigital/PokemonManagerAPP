using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PokemonManagerAPP.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new
            {
                Message = "An unexpected error occurred.",
                Details = context.Exception.Message
            })
            {
                StatusCode = 500
            };
        }
    }
}
