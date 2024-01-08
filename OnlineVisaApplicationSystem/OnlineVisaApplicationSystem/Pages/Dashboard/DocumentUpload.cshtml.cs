using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Reflection.Metadata;
using System.Text;
using Utils;
using Utils.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace OnlineVisaApplicationSystem.Pages.Dashboard
{
    public class DocumentUploadModel : PageModel
    {
        [BindProperty]
        public DocumentsModel PassportDocuments { get; set; }
        [BindProperty]
        public DocumentsModel PhotoDocuments { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            fileUpload(PassportDocuments);

            fileUpload(PhotoDocuments);
        }
        private bool fileUpload(DocumentsModel Documents)
        {
            if (Documents != null && Documents.File != null)
            {


                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var fileBytes = getBytes(Documents.File);
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("multipart/form-data"));

                        MultipartFormDataContent form = new MultipartFormDataContent();
                        form.Add(new StringContent(Documents.File.Name), "FileName");
                        form.Add(new StringContent("PassportCopy"), "FileType");
                        form.Add(new StringContent(Documents.File.ContentType), "FileMemeType");
                        form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), "passportcopy", Documents.File.Name);

                        var response = client.PutAsync(GlobalData.baseUrl + "api/User/Update", form).Result;

                        if (response.IsSuccessStatusCode)
                        {

                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return true;
            }
            else return false;
        }
        private byte[] getBytes(IFormFile File)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                File.CopyTo(ms);
                fileBytes = ms.ToArray();
                var file = Convert.ToBase64String(fileBytes);
            }
            return fileBytes;   
        }
    }
}
