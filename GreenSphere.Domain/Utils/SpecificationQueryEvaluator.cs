using GreenSphere.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Domain.Utils;
public static class SpecificationQueryEvaluator
{
    // context.Users// base q => // 
    public static IQueryable<TEntity> BuildQuery<TEntity>(
        IQueryable<TEntity> query,
        IBaseSpecification<TEntity> specification) where TEntity : BaseEntity
    {
        var inputQuery = query; // context.Users.Include(x => x.ha).inc

        if (specification.IncludeExpressions.Count > 0) // 
        {
            // include(x => x.Profile).Incluide(x => x.Add)
            //foreach (var include in specification.IncludeExpressions)
            //{
            //    inputQuery = inputQuery.Include(include.AddInclude());
            //}

            // var t = 10;
            // for => i in [1, 2, 3] ===> t+= i ==> 10 + 1 => 11 => 13 => 16
            // context.Users.Include(x => x.Profile)
            // context.Users.Include(x => x.Profile).INclude(x => x.add)

            inputQuery = specification.IncludeExpressions.Aggregate(
                inputQuery,
                (current, includeExpression) => includeExpression.AddInclude(current));
        }


        if (specification.Criteria != null)
            inputQuery = inputQuery.Where(specification.Criteria);

        if (specification.OrderBy.Count != 0)
        {
            var firstOrderBy = specification.OrderBy.First();
            var orderedQuery = inputQuery.OrderBy(firstOrderBy); // [1, 2, 3] // 1
            //  list.skip(1) // [2, 3]
            orderedQuery = specification.OrderBy
                .Skip(1)
                .Aggregate(orderedQuery, (current, additionalOrderBy) => current.ThenBy(additionalOrderBy));
            inputQuery = orderedQuery;
        }

        if (specification.OrderByDescending.Count != 0)
        {
            var firstOrderByExpression = specification.OrderByDescending.First();
            var orderedQuery = inputQuery.OrderByDescending(firstOrderByExpression);
            // context.Users.OrderBy(x => x.Name).ThenBy
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