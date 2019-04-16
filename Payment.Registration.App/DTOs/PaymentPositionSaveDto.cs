using System.Collections.Generic;

namespace Payment.Registration.App.DTOs
{
    public class PaymentPositionSaveDto
    {
        public int SortOrder { get; set; }
        
        public string Comment { get; set; }

        public decimal Sum { get; set; }

        public IEnumerable<FileSaveDto> Files { get; set; }
    }
}