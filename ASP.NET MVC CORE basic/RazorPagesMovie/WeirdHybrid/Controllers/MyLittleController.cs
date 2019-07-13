using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeirdHybrid.Controllers
{

    /// <summary>
    /// This is just my API controller essentially. 
    /// </summary>
    [ApiController]
    public class MyLittleController : ControllerBase
    {
        [Route("api/mylittlecontroller")]
        [HttpGet]
        public String Get(string arg)
        {
            
            return $"The value for arg: {arg}";
        }




    }
}