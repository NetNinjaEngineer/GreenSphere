using GreenSphere.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Domain.Utils;
public static class SpecificationQueryEvaluator
{
    public static IQueryable<TEntity> BuildQuery<TEntity>(
        IQueryable<TEntity> query,
        IBaseSpecification<TEntity> specification) where TEntity : BaseEntity
    {
        var inputQuery = query;

        if (specification.IncludeExpressions.Count > 0)
        {
            inputQuery = specification.IncludeExpressions.Aggregate(
                inputQuery,
                (current, includeExpression) => includeExpression.AddInclude(current));
        }


        if (specification.Criteria != null)
            inputQuery = inputQuery.Where(specification.Criteria);

        if (specification.OrderBy.Count != 0)
        {
            var firstOrderBy = specification.OrderBy.First();
            var orderedQuery = inputQuery.OrderBy(firstOrderBy);
            orderedQuery = specification.OrderBy.Skip(1).Aggregate(orderedQuery, (current, additionalOrderBy) => current.ThenBy(additionalOrderBy));
            inputQuery = orderedQuery;
        }

        if (specification.OrderByDescending.Count != 0)
        {
            var firstOrderByExpression = specification.OrderByDescending.First();
            var orderedQuery = inputQuery.OrderByDescending(firstOrderByExpression);
            orderedQuery = specification.OrderByDescending.Skip(1).Aggregate(orderedQuery, (current, additionalOrderByExpression) => current.ThenByDescending(additionalOrderByExpression));
            inputQuery = orderedQuery;
        }

        if (!specification.IsTracking)
            inputQuery = inputQuery.AsNoTracking();

        if (specification.IsPagingEnabled)
            inputQuery = inputQuery.Skip((specification.Skip - 1) * specification.Take).Take(specification.Take);

        return inputQuery;

    }
}