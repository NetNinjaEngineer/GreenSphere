using GreenSphere.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreenSphere.Domain.Utils;

// u => u.Profile // Expression<Func<T, object>>
public class IncludeExpression<TEntity>(Expression<Func<TEntity, object>> expression) : IIncludeExpression<TEntity>
    where TEntity : BaseEntity // contratient
{
    public IQueryable<TEntity> AddInclude(IQueryable<TEntity> query)
    {
        return query.Include(expression);
    }
}