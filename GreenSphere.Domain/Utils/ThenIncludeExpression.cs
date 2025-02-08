using GreenSphere.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreenSphere.Domain.Utils;

public class ThenIncludeExpression<TEntity, TPreviousProperty, TProperty>(
    Expression<Func<TEntity, IEnumerable<TPreviousProperty>>> previousExpression,
    Expression<Func<TPreviousProperty, TProperty>> thenExpression)
    : IIncludeExpression<TEntity>
    where TEntity : BaseEntity
{
    public IQueryable<TEntity> AddInclude(IQueryable<TEntity> query)
    {
        return query.Include(previousExpression).ThenInclude(thenExpression);
    }
}