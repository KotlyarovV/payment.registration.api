using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Registration.Domain;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.Ef.EntityTypeConfigurations
{
    public class PaymentFormEntityTypeConfigurator : IEntityTypeConfiguration<PaymentForm>
    {
        public void Configure(EntityTypeBuilder<PaymentForm> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Applicant)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}