using CRM.Core;
using CRM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TRAVELPLUS.Models;
using TRAVELPLUS.Utilities;

namespace TRAVELPLUS.Controllers.Partner
{
    [Authorize]
    public class PartnerTabInfoController : BaseController
    {
        // GET: PartnerTabInfo

        #region Init

        private IGenericRepository<tbl_Staff> _staffRepository;
        private IGenericRepository<tbl_Contract> _contractRepository;
        private IGenericRepository<tbl_Partner> _partnerRepository;
        private IGenericRepository<tbl_ServicesPartner> _servicesPartnerRepository;
        private IGenericRepository<tbl_Tags> _tagsRepository;
        private IGenericRepository<tbl_PartnerNote> _partnerNoteRepository;
        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private IGenericRepository<tbl_DocumentFile> _documentFileRepository;
        private IGenericRepository<tbl_UpdateHistory> _updateHistoryRepository;
        private IGenericRepository<tbl_ContactHistory> _contactHistoryRepository;
        private IGenericRepository<tbl_AppointmentHistory> _appointmentHistoryRepository;
        private IGenericRepository<tbl_ServicesPartner> _servicesPartnerHistoryRepository;
        private IGenericRepository<tbl_Tour> _tourRepository;
        private IGenericRepository<tbl_TourOption> _tourOptionRepository;
        private IGenericRepository<tbl_LiabilityCustomer> _liabilityCustomerRepository;
        private IGenericRepository<tbl_LiabilityPartner> _liabilityPartnerRepository;
        private IGenericRepository<tbl_Form> _formRepository;
        private IGenericRepository<tbl_Module> _moduleRepository;
        private IGenericRepository<tbl_Evaluation> _evaluationRepository;
        private IGenericRepository<tbl_EvaluationCriteria> _evaluationCriteriaRepository;
        private IGenericRepository<tbl_EvaluationDetail> _evaluationDetailRepository;
        private DataContext _db;

        public PartnerTabInfoController(
            IGenericRepository<tbl_Staff> staffRepository,
            IGenericRepository<tbl_Contract> contractRepository,
            IGenericRepository<tbl_ServicesPartner> servicesPartnerRepository,
            IGenericRepository<tbl_Partner> partnerRepository,
            IGenericRepository<tbl_Tags> tagsRepository,
            IGenericRepository<tbl_Dictionary> dictionaryRepository,
            IGenericRepository<tbl_Task> taskRepository,
            IGenericRepository<tbl_DocumentFile> documentFileRepository,
            IGenericRepository<tbl_UpdateHistory> updateHistoryRepository,
            IGenericRepository<tbl_ContactHistory> contactHistoryRepository,
            IGenericRepository<tbl_AppointmentHistory> appointmentHistoryRepository,
            IGenericRepository<tbl_ServicesPartner> servicesPartnerHistoryRepository,
            IGenericRepository<tbl_PartnerNote> partnerNoteRepository,
            IGenericRepository<tbl_Tour> tourRepository,
            IGenericRepository<tbl_EvaluationCriteria> evaluationCriteriaRepository,
            IGenericRepository<tbl_TourOption> tourOptionRepository,
            IGenericRepository<tbl_LiabilityCustomer> liabilityCustomerRepository,
            IGenericRepository<tbl_LiabilityPartner> liabilityPartnerRepository,
            IGenericRepository<tbl_Form> formRepository,
            IGenericRepository<tbl_Module> moduleRepository,
            IGenericRepository<tbl_Evaluation> evaluationRepository,
            IGenericRepository<tbl_EvaluationDetail> evaluationDetailRepository,
            IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._servicesPartnerRepository = servicesPartnerRepository;
            this._staffRepository = staffRepository;
            this._partnerRepository = partnerRepository;
            this._tagsRepository = tagsRepository;
            this._partnerNoteRepository = partnerNoteRepository;
            this._dictionaryRepository = dictionaryRepository;
            this._documentFileRepository = documentFileRepository;
            this._contactHistoryRepository = contactHistoryRepository;
            this._appointmentHistoryRepository = appointmentHistoryRepository;
            this._updateHistoryRepository = updateHistoryRepository;
            this._servicesPartnerHistoryRepository = servicesPartnerHistoryRepository;
            this._tourRepository = tourRepository;
            this._tourOptionRepository = tourOptionRepository;
            this._liabilityCustomerRepository = liabilityCustomerRepository;
            this._liabilityPartnerRepository = liabilityPartnerRepository;
            this._formRepository = formRepository;
            this._moduleRepository = moduleRepository;
            this._contractRepository = contractRepository;
            this._evaluationRepository = evaluationRepository;
            this._evaluationCriteriaRepository = evaluationCriteriaRepository;
            this._evaluationDetailRepository = evaluationDetailRepository;
            _db = new DataContext();
        }
        #endregion

