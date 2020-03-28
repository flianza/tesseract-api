using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Tesseract.Lib;

namespace Tesseract.Api.Controllers
{
    [ApiController]
    public class TesseractController : ControllerBase
    {
        private readonly ITesseract tesseract;
        public TesseractController(ITesseract tesseract)
        {
            this.tesseract = tesseract;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Dummy()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return this.Ok(new
            {
                assembly = assembly.GetName().Name,
                version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
                os = RuntimeInformation.OSDescription,
                osArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                processArchitecture = RuntimeInformation.ProcessArchitecture.ToString()
            });
        }

        [HttpPost]
        [Route("tesseract")]
        public IActionResult Tesseract([FromBody] string base64Pdf)
        {
            byte[] data = Convert.FromBase64String(base64Pdf);

            TesseractOptions options = new TesseractOptions()
            {
                Languages = new[] { Language.Spanish }
            };
            var result = this.tesseract.PdfToText(data, options);
            
            return this.Ok(result);
        }
    }
}
