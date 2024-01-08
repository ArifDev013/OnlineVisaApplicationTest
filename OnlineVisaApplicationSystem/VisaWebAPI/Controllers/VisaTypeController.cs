using DataModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Services;
using System;
using Utils.Models;
using VisaWebAPI.JWT;

namespace VisaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisaTypeController : CrudBusinessAPIBase<VisaType>
    {

        private readonly IVisaTypeService service;
        public VisaTypeController(IVisaTypeService _service) : base(_service)
        {
            this.service = _service;
        }
        [EnableCors("MyPolicy")]
        [HttpGet("GetList")]
        public virtual IActionResult GetList()
        {
            try
            {
                
                    return Ok( service.GetList() );
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel() { Success = false, ResultSet = ex.Message });
            }
        }
    }
}
