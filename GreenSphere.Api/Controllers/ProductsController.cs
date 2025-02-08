using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/products")]
public class ProductsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<ProductDto>>>> GetAllProductsAsync()
        => CustomResult(await Mediator.Send(new GetAllProductsQuery()));
}
