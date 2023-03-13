using BricsSocial.Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers;

[ApiController]
[ApiExceptionFilter]
[Produces("application/json")]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
