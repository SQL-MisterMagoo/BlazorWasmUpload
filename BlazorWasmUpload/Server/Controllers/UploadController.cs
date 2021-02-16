using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmUpload.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IHostEnvironment hostEnvironment;

        public UploadController(IHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post(IFormFile file)
        {
            Console.WriteLine(file.FileName);
            Guid guid = Guid.NewGuid();
            using (var ms = new FileStream(Path.Combine(hostEnvironment.ContentRootPath,"uploads",file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(ms);
                await ms.FlushAsync();
                ms.Close();
            }
            return Ok($"Uploaded : {file.FileName}");
        }
    }
}
