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
    [Table("VisaApplication")]
    public class VisaApplication
    {
        [Key]
        public long ApplicationID { get; set; }
        [Required]
        [ForeignKey("User")]
        public long UserID { get; set; }
        public virtual User User { get; set; }
        public string RefNumber { get; set; } = "";
        [Required]
        public long VisaApplicantID { get; set; }
        public virtual VisaApplicant Applicant { get; set; }
        [Required]
        public long VisaTypeID { get; set; }
        public virtual VisaType VisaType { get; set; }
        [Required]
        public DateTime AppliedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCharge { get; set; }
        public virtual ICollection<Documents> Documents { get; set; }
        public VisaStatus Status { get; set; }

    }
}
