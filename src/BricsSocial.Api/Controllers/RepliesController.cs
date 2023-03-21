using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Agents.GetAgents;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Replies.Common;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public sealed class RepliesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ReplyDto>>> Get(CancellationToken cancellationToken)
        {
            return new List<ReplyDto>();
        }
    }
}
