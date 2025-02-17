using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetBasketItemSpecification : BaseSpecification<BasketItem>
{
    public GetBasketItemSpecification(string customerEmail, Guid productId)
        : base(basketItem => basketItem.ProductId == productId && basketItem.CustomerBasket.CustomerEmail == customerEmail)
    {
        AddInclude(basketItem => basketItem.CustomerBasket);
    }

    public GetBasketItemSpecification(Guid basketItemId, string customerEmail)
        : base(basketItem => basketItem.Id == basketItemId && basketItem.CustomerBasket.CustomerEmail == customerEmail)
    {
        AddInclude(basketItem => basketItem.CustomerBasket);
    }
}