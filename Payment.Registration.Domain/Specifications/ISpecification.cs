using System;
using System.Linq.Expressions;

namespace Payment.Registration.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfiedBy { get; }
    }
}