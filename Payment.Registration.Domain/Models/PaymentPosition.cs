using System;
using System.Collections.Generic;

namespace Payment.Registration.Domain.Models
{
    public class PaymentPosition
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public decimal Sum { get; set; }

        public int SortOrder { get; set; }

        public  ICollection<File> Files { get; set; }
    }
}