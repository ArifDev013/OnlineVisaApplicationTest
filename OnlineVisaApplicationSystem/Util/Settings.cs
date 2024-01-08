using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Settings
    {
        public string Server { get; set; }

        public string Db { get; set; } = "Master";
        public int DbId { get; set; }



        //public string Uid => "Inthenameofgod_l";

        //public string Password => "Inthenameofgod@313):";
        
        public string Uid =>  "sa";

        public string Password => "mssql123";

        public bool IsServer { get; set; } = true;


    }
}