        #region Permission
        void Permission(int PermissionsId, int formId)
        {
            var list = _db.tbl_ActionData.Where(p => p.FormId == formId && p.PermissionsId == PermissionsId).Select(p => p.FunctionId).ToList();
            ViewBag.IsAdd = list.Contains(1);
            ViewBag.IsDelete = list.Contains(2);
            ViewBag.IsEdit = list.Contains(3);
        }
        #endregion

        #region ThongTinChiTiet
        [ChildActionOnly]
        public ActionResult _ThongTinChiTiet()
        {
            return PartialView("_ThongTinChiTiet");
        }

        [HttpPost]
        public async Task<ActionResult> InfoThongTinChiTiet(int id)
        {
            var model = await _partnerRepository.GetById(id);
            return PartialView("_ThongTinChiTiet", model);
        }
        #endregion

        #region Lịch hẹn
        [ChildActionOnly]
        public ActionResult _LichHen()
        {
            ViewBag.IsAdd = false;
            ViewBag.IsDelete = false;
            ViewBag.IsEdit = false;
            return PartialView("_LichHen");
        }

        [HttpPost]
        public async Task<ActionResult> InfoLichHen(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 63);
            //var model = await _appointmentHistoryRepository.GetAllAsQueryable().Where(p => p.PartnerId == id).ToListAsync();
            var model = _appointmentHistoryRepository.GetAllAsQueryable().AsEnumerable()
                .OrderByDescending(p => p.CreatedDate)
                .Where(p => p.PartnerId == id && p.IsDelete == false)
                            .Select(p => new tbl_AppointmentHistory
                            {
                                Id = p.Id,
                                Title = p.Title,
                                Time = p.Time,
                                StatusId = p.StatusId,
                                tbl_DictionaryStatus = _dictionaryRepository.FindId(p.StatusId),
                                tbl_Staff = _staffRepository.FindId(p.StaffId),
                                Note = p.Note,
                                OtherStaff = p.OtherStaff,
                                TourId = p.TourId,
                                tbl_Tour = _tourRepository.FindId(p.TourId),
                                StaffId = p.StaffId
                            }).ToList();
            return PartialView("_LichHen", model);
        }
        #endregion

        #region Lịch sử liên hệ
        [ChildActionOnly]
        public ActionResult _LichSuLienHe()
        {
            ViewBag.IsAdd = false;
            ViewBag.IsDelete = false;
            ViewBag.IsEdit = false;
            return PartialView("_LichSuLienHe");
        }

