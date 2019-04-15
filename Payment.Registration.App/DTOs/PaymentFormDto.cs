using System;
using System.Collections.Generic;

namespace Payment.Registration.App.DTOs
{
    public enum TypeDto
    {
        Type1,
        Type2
    }
    
    public class ApplicantDto
    {
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        public string Surname { get; set; }
    }
    
    public class PaymentPositionDto
    {
        public int SortOrder { get; set; }
        
        public string Comment { get; set; }

        public decimal Sum { get; set; }

        public IEnumerable<FileDto> Files { get; set; }
    }

    public class FileDto
    {
        public string WayToFile { get; set; }
    }

    public class ApplicantSaveDto
    {
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        public string Surname { get; set; }
    }

    public class FileSaveDto
    {
        public string FileInBase64 { get; set; }
    }

    public class PaymentPositionSaveDto
    {
        public int SortOrder { get; set; }
        
        public string Comment { get; set; }

        public decimal Sum { get; set; }

        public IEnumerable<FileSaveDto> Files { get; set; }
    }
    
    public class PaymentFormSaveDto
    {
        public ApplicantSaveDto Applicant { get; set; }

        public TypeDto Type { get; set; }

        public IEnumerable<PaymentPositionSaveDto> Items { get; set; }
    }
    
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