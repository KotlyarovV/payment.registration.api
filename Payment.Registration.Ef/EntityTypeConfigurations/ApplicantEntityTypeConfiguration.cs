using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Registration.Domain;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.Ef.EntityTypeConfigurations
{
    public class ApplicantEntityTypeConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.HasOne<PaymentForm>()
                .WithOne(p => p.Applicant)
                .HasForeignKey<PaymentForm>("ApplicantId");
        }
    }
}