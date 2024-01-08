using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataModel
{
    /// <summary>
    /// Base class of All Business Service
    /// </summary>
    public class BusinessServiceBase
    {
        private readonly IUnitOfWork unitOfWork;

        public BusinessServiceBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


      //  private IGenericRepository<Settings> ConfigRepository => unitOfWork.GetRepository<Settings>(x => x.Id);
        /// <summary>
        /// Returns Repository of type TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type of Entity</typeparam>
        /// <returns>Repository</returns>
        public IGenericRepository<TEntity> Repository<TEntity>(Expression<Func<TEntity, object>> idProperty) where TEntity : class => unitOfWork.GetRepository<TEntity>(idProperty);


        /// <summary>
        /// Get Configuration Param
        /// </summary>
        /// <param name="id">Id of Config</param>
        /// <returns></returns>
       // public string GetConfig(string id) => ConfigRepository.Get(x => x.Id == id)?.Value;/*  GetByID(id)?.Value;*/

        /// <summary>
        /// Set a Configuration
        /// </summary>
        /// <param name="id">Id of the configuration</param>
        /// <param name="value">Value of the configuration</param>
        //public void SetConfig(string id, string value)
        //{
        //    SaveUnderTransaction(() =>
        //    {
        //        if (ConfigRepository.GetByID(id) is Settings config)
        //            ConfigRepository.Delete(config);
        //        ConfigRepository.Insert(new Settings { Id = id, Value = value });
        //    });
        //}

        /// <summary>
        /// Save Under a transaction
        /// </summary>
        /// <param name="action">action to perform modification</param>
        protected void SaveUnderTransaction(Action action)
        {
            using (var scope = new TransactionScope())
            {
              
                
                action();
                unitOfWork.Save();
                scope.Complete();
            }
        }
    }
}
