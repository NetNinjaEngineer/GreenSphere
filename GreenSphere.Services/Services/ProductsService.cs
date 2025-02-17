using AutoMapper;
using FluentValidation;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Category;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Features.Categories.Commands.CreateCategory;
using GreenSphere.Application.Features.Categories.Commands.DeleteCategory;
using GreenSphere.Application.Features.Categories.Commands.UpdateCategory;
using GreenSphere.Application.Features.Categories.Queries.GetCategoryWithProducts;
using GreenSphere.Application.Features.Products.Commands.CreateProduct;
using GreenSphere.Application.Features.Products.Commands.DeleteProduct;
using GreenSphere.Application.Features.Products.Commands.UpdateProduct;
using GreenSphere.Application.Features.Products.Queries.GetProduct;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Specifications;
using GreenSphere.Domain.Utils;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GreenSphere.Services.Services;
public sealed class ProductsService(
    IMapper mapper,
    IGenericRepository<Product> productsRepository,
    IGenericRepository<Category> categoryRepository,
    IFileService fileService,
    IStringLocalizer<ProductsService> localizer) : IProductsService
{
    public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductSpecParams? @params)
    {
        var specification = new GetAllProductsSpecification(@params);

        var products = await productsRepository.GetAllWithSpecificationAsync(specification);

        var mappedResults = mapper.Map<IReadOnlyList<ProductDto>>(products);

        return Result<IReadOnlyList<ProductDto>>.Success(mappedResults);

    }

    public async Task<Result<ProductDto>> GetProductAsync(GetProductQuery query)
    {
        var existedProduct = await productsRepository.GetBySpecificationAndIdAsync(
            specification: new GetProductWithDetailsSpecification(),
            id: query.Id);

        if (existedProduct is null)
            return Result<ProductDto>.Failure(HttpStatusCode.NotFound, localizer["ProductNotFound", query.Id]);

        var mappedResult = mapper.Map<ProductDto>(existedProduct);

        return Result<ProductDto>.Success(mappedResult);
    }

    public async Task<Result<Guid>> CreateProductAsync(CreateProductCommand command)
    {
        await new CreateProductCommandValidator().ValidateAndThrowAsync(command);

        var existedCategory = await categoryRepository.GetByIdAsync(command.CategoryId);

        if (existedCategory is null)
            return Result<Guid>.Failure(HttpStatusCode.NotFound, localizer["CategoryNotFound", command.CategoryId]);

        var uploadedImageName = await fileService.UploadFileAsync(command.Image, "Images");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            DiscountPercentage = command.DiscountPercentage,
            CategoryId = command.CategoryId,
            Img = uploadedImageName,
            OriginalPrice = command.Price
        };

        productsRepository.Create(product);

        await productsRepository.SaveChangesAsync();

        return Result<Guid>.Success(product.Id);
    }

    public async Task<Result<bool>> DeleteProductAsync(DeleteProductCommand command)
    {
        var existedProduct = await productsRepository.GetByIdAsync(command.ProductId);

        if (existedProduct is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, localizer["ProductNotFound", command.ProductId]);

        productsRepository.Delete(existedProduct);

        await productsRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UploadProductAsync(UpdateProductCommand command)
    {
        await new UpdateProductCommandValidator().ValidateAndThrowAsync(command);

        var existedProduct = await productsRepository.GetByIdAsync(command.ProductId);

        if (existedProduct is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, localizer["ProductNotFound", command.ProductId]);

        existedProduct.Name = command.Name;
        existedProduct.Description = command.Description;
        existedProduct.DiscountPercentage = command.DiscountPercentage;

        if (command.Image is not null)
        {
            fileService.DeleteFileFromPath(existedProduct.Img, "Images");
            var uploadedImageName = await fileService.UploadFileAsync(command.Image, "Images");
            existedProduct.Img = uploadedImageName;
        }

        if (command.CategoryId.HasValue)
        {
            var existedCategory = await categoryRepository.GetByIdAsync(command.CategoryId.Value);

            if (existedCategory is null)
                return Result<bool>.Failure(HttpStatusCode.NotFound, localizer["CategoryNotFound", command.CategoryId]);

            existedProduct.CategoryId = command.CategoryId.Value;
        }

        productsRepository.Update(existedProduct);

        await productsRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<Guid>> CreatCategoryAsync(CreateCategoryCommand command)
    {
        await new CreateCategoryCommandValidator().ValidateAndThrowAsync(command);
        var existedCategory = await categoryRepository.GetBySpecificationAsync(new CheckIsCategoryExistsSpecification(command.Name));

        if (existedCategory != null)
            return Result<Guid>.Failure(HttpStatusCode.BadRequest, localizer["CategoryAlreadyExists", command.Name]);

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description
        };

        categoryRepository.Create(category);
        await categoryRepository.SaveChangesAsync();

        return Result<Guid>.Success(category.Id);
    }
    public async Task<Result<Guid>> UpdateCategoryAsync(UpdateCategoryCommand command)
    {
        await new UpdateCategoryCommandValidator().ValidateAndThrowAsync(command);

        var existingCategory = await categoryRepository.GetByIdAsync(command.CategoryId);

        if (existingCategory is null)
            return Result<Guid>.Failure(HttpStatusCode.NotFound, localizer["CategoryNotFound", command.CategoryId]);

        if (command.Name != null)
            existingCategory.Name = command.Name;

        existingCategory.Description = command.Description;

        categoryRepository.Update(existingCategory);
        await categoryRepository.SaveChangesAsync();

        return Result<Guid>.Success(existingCategory.Id);
    }

    public async Task<Result<bool>> DeleteCategoryAsync(DeleteCategoryCommand command)
    {
        var existingCategory = await categoryRepository.GetByIdAsync(command.CategoryId);

        if (existingCategory is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, localizer["CategoryNotFound", command.CategoryId]);

        categoryRepository.Delete(existingCategory);
        await categoryRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }


    public async Task<Result<IReadOnlyList<CategoryDto>>> GetAllCategoriesAsync(CategorySpecParams? @params)
    {
        var specification = new GetAllCategoriesSpecification(@params);

        var categories = await categoryRepository.GetAllWithSpecificationAsync(specification);

        var mappedResults = mapper.Map<IReadOnlyList<CategoryDto>>(categories);

        return Result<IReadOnlyList<CategoryDto>>.Success(mappedResults);
    }

    public async Task<Result<CategoryWithProductsDto>> GetCategoryWithProductsAsync(Guid categoryId)
    {
        var specification = new GetCategoryWithProductsSpecification(categoryId);

        var category = await categoryRepository.GetBySpecificationAsync(specification);

        if (category is null)
            return Result<CategoryWithProductsDto>.Failure(HttpStatusCode.NotFound, localizer["CategoryNotFound", categoryId]);

        var mappedResult = mapper.Map<CategoryWithProductsDto>(category);

        return Result<CategoryWithProductsDto>.Success(mappedResult);
    }
}
