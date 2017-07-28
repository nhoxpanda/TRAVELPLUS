using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRAVELPLUS.Utilities;

namespace TRAVELPLUS.Controllers
{
    public class PromotionManageController : Controller
    {
        //
        // GET: /PromotionManage/


        //[CheckSessionTimeOut]
        public ActionResult Index()
        {
            return View();
        }

    }
}
