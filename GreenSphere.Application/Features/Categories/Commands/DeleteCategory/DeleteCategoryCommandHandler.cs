using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler(IProductsService service)
        : IRequestHandler<DeleteCategoryCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            => await service.DeleteCategoryAsync(request);
    }
}