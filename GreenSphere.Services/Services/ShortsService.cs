using System.Net;
using AutoMapper;
using FluentValidation;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Features.Shorts.Commands.CreateShort;
using GreenSphere.Application.Features.Shorts.Commands.CreateShortCategory;
using GreenSphere.Application.Features.Shorts.Commands.UpdateShort;
using GreenSphere.Application.Features.Shorts.Commands.UpdateShortCategory;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Specifications;

namespace GreenSphere.Services.Services;

public sealed class ShortsService(
    IMapper mapper,
    ICurrentUser currentUser,
    IGenericRepository<ShortCategory> categoryRepository,
    IGenericRepository<Short> shortRepository,
    IFileService fileService) : IShortsService
{
    public async Task<Result<IReadOnlyList<ShortCategoryDto>>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        var mappedCategories = mapper.Map<IReadOnlyList<ShortCategoryDto>>(categories);
        return Result<IReadOnlyList<ShortCategoryDto>>.Success(mappedCategories);
    }

    public async Task<Result<ShortCategoryDto>> GetCategoryByIdAsync(Guid id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if (category == null)
            return Result<ShortCategoryDto>.Failure(HttpStatusCode.NotFound);

        var mappedCategory = mapper.Map<ShortCategoryDto>(category);

        return Result<ShortCategoryDto>.Success(mappedCategory);
    }

    public Task<Result<ShortCategoryDto>> CreateCategoryAsync(CreateShortCategoryCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateCategoryAsync(UpdateShortCategoryCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteCategoryAsync(Guid id)
    {
        var existedCategory = await categoryRepository.GetByIdAsync(id);
        if (existedCategory == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound);

        categoryRepository.Delete(existedCategory);
        await categoryRepository.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<IReadOnlyList<ShortDto>>> GetAllShortsAsync()
    {
        var specification = new ShortsWithDetailsSpecification();
        var shorts = await shortRepository.GetAllWithSpecificationAsync(specification);
        var mappedShorts = mapper.Map<IReadOnlyList<ShortDto>>(shorts);
        return Result<IReadOnlyList<ShortDto>>.Success(mappedShorts);
    }

    public async Task<Result<ShortDto>> GetShortByIdAsync(Guid id)
    {
        var existedShort = await shortRepository.GetBySpecificationAsync(
            new ShortWithDetailsSpecification(id));

        if (existedShort == null)
            return Result<ShortDto>.Failure(HttpStatusCode.NotFound);

        var mappedShort = mapper.Map<ShortDto>(existedShort);

        return Result<ShortDto>.Success(mappedShort);
    }

    public async Task<Result<Guid>> CreateShortAsync(CreateShortCommand command)
    {
        await new CreateShortCommandValidator().ValidateAndThrowAsync(command, CancellationToken.None);

        var existedCategory = await categoryRepository.GetByIdAsync(command.ShortCategoryId);
        if (existedCategory == null)
            return Result<Guid>.Failure(HttpStatusCode.BadRequest);

        var thumbnailPath = string.Empty;

        if (command.Thumbnail is not null)
            thumbnailPath = await fileService.UploadFileAsync(command.Thumbnail, "Images");

        var shortPath = await fileService.UploadFileAsync(command.Video, "Shorts");

        var mappedShort = mapper.Map<Short>(command);

        mappedShort.Id = Guid.NewGuid();
        mappedShort.CreatorId = currentUser.Id;
        mappedShort.ThumbnailUrl = !string.IsNullOrEmpty(thumbnailPath) ? thumbnailPath : null;
        mappedShort.VideoUrl = shortPath;

        shortRepository.Create(mappedShort);

        await shortRepository.SaveChangesAsync();

        return Result<Guid>.Success(mappedShort.Id);
    }

    public Task<Result<bool>> UpdateShortAsync(UpdateShortCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteShortAsync(Guid id)
    {
        var existedShort = await shortRepository.GetByIdAsync(id);

        if (existedShort == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound);

        fileService.DeleteFileFromPath(existedShort.VideoUrl, "Shorts");

        if (!string.IsNullOrEmpty(existedShort.ThumbnailUrl))
            fileService.DeleteFileFromPath(existedShort.ThumbnailUrl, "Images");

        shortRepository.Delete(existedShort);

        await shortRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}