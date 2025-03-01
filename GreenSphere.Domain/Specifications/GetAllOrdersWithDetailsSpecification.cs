using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetAllOrdersWithDetailsSpecification : BaseSpecification<Order>
{
    public GetAllOrdersWithDetailsSpecification()
    {
        AddInclude(order => order.User);
        AddInclude(order => order.OrderItems);
    }

    public GetAllOrdersWithDetailsSpecification(string currentUserId) : base(order => order.UserId == currentUserId)
    {
        AddInclude(order => order.User);
        AddInclude(order => order.OrderItems);
    }

}