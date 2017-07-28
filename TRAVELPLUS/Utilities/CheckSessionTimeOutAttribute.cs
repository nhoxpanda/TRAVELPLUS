using CRM.Core;
using CRM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace TRAVELPLUS.Utilities
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class CheckSessionTimeOutAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            
            if (HttpContext.Current.Session["userLogin"] == null)
            {
                AuthenticationManager.SignOut();
                //FormsAuthentication.SignOut();
                filterContext.Result = new RedirectResult("~/");
                return;
            }
            else
            {
                var user = HttpContext.Current.Session["userLogin"] as tbl_Staff;
                var db = new DataContext();
                if (user.TokenKey != null && user.TokenKey != db.tbl_Staff.SingleOrDefault(x => x.Id == user.Id).TokenKey)
                {
                    AuthenticationManager.SignOut();
                    //FormsAuthentication.SignOut();
                    filterContext.Result = new RedirectResult("~/Account/Login?ReturnUrl=%2F&message=Tài khoản của bạn đã được đăng nhập ở một máy khác");
                    return;
                }
                //else
                //{
                //    filterContext.Result = new RedirectResult("~/");
                //    return;
                //}
            }
            base.OnActionExecuting(filterContext);
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                return HttpContextExtensions.GetOwinContext(ctx).Authentication;
            }
        }
    }
}