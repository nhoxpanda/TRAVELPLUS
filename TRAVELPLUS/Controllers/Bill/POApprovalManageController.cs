using CRM.Core;
using CRM.Infrastructure;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TRAVELPLUS.Models;
using TRAVELPLUS.Utilities;

namespace TRAVELPLUS.Controllers.Bill
{
    [Authorize]
    public class POApprovalManageController : BaseController
    {
        // GET: POApprovalManage

        #region Init

        private IGenericRepository<tbl_Staff> _staffRepository;
        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private IGenericRepository<tbl_ActionData> _actionDataRepository;
        private IGenericRepository<tbl_AccessData> _accessDataRepository;
        private IGenericRepository<tbl_POApproval> _poApprovalRepository;
        private IGenericRepository<tbl_POApprovalDetail> _poApprovalDetailRepository;
        private IGenericRepository<tbl_POApprovalHistory> _poApprovalHistoryRepository;
        private DataContext _db;
        
        public POApprovalManageController(IGenericRepository<tbl_Dictionary> dictionaryRepository,
            IGenericRepository<tbl_Staff> staffRepository,
            IGenericRepository<tbl_ActionData> actionDataRepository,
            IGenericRepository<tbl_POApproval> poApprovalRepository,
            IGenericRepository<tbl_POApprovalDetail> poApprovalDetailRepository,
            IGenericRepository<tbl_POApprovalHistory> poApprovalHistoryRepository,
            IGenericRepository<tbl_AccessData> accessDataRepository,
            IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._staffRepository = staffRepository;
            this._accessDataRepository = accessDataRepository;
            this._actionDataRepository = actionDataRepository;
            this._dictionaryRepository = dictionaryRepository;
            this._poApprovalRepository = poApprovalRepository;
            this._poApprovalDetailRepository = poApprovalDetailRepository;
            this._poApprovalHistoryRepository = poApprovalHistoryRepository;
            _db = new DataContext();
        }

        #endregion

        #region Partial Info ApprovalPO

        [ChildActionOnly]
        public ActionResult _Partial_InfoApprovalPO(int id)
        {
            var model = new POApprovalViewModel()
            {
                tbl_POApproval = _poApprovalRepository.FindId(id),
                tbl_POApprovalDetail = _poApprovalDetailRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToList()
            };
            return PartialView("_Partial_InfoApprovalPO", model);
        }

        #endregion

        #region Lỗi phân quyền

        public ActionResult Error()
        {
            return View();
        }

        #endregion

