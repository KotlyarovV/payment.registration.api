using System;
using System.Collections.Generic;

namespace Payment.Registration.App.DTOs
{
    public class PaymentPositionDto
    {
        public Guid Id { get; set; }
        
        public int SortOrder { get; set; }
        
        public string Comment { get; set; }

        public decimal Sum { get; set; }

        public IEnumerable<FileDto> Files { get; set; }
    }
}