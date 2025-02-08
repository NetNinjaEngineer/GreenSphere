using AutoMapper;
using FluentValidation;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Features.Products.Commands.CreateProduct;
using GreenSphere.Application.Features.Products.Queries.GetProduct;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Specifications;
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
    public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync()
    {
        var specification = new GetAllProductsSpecification();

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

        var existedCategoyry = await categoryRepository.GetByIdAsync(command.CategoryId);

        if (existedCategoyry is null)
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
}
