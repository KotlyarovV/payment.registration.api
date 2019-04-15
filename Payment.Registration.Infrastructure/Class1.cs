using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Registration.App.Services;
using Payment.Registration.Domain.Models;
using Payment.Registration.Ef.DbContexts;

namespace Payment.Registration.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    { 
        Task<TEntity> Add(TEntity item);
        
        IEnumerable<TEntity> Get();
        
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        
        void Remove(TEntity item);
        
        void Update(TEntity item); 
    }
    
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
        
        public IEnumerable<TEntity> Get()
        {
            return ConfigureQuery().ToList();
        }
         
        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return ConfigureQuery().Where(predicate).ToList();
        }
 
        public async Task<TEntity> Add(TEntity item)
        {
            await dbSet.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }
        
        public void Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            dbSet.Remove(item);
            context.SaveChanges();
        }
    }
    
    public class PaymentFormDataService : Repository<PaymentForm>, IDataService<PaymentForm>
    {
        public PaymentFormDataService(PaymentFormDbContext context) : base(context)
        {
        }

        protected override IQueryable<PaymentForm> ConfigureQuery()
        {
            return dbSet
                .Include(p => p.Applicant)
                .Include(p => p.Items)
                .ThenInclude(i => i.Files);
        }

        public async Task<IEnumerable<PaymentForm>> GetAll()
        {
            return await ConfigureQuery().ToArrayAsync();
        }
    }
}
