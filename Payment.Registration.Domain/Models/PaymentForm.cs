using System;
using System.Collections.Generic;

namespace Payment.Registration.Domain.Models
{
    public class PaymentForm
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public Applicant Applicant { get; set; }

        public Type Type { get; set; }

        public ICollection<PaymentPosition> Items { get; set; }
    }
}