using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRAVELPLUS.Utilities;

namespace TRAVELPLUS.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        //[CheckSessionTimeOut]
        public ActionResult Index()
        {
            return View();
        }
    }
}