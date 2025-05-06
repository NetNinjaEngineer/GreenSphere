using GreenSphere.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Products.Commands.UpdateProduct;
public sealed class UpdateProductCommand : IRequest<Result<bool>>
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public IFormFile? Image { get; set; }
    public string Description { get; set; } = null!;
    public decimal? DiscountPercentage { get; set; }
    public Guid? CategoryId { get; set; }
    public long? StockQuantity { get; set; }
    public long? PointsCost { get; set; }
}