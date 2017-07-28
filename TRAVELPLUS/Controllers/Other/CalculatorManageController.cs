using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRAVELPLUS.Utilities;

namespace TRAVELPLUS.Controllers.Other
{
    [Authorize]
    public class CalculatorManageController : Controller
    {
        // GET: CalculatorManage

        //[CheckSessionTimeOut]
        public ActionResult Index()
        {
            return View();
        }
    }
}