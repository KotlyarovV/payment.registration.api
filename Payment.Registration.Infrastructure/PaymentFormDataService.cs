using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Registration.App.Services;
using Payment.Registration.Domain.Models;
using Payment.Registration.Ef.DbContexts;

namespace Payment.Registration.Infrastructure
{
    public class PaymentFormDataService : Repository<PaymentForm>, IPaymentFormDataService
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

        public async Task<int> GetNewPaymentFormNumber()
        {
            var numbers = await ConfigureQuery()
                .Select(p => p.Number)
                .ToArrayAsync();

            return numbers.Any() ? numbers.Max() + 1 : 1;
        }
    }
}
