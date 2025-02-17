using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetCustomerBasketWithItemsSpecification : BaseSpecification<CustomerBasket>
{
    public GetCustomerBasketWithItemsSpecification(string ownerEmail)
        : base(basket => basket.CustomerEmail == ownerEmail)
    {
        AddInclude(basket => basket.BasketItems);
        AddInclude(basket => basket.BasketItems, item => item.Product);
        AddInclude(basket => basket.BasketItems, item => item.Product.ProductTranslations);
    }
}