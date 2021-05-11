using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


/*namespace AREA1.Tool
{
    public class TestController : Controller
    {
        private IWebHostEnvironment environment;

        public TestController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public IActionResult Upload()
        {
            return View();
        }
        public async Task<IActionResult> Upload(ICollection<IFormFile> fileCollection)
        {
            var uploadDirectoryPath = Path.Combine(this.environment.WebRootPath, "upload"); foreach (IFormFile formFile in fileCollection)
            {
                if (formFile.Length > 0)
                {
                    string fileName = Path.GetFileName
                        (
                        ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Value
                        );
                    using (FileStream stream = new FileStream(Path.Combine(uploadDirectoryPath, fileName), FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return View();
        }
        public FileResult Download(string fileName = "Test.txt")
        {
            byte[] fileByteArray = System.IO.File.ReadAllBytes
                (
                Path.Combine(this.environment.WebRootPath, "upload", fileName)
                );
            return File(fileByteArray, "application/octet-stream", fileName);
        }




    }            
}
*/