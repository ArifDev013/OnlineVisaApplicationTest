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
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CrudBusinessAPIBase<User>
    {

        private readonly IUserService service;
        private readonly IJwtUtils jwtUtils;

        public UserController(IUserService service, IJwtUtils jwtUtils) : base(service)
        {
            this.service = service;
            this.jwtUtils = jwtUtils;
        }

        [EnableCors("MyPolicy")]
        [HttpGet("Authenticate/{username}/{password}")]
        public virtual IActionResult Authenticate(string username, string password)
        {
            try
            {
                var res = service.GetMany(x => x.Username == username && x.Password == password).FirstOrDefault();
                if (res == null)
                    return Ok(new ResponseModel() { ResultSet ="Username and password not matching",Success=false });
                else
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel() { Success = false, ResultSet = ex.Message });
            }
        }
    }
}
