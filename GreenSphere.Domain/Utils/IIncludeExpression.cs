using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Utils;

public interface IIncludeExpression<TEntity> where TEntity : BaseEntity
{
    // context.Users // initial query // IEnumerable // [u.Add, u.Profile] // var pro = context.Products;
    IQueryable<TEntity> AddInclude(IQueryable<TEntity> query);
}