using GreenSphere.Domain.Common;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Persistence.Repositories;
public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly ApplicationDbContext Context = context;

    public async Task<IEnumerable<T>> GetAllAsync()
        => await Context.Set<T>().ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id) => await Context.Set<T>().FindAsync(id);

    public async Task<T?> GetBySpecificationAsync(IBaseSpecification<T> specification)
        => await SpecificationQueryEvaluator.BuildQuery(Context.Set<T>(), specification).FirstOrDefaultAsync();

    public async Task<T?> GetBySpecificationAndIdAsync(IBaseSpecification<T> specification, Guid id)
        => await SpecificationQueryEvaluator.BuildQuery(Context.Set<T>(), specification).FirstOrDefaultAsync(e => e.Id == id);

    public void Create(T entity) => Context.Set<T>().Add(entity);

    public void Update(T entity) => Context.Set<T>().Update(entity);

    public void Delete(T entity) => Context.Set<T>().Remove(entity);
    public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();

    public async Task<IEnumerable<T>> GetAllWithSpecificationAsync(IBaseSpecification<T> specification)
        => await SpecificationQueryEvaluator.BuildQuery(Context.Set<T>(), specification).ToListAsync();


    public async Task<int> GetCountWithSpecificationAsync(IBaseSpecification<T> specification)
        => await SpecificationQueryEvaluator.BuildQuery(Context.Set<T>(), specification).CountAsync();


}
