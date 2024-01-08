using DataModel;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class VisaApplicantService : GenericCrudService<VisaApplicant>, IVisaApplicantService
    {
        public VisaApplicantService(IUnitOfWork unitOfWork) : base(unitOfWork, x => x.VisaApplicantID)
        {
        }
    }
}
