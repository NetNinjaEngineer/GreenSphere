using GreenSphere.Domain.Common;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Interfaces;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllWithSpecificationAsync(IBaseSpecification<T> specification);
    Task<int> GetCountWithSpecificationAsync(IBaseSpecification<T> specification);
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetBySpecificationAsync(IBaseSpecification<T> specification);
    Task<T?> GetBySpecificationAndIdAsync(IBaseSpecification<T> specification, Guid id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveChangesAsync();



}