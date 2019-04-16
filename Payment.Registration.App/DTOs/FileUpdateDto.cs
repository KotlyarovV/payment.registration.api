using System;

namespace Payment.Registration.App.DTOs
{
    public class FileUpdateDto
    {
        public Guid? Id { get; set; }

        public FileSaveDto File { get; set; }
    }
}