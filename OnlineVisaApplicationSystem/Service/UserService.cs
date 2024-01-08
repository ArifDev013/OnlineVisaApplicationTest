using DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services

{
    public class UserService : GenericCrudService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork, x => x.UserID)
        {
        }
    }
}
