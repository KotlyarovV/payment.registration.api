using System.Collections.Generic;

namespace Payment.Registration.App.DTOs
{
    public class PaymentFormUpdateDto
    {
        public ApplicantUpdateDto Applicant { get; set; }

        public TypeDto Type { get; set; }

        public IEnumerable<PaymentPositionUpdateDto> Items { get; set; }
    }
}