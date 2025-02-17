using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Features.Products.Commands.CreateProduct;
using GreenSphere.Application.Features.Products.Commands.DeleteProduct;
using GreenSphere.Application.Features.Products.Commands.UpdateProduct;
using GreenSphere.Application.Features.Products.Queries.GetAllProducts;
using GreenSphere.Application.Features.Products.Queries.GetProduct;
using GreenSphere.Application.Helpers;
using GreenSphere.Domain.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/products")]
public class ProductsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IReadOnlyList<ProductDto>>>> GetAllProductsAsync([FromQuery] ProductSpecParams @params)
        => CustomResult(await Mediator.Send(new GetAllProductsQuery() { ProductSpecParams = @params }));


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<ProductDto>>> GetProductAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetProductQuery { Id = id }));

    [Guard(roles: [Constants.Roles.Admin])]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<Result<Guid>>> CreateProductAsync([FromForm] CreateProductCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard(roles: [Constants.Roles.Admin])]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<Result<bool>>> UpdateProductAsync([FromRoute] Guid id, [FromForm] ProductForUpdateDto request)
        => CustomResult(await Mediator.Send(
            new UpdateProductCommand
            {
                ProductId = id,
                CategoryId = request.CategoryId,
                Description = request.Description,
                DiscountPercentage = request.DiscountPercentage,
                Image = request.Image,
                Name = request.Name,
                Price = request.Price
            }));


    [Guard(roles: [Constants.Roles.Admin])]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Guid>>> DeleteProductAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new DeleteProductCommand { ProductId = id }));
}
