using DataModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service;
using Services;
using System.Data;
using System.Linq;
using Utils.Models;

namespace VisaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisaApplicationController : CrudBusinessAPIBase<VisaApplication>
    {

        private readonly IVisaApplicationService service;
        public VisaApplicationController(IVisaApplicationService service) : base(service)
        {
            this.service = service;
        }

        [EnableCors("MyPolicy")]
        [HttpPost("GetList/{Id}")]
        public virtual IActionResult GetList(int Id)
        {
            return Ok(service.GetAllAsQueryable().Where(x => x.UserID == Id).ToList());

        }
    }
    }
