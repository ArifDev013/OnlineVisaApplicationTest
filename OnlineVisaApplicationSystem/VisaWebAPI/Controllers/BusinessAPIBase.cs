
using DataModel;
using Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Utils.Models;

namespace VisaWebAPI.Controllers
{
    public abstract class BusinessAPIBase<TEntity> : Controller where TEntity : class
    {
        private readonly IGenericCrudService<TEntity> businsessService;

        public BusinessAPIBase(IGenericCrudService<TEntity> businsessService)
        {
            this.businsessService = businsessService;
        }

        /// <summary>
        /// Get Business Service object
        /// </summary>
        protected IGenericCrudService<TEntity> BusinessService => businsessService;

        /// <summary>
        /// Gel all entities
        /// </summary>
        /// <returns>All entities</returns>
        [HttpGet]
        public virtual IActionResult Get() => Ok(new ResponseModel() { ResultSet = BusinessService.GetAll().ToList() } );

        /// <summary>
        /// Get an entity by Id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity</returns>
        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            var stock = BusinessService.GetById(id);
            if (stock == null)
                return Ok(new ResponseModel() { Success = false, ResultSet = id }); ;
            return Ok(new ResponseModel() { ResultSet = stock });
        }

        /// <summary>
        /// Encapsulate general processing
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected IActionResult DoOperation(Func<TEntity>  action)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  
                    
                    return Ok(new ResponseModel() { ResultSet = action()});
                }
                else
                    return Ok(new ResponseModel() { ResultSet = ModelState, Success = false });
            }
            catch (Exception ex)
            {
             return Ok(new ResponseModel() { ResultSet = ex.Message, Success = false });
            }
        }

        /// <summary>
        /// Encapsulate general processing
        /// </summary>
        /// <param name="action"></param>
        /// <param name="resultAction"></param>
        /// <returns></returns>
        protected IActionResult DoOperation<TResult>(Action action, Func<TResult> resultAction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    action();
                    return Ok(resultAction());
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
