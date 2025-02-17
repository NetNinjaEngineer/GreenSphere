using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler(IProductsService service)
        : IRequestHandler<UpdateCategoryCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            => await service.UpdateCategoryAsync(request);
    }
}