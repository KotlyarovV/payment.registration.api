using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment.Registration.App.Services;

namespace Payment.Registration.API.Controllers
{
    [Route("files")]
    public class FileController : Controller
    {
        private readonly FileAppService fileAppService;

        public FileController(FileAppService fileAppService)
        {
            this.fileAppService = fileAppService;
        }

        [HttpGet]
        [Route("{fileName}")]
        public async Task<IActionResult> Get(string fileName)
        {
            var memoryStream = await fileAppService.Get(fileName);
            return new FileContentResult(memoryStream.ToArray(), "application/octet-stream")
            {
                FileDownloadName = fileName
            };
        }
    }
}