using DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services

{
    public class DocumentService : GenericCrudService<Documents>, IDocumentService
    {
        public DocumentService(IUnitOfWork unitOfWork) : base(unitOfWork, x => x.DocumentID)
        {
        }
    }
}
