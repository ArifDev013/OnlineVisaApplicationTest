using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Models
{
    public class UserModel
    {
        public long UserID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; } = "User";
        public string Password { get; set; }
    }
}
