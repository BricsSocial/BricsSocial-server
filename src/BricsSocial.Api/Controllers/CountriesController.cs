using BricsSocial.Application.Countries.Common;
using BricsSocial.Application.Countries.GetCountries;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public sealed class CountriesController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CountryDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetCountriesQuery(), cancellationToken));
        }
    }
}
