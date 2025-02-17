using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler(IProductsService service)
        : IRequestHandler<CreateCategoryCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            => await service.CreatCategoryAsync(request);

    }
}