        #region Cấp độ 1
        public async Task<ActionResult> Level1(int id, FormCollection fc)
        {
            var check = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 1);
            string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            if (check != null)
            {
                ViewBag.ID = id;
                ViewBag.URL = Request.Url.AbsoluteUri;

                // button chấp nhận, từ chối
                if (Request["btnAccept"] != null)
                {
                    check.Times++;
                    check.IsAccept = true;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // update ApprovalForm
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    item.Requester = check.tbl_Staff.FullName;
                    item.StatusId++;
                    await _poApprovalRepository.Update(item);
                    // send mail next level
                    string email = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 2).tbl_Staff.Email;
                    string body = "<p>Nhân viên: " + item.Requester + " đã duyệt PO cấp độ 1</p>"
                                + "<p>Vui lòng click vào <a href='"+ domain + "/duyet-po-cap-do-2/" + item.Id + "'>đây</a> để duyệt cấp độ tiếp theo</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", email, "[TRAVELPLUS] Thông báo duyệt PO cấp độ 2", body);
                }
                if (Request["btnCancel"] != null)
                {
                    check.Times++;
                    check.IsAccept = false;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // send mail to createPO
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    string body = "<p>Nhân viên: " + check.tbl_Staff.FullName + " đã từ chối duyệt PO cấp độ 1</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", item.tbl_Staff.Email, "[TRAVELPLUS] Thông báo từ chối duyệt PO cấp độ 1", body);
                }

                // disabled button chấp nhận, từ chối
                if (check.IsAccept == true)
                {
                    ViewBag.Disabled = "disabled";
                }
                else
                {
                    ViewBag.Disabled = "";
                }
                ViewBag.History = _poApprovalHistoryRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToList();
                return View(await _poApprovalRepository.GetById(id));
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        #endregion

        #region Cấp độ 2
        public async Task<ActionResult> Level2(int id, FormCollection fc)
        {
            var check = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 2);
            string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            if (check != null)
            {
                ViewBag.ID = id;
                ViewBag.URL = Request.Url.AbsoluteUri;

                // button chấp nhận, từ chối
                if (Request["btnAccept"] != null)
                {
                    check.Times++;
                    check.IsAccept = true;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // update ApprovalForm
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    item.Purchasing = check.tbl_Staff.FullName;
                    item.StatusId++;
                    await _poApprovalRepository.Update(item);
                    // send mail next level
                    string email = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 3).tbl_Staff.Email;
                    string body = "<p>Nhân viên: " + item.Purchasing + " đã duyệt PO cấp độ 2</p>"
                                + "<p>Vui lòng click vào <a href='" + domain + "/duyet-po-cap-do-3/" + item.Id + "'>đây</a> để duyệt cấp độ tiếp theo</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", email, "[TRAVELPLUS] Thông báo duyệt PO cấp độ 3", body);
                }
                if (Request["btnCancel"] != null)
                {
                    check.Times++;
                    check.IsAccept = false;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // send mail to createPO
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    string body = "<p>Nhân viên: " + check.tbl_Staff.FullName + " đã từ chối duyệt PO cấp độ 2</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", item.tbl_Staff.Email, "[TRAVELPLUS] Thông báo từ chối duyệt PO cấp độ 2", body);
                }

                // disabled button chấp nhận, từ chối
                if (check.IsAccept == true)
                {
                    ViewBag.Disabled = "disabled";
                }
                else
                {
                    ViewBag.Disabled = "";
                }
                ViewBag.History = _poApprovalHistoryRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToList();
                return View(await _poApprovalRepository.GetById(id));
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        #endregion

        #region Cấp độ 3
        public async Task<ActionResult> Level3(int id, FormCollection fc)
        {
            var check = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 3);
            string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            if (check != null)
            {
                ViewBag.ID = id;
                ViewBag.URL = Request.Url.AbsoluteUri;

                // button chấp nhận, từ chối
                if (Request["btnAccept"] != null)
                {
                    check.Times++;
                    check.IsAccept = true;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // update ApprovalForm
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    item.ViceDirector = check.tbl_Staff.FullName;
                    item.StatusId++;
                    await _poApprovalRepository.Update(item);
                    // send mail next level
                    string email = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 4).tbl_Staff.Email;
                    string body = "<p>Nhân viên: " + item.ViceDirector + " đã duyệt PO cấp độ 3</p>"
                                + "<p>Vui lòng click vào <a href='" + domain + "/duyet-po-cap-do-4/" + item.Id + "'>đây</a> để duyệt cấp độ tiếp theo</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", email, "[TRAVELPLUS] Thông báo duyệt PO cấp độ 4", body);
                }
                if (Request["btnCancel"] != null)
                {
                    check.Times++;
                    check.IsAccept = false;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // send mail to createPO
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    string body = "<p>Nhân viên: " + check.tbl_Staff.FullName + " đã từ chối duyệt PO cấp độ 3</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", item.tbl_Staff.Email, "[TRAVELPLUS] Thông báo từ chối duyệt PO cấp độ 3", body);
                }

                // disabled button chấp nhận, từ chối
                if (check.IsAccept == true)
                {
                    ViewBag.Disabled = "disabled";
                }
                else
                {
                    ViewBag.Disabled = "";
                }
                ViewBag.History = _poApprovalHistoryRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToList();
                return View(await _poApprovalRepository.GetById(id));
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        #endregion

        #region Cấp độ 4
        public async Task<ActionResult> Level4(int id, FormCollection fc)
        {
            var check = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 4);
            string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            if (check != null)
            {
                ViewBag.ID = id;
                ViewBag.URL = Request.Url.AbsoluteUri;

                // button chấp nhận, từ chối
                if (Request["btnAccept"] != null)
                {
                    check.Times++;
                    check.IsAccept = true;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // update ApprovalForm
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    item.PayableAccoutant = check.tbl_Staff.FullName;
                    item.StatusId++;
                    await _poApprovalRepository.Update(item);
                    // send mail next level
                    string email = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 5).tbl_Staff.Email;
                    string body = "<p>Nhân viên: " + item.PayableAccoutant + " đã duyệt PO cấp độ 4</p>"
                                + "<p>Vui lòng click vào <a href='" + domain + "/duyet-po-cap-do-5/" + item.Id + "'>đây</a> để duyệt cấp độ tiếp theo</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", email, "[TRAVELPLUS] Thông báo duyệt PO cấp độ 5", body);
                }
                if (Request["btnCancel"] != null)
                {
                    check.Times++;
                    check.IsAccept = false;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // send mail to createPO
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    string body = "<p>Nhân viên: " + check.tbl_Staff.FullName + " đã từ chối duyệt PO cấp độ 4</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", item.tbl_Staff.Email, "[TRAVELPLUS] Thông báo từ chối duyệt PO cấp độ 4", body);
                }

                // disabled button chấp nhận, từ chối
                if (check.IsAccept == true)
                {
                    ViewBag.Disabled = "disabled";
                }
                else
                {
                    ViewBag.Disabled = "";
                }
                ViewBag.History = _poApprovalHistoryRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToList();
                return View(await _poApprovalRepository.GetById(id));
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        #endregion

        #region Cấp độ 5
        public async Task<ActionResult> Level5(int id, FormCollection fc)
        {
            var check = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 5);
            string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            if (check != null)
            {
                ViewBag.ID = id;
                ViewBag.URL = Request.Url.AbsoluteUri;

                // button chấp nhận, từ chối
                if (Request["btnAccept"] != null)
                {
                    check.Times++;
                    check.IsAccept = true;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // update ApprovalForm
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    item.CheifAccountant = check.tbl_Staff.FullName;
                    item.StatusId++;
                    await _poApprovalRepository.Update(item);
                    // send mail next level
                    string email = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 6).tbl_Staff.Email;
                    string body = "<p>Nhân viên: " + item.CheifAccountant + " đã duyệt PO cấp độ 5</p>"
                                + "<p>Vui lòng click vào <a href='" + domain + "/duyet-po-cap-do-6/" + item.Id + "'>đây</a> để duyệt cấp độ tiếp theo</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", email, "[TRAVELPLUS] Thông báo duyệt PO cấp độ 6", body);
                }
                if (Request["btnCancel"] != null)
                {
                    check.Times++;
                    check.IsAccept = false;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // send mail to createPO
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    string body = "<p>Nhân viên: " + check.tbl_Staff.FullName + " đã từ chối duyệt PO cấp độ 5</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", item.tbl_Staff.Email, "[TRAVELPLUS] Thông báo từ chối duyệt PO cấp độ 5", body);
                }

                // disabled button chấp nhận, từ chối
                if (check.IsAccept == true)
                {
                    ViewBag.Disabled = "disabled";
                }
                else
                {
                    ViewBag.Disabled = "";
                }
                ViewBag.History = _poApprovalHistoryRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToList();
                return View(await _poApprovalRepository.GetById(id));
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        #endregion

        #region Cấp độ 6
        public async Task<ActionResult> Level6(int id, FormCollection fc)
        {
            var check = _poApprovalHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .FirstOrDefault(p => p.POApprovalId == id && p.StaffId == clsPermission.GetUser().StaffID
                                && p.ApprovalPermissionId == 6);
            string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            if (check != null)
            {
                ViewBag.ID = id;
                ViewBag.URL = Request.Url.AbsoluteUri;

                // button chấp nhận, từ chối
                if (Request["btnAccept"] != null)
                {
                    check.Times++;
                    check.IsAccept = true;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // update ApprovalForm
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    item.ManagingDirector = check.tbl_Staff.FullName;
                    item.StatusId++;
                    await _poApprovalRepository.Update(item);
                }
                if (Request["btnCancel"] != null)
                {
                    check.Times++;
                    check.IsAccept = false;
                    check.Note = fc["txtNote"];
                    await _poApprovalHistoryRepository.Update(check);
                    // send mail to createPO
                    var item = _poApprovalRepository.FindId(check.POApprovalId);
                    string body = "<p>Nhân viên: " + check.tbl_Staff.FullName + " đã từ chối duyệt PO cấp độ 6</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", item.tbl_Staff.Email, "[TRAVELPLUS] Thông báo từ chối duyệt PO cấp độ 6", body);
                }

                // disabled button chấp nhận, từ chối
                if (check.IsAccept == true)
                {
                    ViewBag.Disabled = "disabled";
                }
                else
                {
                    ViewBag.Disabled = "";
                }
                ViewBag.History = _poApprovalHistoryRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToList();
                return View(await _poApprovalRepository.GetById(id));
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        #endregion

    }
}