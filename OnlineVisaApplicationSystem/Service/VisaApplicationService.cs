using DataModel;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class VisaApplicationService : GenericCrudService<VisaApplication>, IVisaApplicationService
    {
        public VisaApplicationService(IUnitOfWork unitOfWork) : base(unitOfWork, x => x.ApplicationID)
        {
        }
    }
}
