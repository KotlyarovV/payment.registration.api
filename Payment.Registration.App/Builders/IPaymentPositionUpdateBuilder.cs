using System.Collections.Generic;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.App.Builders
{
    public interface IPaymentPositionUpdateBuilder : IBuilder<PaymentPositionUpdateDto, IReadOnlyCollection<File>, PaymentPosition>
    {
        PaymentPosition Map(PaymentPositionUpdateDto source, IReadOnlyCollection<File> files, PaymentPosition destination);
    }
}