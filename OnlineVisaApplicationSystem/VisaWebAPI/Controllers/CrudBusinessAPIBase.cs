
using DataModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace VisaWebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    public abstract class CrudBusinessAPIBase<TEntity> : BusinessAPIBase<TEntity> where TEntity : class
    {
        

        public CrudBusinessAPIBase(IGenericCrudService<TEntity> BusinessServiceBase) : base(BusinessServiceBase)
        {
        }

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="value">Entity to add</param>
        /// <returns></returns>
        [EnableCors("MyPolicy")]
        [HttpPost("Add")]
        public virtual IActionResult Add([FromBody]TEntity value) => DoOperation(() =>  BusinessService.Add(value));

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="value">Entity to update</param>
        /// <returns></returns>
        [EnableCors("MyPolicy")]
        [HttpPut("Update")]
        public virtual IActionResult Update([FromBody]TEntity value) => DoOperation(() => BusinessService.Update(value));

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">entity to delete</param>
        /// <returns></returns>
        //[EnableCors("MyPolicy")]
        //[HttpDelete("{id}")]
        //public virtual IActionResult Delete(string id) => DoOperation(() => BusinessService.Delete(BusinessService.GetById(id)));
        
    }
}
