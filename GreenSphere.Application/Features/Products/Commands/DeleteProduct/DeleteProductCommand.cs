using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Products.Commands.DeleteProduct;
public sealed class DeleteProductCommand : IRequest<Result<bool>>
{
    public Guid ProductId { get; set; }
}
