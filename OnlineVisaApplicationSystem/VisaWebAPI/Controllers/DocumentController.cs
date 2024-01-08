using DataModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Linq;
using System;
using Utils.Models;
using Service;
using VisaWebAPI.JWT;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace VisaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class DocumentController : CrudBusinessAPIBase<Documents>
    {

        private readonly IDocumentService service;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DocumentController(IDocumentService _service, IWebHostEnvironment _webHostEnvironment) : base(_service)
        {
            this.service = _service;
            this.webHostEnvironment = _webHostEnvironment;
        }
        [EnableCors("MyPolicy")]
        [HttpPost("Save")]
        public IActionResult Save([FromForm]DocumentsModel model )
        {
            try
            {
                Documents documents = new Documents();
                if (model.File != null && model.File.Length > 0)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = $"{Guid.NewGuid()}_{model.File.FileName}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.File.CopyTo(fileStream);
                    }
                    documents.ApplicationID = model.ApplicationID;
                    documents.DocumentID = model.ApplicationID;
                    documents.FileName = uniqueFileName;
                    documents.ApplicationID = model.ApplicationID;
                    documents.FilePath = filePath;
                    documents.FileMemeType=model.FileMemeType;

                }
                return Ok(service.Add(documents));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
