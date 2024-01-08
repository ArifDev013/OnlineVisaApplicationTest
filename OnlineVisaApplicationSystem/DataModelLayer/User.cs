using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("User")]
    public class User
    {
        [Key]
        public long UserID { get; set; }
        [StringLength(50)]
        [Required]
        [Index(IsUnique = true)]
        public string Username { get; set; }
        [StringLength(50)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; } 
        public string Password { get; set; }
    }
}
