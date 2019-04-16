using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payment.Registration.Domain.Specifications;

namespace Payment.Registration.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    { 
        Task<TEntity> Add(TEntity item);
        
        Task<TEntity> Get(ISpecification<TEntity> spec);
        
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        
        Task Remove(TEntity item);
        
        void Update(TEntity item); 
    }
}