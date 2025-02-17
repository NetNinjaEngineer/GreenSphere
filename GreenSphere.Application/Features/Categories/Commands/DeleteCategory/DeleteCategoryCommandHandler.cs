using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler(IProductsService service)
        : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            => await service.DeleteCategoryAsync(request);
    }
}