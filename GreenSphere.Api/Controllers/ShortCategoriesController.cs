using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Attributes;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Features.Shorts.Commands.CreateShortCategory;
using GreenSphere.Application.Features.Shorts.Commands.DeleteShortCategory;
using GreenSphere.Application.Features.Shorts.Commands.UpdateShortCategory;
using GreenSphere.Application.Features.Shorts.Queries.GetAllShortCategories;
using GreenSphere.Application.Features.Shorts.Queries.GetShortCategory;
using GreenSphere.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{apiVersion:apiVersion}/short-categories")]
public class ShortCategoriesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [ProducesResponseType<Result<IReadOnlyList<ShortCategoryDto>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
        => CustomResult(await Mediator.Send(new GetAllShortCategoriesQuery()));

    [HttpGet("{id:guid}")]
    [ProducesResponseType<Result<ShortCategoryDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<ShortCategoryDto>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetShortCategoryQuery { Id = id }));

    [Guard(roles: [Constants.Roles.Admin])]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new DeleteShortCategoryCommand { CategoryId = id }));

    [Guard(roles: [Constants.Roles.Admin])]
    [HttpPost]
    [ProducesResponseType<Result<ShortCategoryDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<ShortCategoryDto>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result<ShortCategoryDto>>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateShortCategoryCommand command)
    => CustomResult(await Mediator.Send(command));


    [Guard(roles: [Constants.Roles.Admin])]
    [HttpPut("{id:guid}")]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Guid id, [FromBody] ShortCategoryUpdateDto dto)
        => CustomResult(await Mediator.Send(new UpdateShortCategoryCommand { Id = id, NameAr = dto.NameAr, NameEn = dto.NameEn, Description = dto.Description }));
}