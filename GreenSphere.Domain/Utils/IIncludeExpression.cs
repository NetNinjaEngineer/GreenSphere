using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Utils;

public interface IIncludeExpression<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> AddInclude(IQueryable<TEntity> query);
}