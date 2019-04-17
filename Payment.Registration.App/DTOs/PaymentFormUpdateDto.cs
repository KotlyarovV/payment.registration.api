using System.Collections.Generic;

namespace Payment.Registration.App.DTOs
{
    public class PaymentFormUpdateDto
    {
        public ApplicantSaveDto Applicant { get; set; }

        public TypeDto Type { get; set; }

        public IEnumerable<PaymentPositionUpdateDto> Items { get; set; }
    }
}