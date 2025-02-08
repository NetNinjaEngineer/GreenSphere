using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Products.Commands.UpdateProduct;
public sealed class UpdateProductCommandHandler(IProductsService service)
    : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        => await service.UploadProductAsync(request);
}
