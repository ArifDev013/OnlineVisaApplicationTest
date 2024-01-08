using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Enums 
    {
        public enum VisaStatus{
        Processing=0,
        Active=1,
        Canceled=2,
        Approved=3,
        Rejected=4,
        Expired=6
        }
        public enum MaritalStatus
        {
            Single=0,
            Married=1,
            Divorced=2
        }
        public enum Gender
        {
            Male = 0,
            Female = 1,
            Other = 2
        }
        public enum FileType
        {
            PassportSizePhoto = 0,
            PassportCopy = 1,
            OtherDocument = 2
        }
    }
}
