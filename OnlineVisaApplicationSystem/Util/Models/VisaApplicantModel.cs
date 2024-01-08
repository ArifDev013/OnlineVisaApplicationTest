using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils.Enums;

namespace Utils.Models
{
    public class VisaApplicantModel
    {
        public long VisaApplicantID { get; set; }
        public string ApplicantName { get; set; }
        public string Surename { get; set; } = "";
        public Gender Gender { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; } = null;
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; } = "";
        public string Nationality { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public MaritalStatus MaritalStatus { get; set; } = 0;
    }
}
