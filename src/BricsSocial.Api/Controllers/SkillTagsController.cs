using BricsSocial.Application.SkillTags.Common;
using BricsSocial.Application.SkillTags.GetSkillTags;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public sealed class SkillTagsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SkillTagDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetSkillTagsQuery(), cancellationToken));
        }
    }
}
