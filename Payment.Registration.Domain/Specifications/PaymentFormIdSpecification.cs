using System;
using System.Linq.Expressions;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.Domain.Specifications
{
    public class PaymentFormIdSpecification : ISpecification<PaymentForm>
    {
        public PaymentFormIdSpecification(Guid id)
        {
            IsSatisfiedBy = p => p.Id == id;
        }
        
        public Expression<Func<PaymentForm, bool>> IsSatisfiedBy { get; }
    }
}