using System.Net;
using AutoMapper;
using FluentValidation;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.Features.ShortCategories.Commands.CreateShortCategory;
using GreenSphere.Application.Features.Shorts.Commands.CreateShort;
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

    public async Task<Result<ShortCategoryDto>> CreateCategoryAsync(CreateShortCategoryCommand command)
    {
        await new CreateShortCategoryCommandValidator().ValidateAndThrowAsync(command, CancellationToken.None);

        var specification = new CheckDuplicateCategorySpecification(
            nameEn: command.NameEn,
            nameAr: command.NameAr);

        var existingCategory = await categoryRepository.GetBySpecificationAsync(specification);

        if (existingCategory != null)
        {
            return Result<ShortCategoryDto>.Failure(
                HttpStatusCode.Conflict,
                "Category with the same name already exists");
        }

        var mappedCategory = mapper.Map<ShortCategory>(command);
        mappedCategory.Id = Guid.NewGuid();
        mappedCategory.CreatedAt = DateTimeOffset.Now;

        categoryRepository.Create(mappedCategory);
        await categoryRepository.SaveChangesAsync();

        var categoryDto = mapper.Map<ShortCategoryDto>(mappedCategory);

        return Result<ShortCategoryDto>.Success(categoryDto, "Category created successfully");
    }

    public async Task<Result<bool>> UpdateCategoryAsync(UpdateShortCategoryCommand command)
    {
        await new UpdateShortCategoryCommandValidator().ValidateAndThrowAsync(command, CancellationToken.None);

        var existingCategory = await categoryRepository.GetByIdAsync(command.Id);
        if (existingCategory == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, "Category not found");

        if (!string.IsNullOrEmpty(command.NameEn) || !string.IsNullOrEmpty(command.NameAr))
        {
            var newNameEn = command.NameEn ?? existingCategory.NameEn;
            var newNameAr = command.NameAr ?? existingCategory.NameAr;

            var duplicateSpec = new CheckDuplicateCategorySpecification(
                nameEn: newNameEn,
                nameAr: newNameAr);

            var duplicateCategory = await categoryRepository.GetBySpecificationAsync(duplicateSpec);
            if (duplicateCategory != null && duplicateCategory.Id != command.Id)
                return Result<bool>.Failure(HttpStatusCode.Conflict, "Category with this name already exists");
        }

        if (!string.IsNullOrEmpty(command.NameEn))
            existingCategory.NameEn = command.NameEn;

        if (!string.IsNullOrEmpty(command.NameAr))
            existingCategory.NameAr = command.NameAr;

        if (!string.IsNullOrEmpty(command.Description))
            existingCategory.Description = command.Description;

        existingCategory.UpdatedAt = DateTimeOffset.Now;

        categoryRepository.Update(existingCategory);
        var updated = await categoryRepository.SaveChangesAsync();

        return Result<bool>.Success(updated > 0, updated > 0 ? "Category updated successfully" : "No changes were made");
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

    public async Task<Result<bool>> UpdateShortAsync(UpdateShortCommand command)
    {
        await new UpdateShortCommandValidator().ValidateAndThrowAsync(command, CancellationToken.None);

        var existingShort = await shortRepository.GetByIdAsync(command.Id);
        if (existingShort == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, "Short not found");

        if (existingShort.CreatorId != currentUser.Id)
            return Result<bool>.Failure(HttpStatusCode.Forbidden, "You don't have permission to update this short");

        if (command.ShortCategoryId.HasValue)
        {
            var existedCategory = await categoryRepository.GetByIdAsync(command.ShortCategoryId.Value);
            if (existedCategory == null)
                return Result<bool>.Failure(HttpStatusCode.BadRequest, "Invalid category");
        }

        if (command.Thumbnail is not null)
        {
            if (!string.IsNullOrEmpty(existingShort.ThumbnailUrl))
            {
                fileService.DeleteFileFromPath(existingShort.ThumbnailUrl, "Images");
            }

            existingShort.ThumbnailUrl = await fileService.UploadFileAsync(command.Thumbnail, "Images");
        }

        if (command.Video is not null)
        {
            if (!string.IsNullOrEmpty(existingShort.VideoUrl))
            {
                fileService.DeleteFileFromPath(existingShort.VideoUrl, "Shorts");
            }

            existingShort.VideoUrl = await fileService.UploadFileAsync(command.Video, "Shorts");
        }

        if (!string.IsNullOrEmpty(command.Title))
            existingShort.Title = command.Title;

        if (command.Description != null)
            existingShort.Description = command.Description;

        if (command.IsFeatured.HasValue)
            existingShort.IsFeatured = command.IsFeatured.Value;

        if (command.ShortCategoryId.HasValue)
            existingShort.ShortCategoryId = command.ShortCategoryId.Value;

        shortRepository.Update(existingShort);
        var updated = await shortRepository.SaveChangesAsync();

        return Result<bool>.Success(updated > 0, updated > 0 ? "Short updated successfully" : "No changes were made");
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