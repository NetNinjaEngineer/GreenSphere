using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Features.ShortCategories.Commands.CreateShortCategory;
using GreenSphere.Application.Features.Shorts.Commands.CreateShort;
using GreenSphere.Application.Features.Shorts.Commands.UpdateShort;
using GreenSphere.Application.Features.Shorts.Commands.UpdateShortCategory;

namespace GreenSphere.Application.Interfaces.Services;

public interface IShortsService
{
    Task<Result<IReadOnlyList<ShortCategoryDto>>> GetAllCategoriesAsync();
    Task<Result<ShortCategoryDto>> GetCategoryByIdAsync(Guid id);
    Task<Result<ShortCategoryDto>> CreateCategoryAsync(CreateShortCategoryCommand command);
    Task<Result<bool>> UpdateCategoryAsync(UpdateShortCategoryCommand command);
    Task<Result<bool>> DeleteCategoryAsync(Guid id);

    Task<Result<IReadOnlyList<ShortDto>>> GetAllShortsAsync();
    Task<Result<ShortDto>> GetShortByIdAsync(Guid id);
    Task<Result<Guid>> CreateShortAsync(CreateShortCommand command);
    Task<Result<bool>> UpdateShortAsync(UpdateShortCommand command);
    Task<Result<bool>> DeleteShortAsync(Guid id);
}