using System.Collections.Generic;

namespace Payment.Registration.App.DTOs
{
    public class PaymentFormSaveDto
    {
        public ApplicantSaveDto Applicant { get; set; }

        public TypeDto Type { get; set; }

        public IEnumerable<PaymentPositionSaveDto> Items { get; set; }
    }
}