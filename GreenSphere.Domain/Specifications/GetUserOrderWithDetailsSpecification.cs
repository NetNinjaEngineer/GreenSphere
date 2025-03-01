using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetUserOrderWithDetailsSpecification : BaseSpecification<Order>
{
    public GetUserOrderWithDetailsSpecification(Guid orderId, string currentUserId) : base(order => order.UserId == currentUserId && order.Id == orderId)
    {
        AddInclude(order => order.User);
        AddInclude(order => order.OrderItems);
    }
}