using GreenSphere.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public IFormFile Image { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal? DiscountPercentage { get; set; }
    public Guid CategoryId { get; set; }
}