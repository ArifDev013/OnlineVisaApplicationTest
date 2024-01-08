using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IGenericBusinessService<TEntity> where TEntity : class
    {
        IGenericRepository<TEntity> Repository { get; }
    }
}
