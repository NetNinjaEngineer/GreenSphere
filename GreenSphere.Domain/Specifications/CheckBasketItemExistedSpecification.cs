using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class CheckBasketItemExistedSpecification : BaseSpecification<BasketItem>
{
    public CheckBasketItemExistedSpecification(Guid basketItemId, string customerEmail, Guid basketId)
        : base(basketItem =>
            basketItem.Id == basketItemId &&
            basketItem.CustomerBasketId == basketId &&
            basketItem.CustomerBasket.CustomerEmail == customerEmail)
    {
        AddInclude(basketItem => basketItem.CustomerBasket);
    }
}