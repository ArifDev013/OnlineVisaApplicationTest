using DataModel;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services

{
    public class VisaTypeService : GenericCrudService<VisaType>, IVisaTypeService
    {
        public VisaTypeService(IUnitOfWork unitOfWork) : base(unitOfWork, x => x.VisaTypeID)
        {
        }

        public dynamic GetList()
        { 
            try
            {
              return Repository.GetAllAsQueryable().Select(x=>new { x.VisaTypeID, x.VisaName }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
