using System;
using System.IO;
using System.Threading.Tasks;
using Payment.Registration.App.Services;

namespace FileStorage
{
    public class FileStorage : IFileStorageService
    {
        public async Task Save(MemoryStream memoryStream, string wayToFile)
        {
            using (FileStream file = new FileStream(wayToFile, FileMode.Open, FileAccess.Read))
                await file.CopyToAsync(memoryStream);
        }
    }
}