        [HttpPost]
        public async Task<ActionResult> InfoLichSuLienHe(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 64);
            //var model = await _contactHistoryRepository.GetAllAsQueryable().Where(p => p.PartnerId == id).ToListAsync();
            var model = _db.tbl_ContactHistory.AsEnumerable().Where(p => p.PartnerId == id && p.IsDelete == false)
                       .Select(p => new tbl_ContactHistory
                       {
                           Id = p.Id,
                           ContactDate = p.ContactDate,
                           Request = p.Request,
                           Note = p.Note,
                           tbl_Staff = _staffRepository.FindId(p.StaffId),
                           tbl_Dictionary = _dictionaryRepository.FindId(p.DictionaryId),
                           StaffId = p.StaffId,
                           DictionaryId = p.DictionaryId,
                           OtherStaffId = p.OtherStaffId,
                           tbl_StaffOther = _staffRepository.FindId(p.OtherStaffId)
                       }).ToList();
            return PartialView("_LichSuLienHe", model);
        }
        #endregion

        #region Thầu/tour
        [ChildActionOnly]
        public ActionResult _TourTuyen()
        {
            return PartialView("_TourTuyen");
        }

        [HttpPost]
        public async Task<ActionResult> InfoTourTuyen(int id)
        {
            var model = _tourOptionRepository.GetAllAsQueryable().Where(c => c.PartnerId == id && c.IsDelete == false)
                .Select(p => new TourListViewModel
                {
                    Id = p.tbl_Tour.Id,
                    Code = p.tbl_Tour.Code,
                    Name = p.tbl_Tour.Name,
                    NumberCustomer = p.tbl_Tour.NumberCustomer ?? 0,
                    StartDate = p.tbl_Tour.StartDate,
                    EndDate = p.tbl_Tour.EndDate,
                    NumberDay = p.tbl_Tour.NumberDay ?? 0,
                    TourType = p.tbl_Tour.tbl_DictionaryTypeTour.Name,
                    Status = p.tbl_Tour.tbl_DictionaryStatus.Name,
                }).ToList();
            return PartialView("_TourTuyen", model);
        }
        #endregion

        #region Đánh giá
        [ChildActionOnly]
        public ActionResult _DanhGia()
        {
            return PartialView("_DanhGia");
        }

        [HttpPost]
        public ActionResult InfoDanhGia(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 1149);
            var model = _evaluationRepository.GetAllAsQueryable().AsEnumerable()
                         .Where(p => p.IsDelete == false && p.PartnerId == id
                         & (p.CreateStaffId == clsPermission.GetUser().StaffID || p.CreateStaffId == 2))
                         .OrderByDescending(p => p.CreatedDate)
                         .Select(p => new tbl_Evaluation() {
                             Id = p.Id,
                             Name = p.Name,
                             tbl_TagsArea = _tagsRepository.FindId(p.AreaId),
                             tbl_TagsProvince = _tagsRepository.FindId(p.ProvinceId),
                             tbl_Tour = _tourRepository.FindId(p.TourId),
                             Point = p.Point,
                             Note = p.Note,
                             CreatedDate = p.CreatedDate,
                             tbl_StaffCreate = _staffRepository.FindId(p.CreateStaffId)
                         }).ToList();
            return PartialView("_DanhGia", model);
        }

        [HttpPost]
        public ActionResult DetailDanhGia(int? id)
        {
            Permission(clsPermission.GetUser().PermissionID, 1149);
            var model = _evaluationDetailRepository.GetAllAsQueryable()
                .AsEnumerable().Where(p => p.EvaluationId == id)
                .Select(p=> new tbl_EvaluationDetail {
                    Id = p.Id,
                    Point = p.Point,
                    tbl_EvaluationCriteria = _evaluationCriteriaRepository.FindId(p.EvaluationCriteriaId),
                    Note = p.Note
                }).ToList();
            return PartialView("_Partial_Detail_DanhGia", model);
        }
        #endregion

        #region Dịch vụ cung cấp
        [ChildActionOnly]
        public ActionResult _DichVuCungCap()
        {
            return PartialView("_DichVuCungCap");
        }

        [HttpPost]
        public async Task<ActionResult> InfoDichVuCungCap(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 17);
            var model = await _servicesPartnerRepository.GetAllAsQueryable().Where(p => p.PartnerId == id && p.IsDelete == false).ToListAsync();
            return PartialView("_DichVuCungCap", model);
        }
        #endregion

        #region Ghi chú
        [ChildActionOnly]
        public ActionResult _GhiChu()
        {
            ViewBag.IsAdd = false;
            ViewBag.IsDelete = false;
            ViewBag.IsEdit = false;
            return PartialView("_GhiChu");
        }

        [HttpPost]
        public async Task<ActionResult> InfoGhiChu(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 65);
            var model = _db.tbl_PartnerNote.AsEnumerable().Where(p => p.PartnerId == id).Where(p => p.IsDelete == false).ToList();
            return PartialView("_GhiChu", model);
        }
        #endregion

        #region Invoice
        [ChildActionOnly]
        public ActionResult _Invoice()
        {
            return PartialView("_Invoice");
        }

        [HttpPost]
        public async Task<ActionResult> InfoInvoice()
        {
            return PartialView("_Invoice");
        }
        #endregion

        #region Tài liệu mẫu
        [ChildActionOnly]
        public ActionResult _TaiLieuMau()
        {
            ViewBag.IsAdd = false;
            ViewBag.IsDelete = false;
            ViewBag.IsEdit = false;
            return PartialView("_TaiLieuMau");
        }

        [HttpPost]
        public async Task<ActionResult> InfoTaiLieuMau(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 62);
            // var model = await _documentFileRepository.GetAllAsQueryable().Where(p => p.PartnerId == id).ToListAsync();
            var model = _db.tbl_DocumentFile.AsEnumerable().Where(p => p.PartnerId == id && p.IsDelete == false)
                    .Select(p => new tbl_DocumentFile
                    {
                        Id = p.Id,
                        FileName = p.FileName,
                        FileSize = p.FileSize,
                        Note = p.Note,
                        CreatedDate = p.CreatedDate,
                        TagsId = p.TagsId,
                        tbl_Staff = _staffRepository.FindId(p.StaffId)
                    }).ToList();
            return PartialView("_TaiLieuMau", model);
        }
        #endregion

        #region Cập nhật thay đổi
        [ChildActionOnly]
        public ActionResult _CapNhatThayDoi()
        {
            return PartialView("_CapNhatThayDoi");
        }

        [HttpPost]
        public ActionResult InfoCapNhatThayDoi(int id)
        {
            var model = _updateHistoryRepository.GetAllAsQueryable().AsEnumerable()
                .Where(p => p.IsDelete == false && p.PartnerId == id)
                .OrderByDescending(p => p.CreatedDate)
                .Select(p => new HistoryLogViewModel()
                {
                    Id = p.Id,
                    Staff = _staffRepository.FindId(p.StaffId).FullName,
                    Date = string.Format("{0:dd-MM-yyyy hh:mm tt}", p.CreatedDate),
                    Note = p.Note,
                    StaffId = p.StaffId,
                    Form = p.FormId != null ? _formRepository.FindId(p.FormId).Name : "",
                    Module = p.ModuleId != null ? _moduleRepository.FindId(p.ModuleId).Name : ""
                }).ToList();
            return PartialView("_CapNhatThayDoi", model);
        }
        #endregion

        #region Hợp đồng
        [ChildActionOnly]
        public ActionResult _HopDong()
        {
            ViewBag.IsAdd = false;
            ViewBag.IsEdit = false;
            ViewBag.IsDelete = false;
            return PartialView("_HopDong");
        }

        [HttpPost]
        public ActionResult InfoHopDong(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 1140);
            var docs = _documentFileRepository.GetAllAsQueryable();
            var model = _contractRepository.GetAllAsQueryable().AsEnumerable()
                .Where(p => p.PartnerId == id && p.IsDelete == false)
                .Select(p => new ContractTourViewModel
                {
                    Id = p.Id,
                    Code = p.Code,
                    ContractDate = p.ContractDate,
                    FileName = p.FileName,
                    tbl_Staff = p.tbl_Staff,
                    PartnerId = p.PartnerId ?? 0,
                    CreatedDate = p.CreatedDate
                }).ToList();
            return PartialView("_HopDong", model);
        }
        #endregion
    }
}