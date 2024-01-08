using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils.Enums;

namespace DataModel
{
    [Table("VisaApplicants")]
    public class VisaApplicant

    {
        [Key]
        public long VisaApplicantID { get; set; }
        [Required]
        [StringLength(100)]
        public string ApplicantName { get; set; }
        [Required]
        [StringLength(100)]
        public string Surename { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [StringLength(200)]
        public string Address1 { get; set; }

        [StringLength(200)]
        public string Address2 { get; set; } = null;
        [Required]
        [StringLength(200)]
        public string City { get; set; }
        [Required]
        [StringLength(200)]
        public string State { get; set; }
        [Required]
        [StringLength(200)]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [Required]
        [StringLength(30)]
        public string Nationality { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        [StringLength(20)]
        public string MobileNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(10)]
        public string PassportNumber { get; set; }
        [Required]
        public DateTime PassportExpiryDate { get; set; }

        public MaritalStatus MaritalStatus { get; set; } = 0;
         
    }
}
