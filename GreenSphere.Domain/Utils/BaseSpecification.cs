using GreenSphere.Domain.Common;
using System.Linq.Expressions;

namespace GreenSphere.Domain.Utils;
public abstract class BaseSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
{
    private readonly List<IIncludeExpression<TEntity>> _includeExpressions = [];

    // Func<TEntity, object> =>  fuction => // e => // user profile => u => u.Profile

    // context.Users.Where(u => u.IsActive== true) // Expression<Func<T, bool>>
    // name email // .OrderBy(u => u.id) // Func<T, object> // List<Expression< Func<T, object>>> // 
    // .Skip().Take() // Paging => 1 => 10 2=> 10 // 1 2 3 4 5 // 1 - 5 // 20
    // context.Users.Include(u => u.Address).Include()

    public List<Expression<Func<TEntity, object>>> OrderBy { get; set; } = [];
    public List<Expression<Func<TEntity, object>>> OrderByDescending { get; set; } = [];
    public Expression<Func<TEntity, bool>>? Criteria { get; set; }
    public bool IsTracking { get; set; } = true;
    public bool IsPagingEnabled { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }

    protected BaseSpecification() { }

    protected BaseSpecification(Expression<Func<TEntity, bool>> criteriaExpression)
        => Criteria = criteriaExpression;

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        _includeExpressions.Add(new IncludeExpression<TEntity>(includeExpression));
    }

    protected void AddInclude<TPreviousProperty, TProperty>(
        Expression<Func<TEntity, IEnumerable<TPreviousProperty>>> previousExpression,
        Expression<Func<TPreviousProperty, TProperty>> thenExpression)
    {
        _includeExpressions.Add(
            new ThenIncludeExpression<TEntity, TPreviousProperty, TProperty>(
                previousExpression, thenExpression));
    }

    public IReadOnlyList<IIncludeExpression<TEntity>> IncludeExpressions => _includeExpressions;

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        => OrderBy.Add(orderByExpression);

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescending.Add(orderByDescendingExpression);

    protected void DisableTracking() => IsTracking = false;

    protected void ApplyPaging(int skip, int take)
    {
        IsPagingEnabled = true;
        Skip = skip;
        Take = take;
    }

}
