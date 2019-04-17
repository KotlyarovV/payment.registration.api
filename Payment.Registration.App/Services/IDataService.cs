using System.Collections.Generic;
using System.Threading.Tasks;
using Payment.Registration.Domain.Specifications;

namespace Payment.Registration.App.Services
{
    public interface IDataService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity> specification);
        
        Task<TEntity> Get(ISpecification<TEntity> spec);
        
        Task<TEntity> Add(TEntity entity);

        Task Update(TEntity entity);

        Task Remove(TEntity entity);
    }
}