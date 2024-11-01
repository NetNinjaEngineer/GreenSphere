using GreenSphere.Application.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GreenSphere.Api.Controllers.Base;

// [ApiKey]
[ApiController]
public class BaseApiController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;

    public ActionResult CustomResult<T>(GreenSphere.Application.Abstractions.Result<T> result)
    {
        return GetObjectResult(result);
    }

    private static ActionResult GetObjectResult<T>(GreenSphere.Application.Abstractions.Result<T> result) => result.StatusCode switch
    {
        HttpStatusCode.OK => new OkObjectResult(result),
        HttpStatusCode.BadRequest => new BadRequestObjectResult(result),
        HttpStatusCode.NotFound => new NotFoundObjectResult(result),
        HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(result),
        HttpStatusCode.Conflict => new ConflictObjectResult(result),
        HttpStatusCode.NoContent => new NoContentResult(),
        HttpStatusCode.Created => new ObjectResult(result)
    };
    
    public ActionResult CustomResult<T>(GreenSphere.Application.Bases.Result<T> result)
    {
        return GetObjectResult(result);
    }

    private static ActionResult GetObjectResult<T>(GreenSphere.Application.Bases.Result<T> result) => result.StatusCode switch
    {
        HttpStatusCode.OK => new OkObjectResult(result),
        HttpStatusCode.BadRequest => new BadRequestObjectResult(result),
        HttpStatusCode.NotFound => new NotFoundObjectResult(result),
        HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(result),
        HttpStatusCode.Conflict => new ConflictObjectResult(result),
        HttpStatusCode.NoContent => new NoContentResult(),
        HttpStatusCode.Created => new ObjectResult(result)
    };
}
