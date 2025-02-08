using AutoMapper;
using FluentValidation;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
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
}
