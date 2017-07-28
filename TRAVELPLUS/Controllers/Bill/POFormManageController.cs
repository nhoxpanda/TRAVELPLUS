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
    public class POFormManageController : BaseController
    {
        // GET: POFormManage

        #region Init

        private IGenericRepository<tbl_Staff> _staffRepository;
        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private IGenericRepository<tbl_Partner> _partnerRepository;
        private IGenericRepository<tbl_ActionData> _actionDataRepository;
        private IGenericRepository<tbl_AccessData> _accessDataRepository;
        private IGenericRepository<tbl_POApproval> _poApprovalRepository;
        private IGenericRepository<tbl_POApprovalDetail> _poApprovalDetailRepository;
        private IGenericRepository<tbl_POApprovalHistory> _poApprovalHistoryRepository;
        private IGenericRepository<tbl_POApprovalPermission> _poApprovalPermissionRepository;
        private IGenericRepository<tbl_Bank> _bankRepository;
        private IGenericRepository<tbl_BankDetail> _bankDetailRepository;
        private DataContext _db;

        public POFormManageController(IGenericRepository<tbl_Dictionary> dictionaryRepository,
            IGenericRepository<tbl_Staff> staffRepository,
            IGenericRepository<tbl_Partner> partnerRepository,
            IGenericRepository<tbl_ActionData> actionDataRepository,
            IGenericRepository<tbl_AccessData> accessDataRepository,
            IGenericRepository<tbl_POApproval> poApprovalRepository,
            IGenericRepository<tbl_POApprovalDetail> poApprovalDetailRepository,
            IGenericRepository<tbl_POApprovalHistory> poApprovalHistoryRepository,
            IGenericRepository<tbl_POApprovalPermission> poApprovalPermissionRepository,
            IGenericRepository<tbl_Bank> bankRepository,
            IGenericRepository<tbl_BankDetail> bankDetailRepository,
            IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._staffRepository = staffRepository;
            this._bankRepository = bankRepository;
            this._bankDetailRepository = bankDetailRepository;
            this._partnerRepository = partnerRepository;
            this._actionDataRepository = actionDataRepository;
            this._dictionaryRepository = dictionaryRepository;
            this._accessDataRepository = accessDataRepository;
            this._poApprovalRepository = poApprovalRepository;
            this._poApprovalDetailRepository = poApprovalDetailRepository;
            this._poApprovalHistoryRepository = poApprovalHistoryRepository;
            this._poApprovalPermissionRepository = poApprovalPermissionRepository;
            _db = new DataContext();
        }

        #endregion

        #region Permission
        int SDBID = 6;
        int maPB = 0, maNKD = 0, maNV = 0, maCN = 0;
        void Permission(int PermissionsId, int formId)
        {
            var list = _actionDataRepository.GetAllAsQueryable().AsEnumerable()
                .Where(p => p.FormId == formId && p.PermissionsId == PermissionsId)
                .Select(p => p.FunctionId).ToList();
            ViewBag.IsAdd = list.Contains(1);
            ViewBag.IsDelete = list.Contains(2);
            ViewBag.IsEdit = list.Contains(3);

            var ltAccess = _accessDataRepository.GetAllAsQueryable().AsEnumerable()
               .Where(p => p.PermissionId == PermissionsId && p.FormId == formId).Select(p => p.ShowDataById).FirstOrDefault();
            if (ltAccess != 0)
                this.SDBID = ltAccess;

            switch (SDBID)
            {
                case 2:
                    maPB = clsPermission.GetUser().DepartmentID;
                    maCN = clsPermission.GetUser().BranchID;
                    break;
                case 3:
                    maNKD = clsPermission.GetUser().GroupID;
                    maCN = clsPermission.GetUser().BranchID; break;
                case 4: maNV = clsPermission.GetUser().StaffID; break;
                case 5: maCN = clsPermission.GetUser().BranchID; break;
            }
        }
        #endregion

        #region Index
        public async Task<ActionResult> Index()
        {
            Permission(clsPermission.GetUser().PermissionID, 1148);
            var model = await _poApprovalRepository.GetAllAsQueryable().Where(p => p.IsDelete == false)
                .OrderBy(p => p.Deadline).ToListAsync();
            return View(model);
        }
        #endregion

        #region Create

        public async Task<ActionResult> Create(tbl_POApproval model, FormCollection fc)
        {
            decimal? total = 0;
            // thêm po
            model.CreateDate = DateTime.Now;
            model.StaffId = clsPermission.GetUser().StaffID;
            model.StatusId = 3368;
            model.Requester = _staffRepository.FindId(model.StaffId).FullName;
            await _poApprovalRepository.Create(model);
            // chi tiết po
            int count = Convert.ToInt32(fc["NumberInvoice"]);
            for (int i = 1; i <= count; i++)
            {
                if (!string.IsNullOrEmpty(fc["DescriptionInvoice" + i]))
                {
                    var detail = new tbl_POApprovalDetail()
                    {
                        CreateDate = DateTime.Now,
                        Description = fc["DescriptionInvoice" + i],
                        Invoice = fc["NoteInvoice" + i],
                        POApprovalId = model.Id,
                        Price = !string.IsNullOrEmpty(fc["PriceInvoice" + i]) ? Convert.ToDecimal(fc["PriceInvoice" + i]) : 0,
                        Quantity = !string.IsNullOrEmpty(fc["QuantityInvoice" + i]) ? Convert.ToInt32(fc["QuantityInvoice" + i]) : 0,
                        Total = !string.IsNullOrEmpty(fc["TotalInvoice" + i]) ? Convert.ToDecimal(fc["TotalInvoice" + i]) : 0
                    };
                    await _poApprovalDetailRepository.Create(detail);
                    total += detail.Total;
                }
            }
            // cập nhật số tiền
            model.AmountNumber = total;
            model.AmountWords = VNCurrency.ToString(total ?? 0);
            await _poApprovalRepository.Update(model);

            // lịch sử phiếu duyệt PO
            int j = 1;
            foreach (var item in _poApprovalPermissionRepository.GetAllAsQueryable().ToList())
            {
                var hisPO = new tbl_POApprovalHistory()
                {
                    CreateDate = DateTime.Now,
                    ApprovalPermissionId = j,
                    POApprovalId = model.Id,
                    StaffId = !string.IsNullOrEmpty(item.ListStaffId) ? Convert.ToInt32(item.ListStaffId.Replace(",", "")) : (int?)null
                };
                if (j == 1)
                {
                    hisPO.Times = 1;
                    hisPO.IsAccept = true;
                }
                await _poApprovalHistoryRepository.Create(hisPO);
                // send mail duyệt cấp độ 2
                if (j == 2)
                {
                    string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                    // gửi mail duyệt cấp 2
                    string email = _staffRepository.FindId(hisPO.StaffId).Email;
                    string body = "<p>Nhân viên: " + model.Requester + " đã tạo và duyệt PO cấp độ 1</p>"
                        + "<p>Vui lòng click vào <a href='" + domain + "/duyet-po-cap-do-2/" + item.Id + "'>đây</a> để duyệt cấp độ 2</p>";
                    SendEmail.Email_With_CCandBCC("panda.sendmail.demo@gmail.com", "zxcvbnm@123", email, "[TRAVELPLUS] Thông báo duyệt PO cấp độ 2", body);
                }
                j++;
            }
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> GetBanking(int id, int i)
        {
            if (i == 0)// khách hàng
            {
                var items = await _bankDetailRepository.GetAllAsQueryable().Where(p => p.CustomerId == id).Select(p => p.tbl_Bank).ToListAsync();
                return Json(new SelectList(items, "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
            else // đối tác
            {
                var items = await _bankDetailRepository.GetAllAsQueryable().Where(p => p.PartnerId == id).Select(p => p.tbl_Bank).ToListAsync();
                return Json(new SelectList(items, "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> GetDetailBank(int id)
        {
            var model = await _bankRepository.GetById(id);
            if (model != null)
            {
                string data = model.Note + " - " + model.Account;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Update

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _poApprovalRepository.GetById(id);
            ViewBag.DetailList = _poApprovalDetailRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToList();
            return PartialView("_Partial_EditPO", model);
        }

        public async Task<ActionResult> Update(tbl_POApproval model, FormCollection fc)
        {
            decimal? total = 0;
            // thêm po
            model.StaffId = clsPermission.GetUser().StaffID;
            // xóa các chi tiết đã có
            var list = _poApprovalDetailRepository.GetAllAsQueryable().Where(p => p.POApprovalId == model.Id).ToList();
            foreach (var l in list)
            {
                await _poApprovalDetailRepository.DeleteMany(l.Id.ToString().Split(',').ToArray(), true);
            }
            // chi tiết po
            int count = Convert.ToInt32(fc["NumberInvoiceEdit"]);
            for (int i = 1; i <= count; i++)
            {
                if (!string.IsNullOrEmpty(fc["DescriptionInvoiceEdit" + i]))
                {
                    var detail = new tbl_POApprovalDetail()
                    {
                        CreateDate = DateTime.Now,
                        Description = fc["DescriptionInvoiceEdit" + i],
                        Invoice = fc["NoteInvoiceEdit" + i],
                        POApprovalId = model.Id,
                        Price = !string.IsNullOrEmpty(fc["PriceInvoiceEdit" + i]) ? Convert.ToDecimal(fc["PriceInvoiceEdit" + i]) : 0,
                        Quantity = !string.IsNullOrEmpty(fc["QuantityInvoiceEdit" + i]) ? Convert.ToInt32(fc["QuantityInvoiceEdit" + i]) : 0,
                        Total = !string.IsNullOrEmpty(fc["TotalInvoiceEdit" + i]) ? Convert.ToDecimal(fc["TotalInvoiceEdit" + i]) : 0
                    };
                    await _poApprovalDetailRepository.Create(detail);
                    total += detail.Total;
                }
            }
            // cập nhật số tiền
            model.AmountNumber = total;
            model.AmountWords = VNCurrency.ToString(total ?? 0);
            await _poApprovalRepository.Update(model);

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(FormCollection fc)
        {
            Permission(clsPermission.GetUser().PermissionID, 1148);
            try
            {
                if (fc["listItemId"] != null && fc["listItemId"] != "")
                {
                    var listIds = fc["listItemId"].Split(',');
                    listIds = listIds.Take(listIds.Count() - 1).ToArray();
                    if (listIds.Count() > 0)
                    {
                        if (await _poApprovalRepository.DeleteMany(listIds, false))
                        {
                            return Json(new ActionModel() { Succeed = true, Code = "200", View = "", Message = "Xóa dữ liệu thành công !", IsPartialView = false, RedirectTo = Url.Action("Index", "POFormManage") }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new ActionModel() { Succeed = false, Code = "200", View = "", Message = "Xóa dữ liệu thất bại !" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return Json(new ActionModel() { Succeed = false, Code = "200", View = "", Message = "Vui lòng chọn những mục cần xóa !" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Detail

        public async Task<ActionResult> Details(int id)
        {
            var model = new POApprovalViewModel()
            {
                tbl_POApproval = await _poApprovalRepository.GetById(id),
                tbl_POApprovalDetail = await _poApprovalDetailRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToListAsync()
            };
            return PartialView("_Partial_Detail", model);
        }
        #endregion

        #region Preview

        public async Task<ActionResult> Preview(int id)
        {
            var model = new POApprovalViewModel()
            {
                tbl_POApproval = await _poApprovalRepository.GetById(id),
                tbl_POApprovalDetail = await _poApprovalDetailRepository.GetAllAsQueryable().Where(p => p.POApprovalId == id).ToListAsync()
            };
            return PartialView("_Partial_PreviewPO", model);
        }

        #endregion

    }
}