using System.IO;
using System.Threading.Tasks;

namespace Payment.Registration.App.Services
{
    public class FileAppService
    {
        private readonly IFileStorageService fileStorageService;

        public FileAppService(IFileStorageService fileStorageService)
        {
            this.fileStorageService = fileStorageService;
        }
        
        public async Task<MemoryStream> Get(string file)
        {
            var memoryStream = new MemoryStream();
            await fileStorageService.Load(memoryStream, file);
            return memoryStream;
        }
    }
}