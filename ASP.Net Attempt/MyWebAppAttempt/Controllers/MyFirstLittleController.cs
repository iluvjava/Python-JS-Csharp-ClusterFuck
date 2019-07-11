using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


/// <summary>
/// When each new controller is created, it created a matching view templates too. 
/// </summary>
namespace MyWebAppAttempt.Controllers
{
    public class MyFirstLittleController : Controller
    {
        // GET: MyFirstLittle
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListStuff()
        {
            return View();
        }
    }
}