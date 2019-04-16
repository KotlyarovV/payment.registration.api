using System;

namespace Payment.Registration.App.DTOs
{
    public class ApplicantUpdateDto
    {
        public Guid? Id { get; set; }
        
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        public string Surname { get; set; }
    }
}