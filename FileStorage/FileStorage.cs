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
            using (FileStream file = new FileStream(wayToFile, FileMode.Create, FileAccess.Write))
                await memoryStream.CopyToAsync(file);
        }

        public async Task Load(MemoryStream memoryStream, string wayToFile)
        {
            using (FileStream file = new FileStream(wayToFile, FileMode.Open, FileAccess.Read))
                file.CopyTo(memoryStream);
        }

        public Task Delete(string wayToFile)
        {
            return Task.Factory.StartNew(() =>
            {
                if(File.Exists(wayToFile))
                {
                    File.Delete(wayToFile);
                }
            });
        }
    }
}