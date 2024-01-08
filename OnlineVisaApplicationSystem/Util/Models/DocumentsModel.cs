using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Models
{
    public class DocumentsModel
    {
        public long DocumentID { get; set; }
        public long ApplicationID { get; set; }
        public IFormFile File { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileMemeType { get; set; }  //pdf,jpg,png
        public string FilePath { get; set; }
    }
}
