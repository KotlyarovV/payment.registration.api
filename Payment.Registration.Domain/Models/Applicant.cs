using System;

namespace Payment.Registration.Domain.Models
{
    public class Applicant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }
    }
}