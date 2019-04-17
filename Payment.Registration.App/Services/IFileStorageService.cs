using System.IO;
using System.Threading.Tasks;

namespace Payment.Registration.App.Services
{
    public interface IFileStorageService
    {
        Task Save(MemoryStream memoryStream, string wayToFile);

        Task Load(MemoryStream memoryStream, string wayToFile);

        Task Delete(string wayToFile);
    }
}