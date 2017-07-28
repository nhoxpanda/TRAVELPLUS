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
    public class POApprovalPermissionController : BaseController
    {
        // GET: POApprovalPermission

        #region Init

        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private IGenericRepository<tbl_Staff> _staffRepository;
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

        public POApprovalPermissionController(IGenericRepository<tbl_Dictionary> dictionaryRepository,
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

        #region List

        public async Task<ActionResult> Index()
        {
            Permission(clsPermission.GetUser().PermissionID, 1147);
            return View(await _poApprovalPermissionRepository.GetAllAsQueryable().ToListAsync());
        }

        #endregion

        #region Update Staff

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _poApprovalPermissionRepository.GetById(id);
            ViewBag.StaffFullList = await _staffRepository.GetAllAsQueryable().Where(p => p.IsDelete == false).OrderBy(p => p.FullName).ToListAsync();
            return PartialView("_Partial_Edit", model);

        }

        public async Task<ActionResult> Update(tbl_POApprovalPermission model)
        {
            await _poApprovalPermissionRepository.Update(model);
            return RedirectToAction("Index");
        }
        #endregion
    }
}