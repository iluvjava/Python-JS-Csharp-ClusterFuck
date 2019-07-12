using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeirdHybrid.Controllers
{
    [ApiController]
    public class MyLittleController : ControllerBase
    {
        [Route("api/mylittlecontroller")]
        [HttpGet]
        public String Get( string arg)
        {
            
            return $"This is the query string: {arg}";
        }


    }
}