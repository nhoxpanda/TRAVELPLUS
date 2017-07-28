using CRM.Core;
using CRM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRAVELPLUS.Utilities;

namespace TRAVELPLUS.Controllers.Marketing
{
    [Authorize]
    public class MailSendingController : BaseController
    {
        // GET: MailSending

        #region Init

        private IGenericRepository<tbl_MailConfig> _mailConfigRepository;
        private IGenericRepository<tbl_Staff> _staffRepository;
        private DataContext _db;

        public MailSendingController(
            IGenericRepository<tbl_MailConfig> mailConfigRepository,
            IGenericRepository<tbl_Staff> staffRepository,
            IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._mailConfigRepository = mailConfigRepository;
            this._staffRepository = staffRepository;
            _db = new DataContext();
        }
        #endregion


        //[CheckSessionTimeOut]
        public ActionResult Index()
        {
            return View();
        }
    }
}