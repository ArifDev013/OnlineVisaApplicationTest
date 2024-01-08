using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Documents")]
    public class Documents
    {
        [Key]
        public long DocumentID { get; set; }
        public long ApplicationID { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileMemeType { get; set; }  //pdf,jpg,png
        public string FilePath { get; set; }

    }
}
