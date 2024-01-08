using DataModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Linq;
using System;
using Utils.Models;
using Service;
using VisaWebAPI.JWT;

namespace VisaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class VisaApplicantController : CrudBusinessAPIBase<VisaApplicant>
    {

        private readonly IVisaApplicantService service;
        public VisaApplicantController(IVisaApplicantService service) : base(service)
        {
            this.service = service;
        }
      
    }
}
