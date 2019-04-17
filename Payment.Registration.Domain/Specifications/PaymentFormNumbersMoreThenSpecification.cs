using System;
using System.Linq.Expressions;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.Domain.Specifications
{
    public class PaymentFormNumbersMoreThenSpecification : ISpecification<PaymentForm>
    {
        public PaymentFormNumbersMoreThenSpecification(int number)
        {
            IsSatisfiedBy = p => p.Number > number;
        }
        
        public Expression<Func<PaymentForm, bool>> IsSatisfiedBy { get; }
    }
}