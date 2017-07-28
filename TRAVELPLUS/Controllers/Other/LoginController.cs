using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRAVELPLUS.Utilities;

namespace TRAVELPLUS.Controllers.Other
{
    public class LoginController : Controller
    {
        // GET: Login

        //[CheckSessionTimeOut]
        public ActionResult Index()
        {
            return View();
        }
    }
}