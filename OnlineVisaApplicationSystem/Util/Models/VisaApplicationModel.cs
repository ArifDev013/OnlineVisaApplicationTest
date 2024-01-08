using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils.Enums;

namespace Utils.Models
{
    public class VisaApplicationModel
    {
        public long ApplicationID { get; set; } 
        public long UserID { get; set; }
        public virtual UserModel User { get; set; } 
        public long VisaApplicantID { get; set; }
        public virtual VisaApplicantModel Applicant { get; set; } 
        public long VisaTypeID { get; set; }
        public virtual VisaTypeModel VisaType { get; set; } 
        public DateTime AppliedDate { get; set; }=DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCharge { get; set; } = 0.00;
        public virtual ICollection<DocumentsModel> Documents { get; set; }
        public VisaStatus Status { get; set; }=VisaStatus.Processing;
    }
}
