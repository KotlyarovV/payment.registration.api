using Microsoft.EntityFrameworkCore;
using Payment.Registration.Domain;
using Payment.Registration.Domain.Models;
using Payment.Registration.Ef.EntityTypeConfigurations;

namespace Payment.Registration.Ef.DbContexts
{
    public class PaymentFormDbContext : DbContext
    {
        public PaymentFormDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<PaymentForm> PaymentForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder
                .ApplyConfiguration(new ApplicantEntityTypeConfiguration())
                .ApplyConfiguration(new PaymentFormEntityTypeConfigurator())
                .ApplyConfiguration(new PaymentPositionEntityTypeConfigurator());
        }
    }
}
