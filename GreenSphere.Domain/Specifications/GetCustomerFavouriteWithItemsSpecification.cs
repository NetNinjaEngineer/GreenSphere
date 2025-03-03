using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetCustomerFavouriteWithItemsSpecification : BaseSpecification<CustomerFavourite>
{
    public GetCustomerFavouriteWithItemsSpecification(string ownerEmail)
        : base(favourite => favourite.CustomerEmail == ownerEmail)
    {
        AddInclude(basket => basket.FavouriteItems);
        AddInclude(basket => basket.FavouriteItems, item => item.Product);
        AddInclude(basket => basket.FavouriteItems, item => item.Product.ProductTranslations);
    }
}
