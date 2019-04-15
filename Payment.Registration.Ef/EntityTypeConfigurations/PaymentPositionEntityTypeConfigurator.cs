using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Registration.Domain;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.Ef.EntityTypeConfigurations
{
    public class PaymentPositionEntityTypeConfigurator : IEntityTypeConfiguration<PaymentPosition>
    {
        public void Configure(EntityTypeBuilder<PaymentPosition> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Sum).HasColumnType("decimal(5, 2)");
            builder.HasMany(p => p.Files)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}