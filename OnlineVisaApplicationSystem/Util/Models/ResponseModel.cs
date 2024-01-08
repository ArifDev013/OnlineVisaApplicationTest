using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; } = true;
        public dynamic ResultSet { get; set; }
    }
}
