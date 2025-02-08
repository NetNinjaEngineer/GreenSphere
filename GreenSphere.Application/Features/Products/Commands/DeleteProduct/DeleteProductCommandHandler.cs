using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Products.Commands.DeleteProduct;
public sealed class DeleteProductCommandHandler(IProductsService service)
    : IRequestHandler<DeleteProductCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        => await service.DeleteProductAsync(request);
}
