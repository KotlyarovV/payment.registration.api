using System;
using System.Collections.Generic;

namespace Payment.Registration.App.DTOs
{
    public class PaymentFormDto
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public TypeDto Type { get; set; }

        public ApplicantDto Applicant { get; set; }

        public IEnumerable<PaymentPositionDto> Items { get; set; }
    }
}