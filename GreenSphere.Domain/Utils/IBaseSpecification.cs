using GreenSphere.Domain.Common;
using System.Linq.Expressions;

namespace GreenSphere.Domain.Utils;
public interface IBaseSpecification<TEntity> where TEntity : BaseEntity
{
    List<Expression<Func<TEntity, object>>> OrderBy { get; set; }
    List<Expression<Func<TEntity, object>>> OrderByDescending { get; set; }
    public Expression<Func<TEntity, bool>>? Criteria { get; set; }
    public bool IsPagingEnabled { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public bool IsTracking { get; set; }
    IReadOnlyList<IIncludeExpression<TEntity>> IncludeExpressions { get; }

}
