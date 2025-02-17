using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Category;
using GreenSphere.Application.Features.Categories.Commands.CreateCategory;
using GreenSphere.Application.Features.Categories.Commands.DeleteCategory;
using GreenSphere.Application.Features.Categories.Commands.UpdateCategory;
using GreenSphere.Application.Features.Categories.Queries.GetAllCategories;
using GreenSphere.Application.Features.Categories.Queries.GetCategoryWithProducts;
using GreenSphere.Application.Helpers;
using GreenSphere.Domain.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/categories")]
public class CategoriesController(IMediator mediator) : BaseApiController(mediator)
{
    [Guard(roles: [Constants.Roles.Admin])]
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> CreateCategoryAsync([FromBody] CreateCategoryCommand command)
        => CustomResult(await Mediator.Send(command));


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Result<CategoryWithProductsDto>>> GetCategoryAsync([FromRoute] Guid id)
    => CustomResult(await Mediator.Send(new GetCategoryWithProductsQuery { CategoryId = id }));

    [HttpGet]
    public async Task<ActionResult<Result<IReadOnlyList<CategoryDto>>>> GetAllAsync([FromQuery] CategorySpecParams? categorySpecParams)
        => CustomResult(await Mediator.Send(new GetAllCategoriesQuery() { CategorySpecParams = categorySpecParams }));

    [Guard(roles: [Constants.Roles.Admin])]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Result<Guid>>> UpdateCategoryAsync(
        [FromRoute] Guid id,
        [FromBody] CategoryForUpdateDto updateDto)
    {
        var command = new UpdateCategoryCommand { CategoryId = id, Name = updateDto.Name, Description = updateDto.Description };
        return CustomResult(await Mediator.Send(command));
    }

    [Guard(roles: [Constants.Roles.Admin])]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Result<bool>>> DeleteCategoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new DeleteCategoryCommand { CategoryId = id }));

}
