using GreenSphere.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Domain.Specifications;
public static class SpecificationQueryEvaluator
{
    public static IQueryable<TEntity> BuildQuery<TEntity>(
        IQueryable<TEntity> query,
        IBaseSpecification<TEntity> specification) where TEntity : BaseEntity
    {
        var inputQuery = query.AsQueryable();

        if (specification.Includes.Count != 0)
            inputQuery = specification.Includes.Aggregate(inputQuery, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

        if (specification.Criteria != null)
            inputQuery = inputQuery.Where(specification.Criteria);

        if (specification.OrderBy.Count != 0)
        {
            var firstOrderBy = specification.OrderBy.First();
            var orderedQuery = inputQuery.OrderBy(firstOrderBy);

            foreach (var additionalOrderBy in specification.OrderBy.Skip(1))
            {
                orderedQuery = orderedQuery.ThenBy(additionalOrderBy);
            }
            inputQuery = orderedQuery;
        }

        if (specification.OrderByDescending.Count != 0)
        {
            var firstOrderByExpression = specification.OrderByDescending.First();
            var orderedQuery = inputQuery.OrderByDescending(firstOrderByExpression);
            foreach (var additionalOrderByExpression in specification.OrderByDescending.Skip(1))
            {
                orderedQuery = orderedQuery.ThenByDescending(additionalOrderByExpression);
            }
            inputQuery = orderedQuery;
        }

        if (!specification.IsTracking)
            inputQuery = inputQuery.AsNoTracking();

        if (specification.IsPagingEnabled)
            inputQuery = inputQuery.Skip((specification.Skip - 1) * specification.Take).Take(specification.Take);

        return inputQuery;

    }
}