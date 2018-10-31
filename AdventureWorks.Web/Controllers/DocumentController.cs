using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Web.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IAzureService _azureService;
        private readonly ILogger _logger;


        public DocumentController(IAzureService azureService, ILogger<DocumentController> logger)
        {
            _azureService = azureService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            _logger.LogInformation($"{size} bytes to upload...");

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        _logger.LogInformation($"Uploading {formFile.FileName}");
                        await formFile.CopyToAsync(stream);
                        stream.Position = 0;
                        var uri = await _azureService.AddBlobAsync(stream, formFile.FileName);
                        _logger.LogInformation($"{formFile.FileName} saved: {uri}");

                        //var fileMetadata = new FileMetadata
                        //{
                        //    Filename = formFile.FileName,
                        //    Size = formFile.Length,
                        //    UploadedDeate = DateTime.UtcNow,
                        //    StoringUri = uri
                        //};

                        //await _azureService.AddToQueueAsync(fileMetadata);
                        //_logger.LogInformation($"{formFile.FileName} added to queue");
                    }
                }
            }
            _logger.LogInformation($"{size} uploaded.");
            return Ok(new { count = files.Count, size });
        }
    }
}