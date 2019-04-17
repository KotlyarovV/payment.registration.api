using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Registration.Domain.Specifications;

namespace Payment.Registration.Infrastructure
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        protected readonly DbSet<TEntity> dbSet;

        protected Repository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        protected abstract IQueryable<TEntity> ConfigureQuery();
        
        public Task<TEntity> Get(ISpecification<TEntity> spec)
        {
            return ConfigureQuery().Where(spec.IsSatisfiedBy).SingleAsync();
        }
         
        public async Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity> spec)
        {
            return await ConfigureQuery().Where(spec.IsSatisfiedBy).ToListAsync();
        }
 
        public async Task<TEntity> Add(TEntity item)
        {
            await dbSet.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }
        
        public async Task Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        
        public async Task Remove(TEntity item)
        {
            dbSet.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}