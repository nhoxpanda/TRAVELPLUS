using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRAVELPLUS.Models;
using CRM.Core;
using CRM.Infrastructure;
using System.Threading.Tasks;
using CRM.Enum;
using TRAVELPLUS.Utilities;

namespace TRAVELPLUS.Controllers.Customer
{
    [Authorize]
    public class PartnerOtherTabController : BaseController
    {
        // GET: PartnerOtherTab

        #region Init

        private IGenericRepository<tbl_Staff> _staffRepository;
        private IGenericRepository<tbl_Customer> _customerRepository;
        private IGenericRepository<tbl_Contract> _contractRepository;
        private IGenericRepository<tbl_Partner> _partnerRepository;
        private IGenericRepository<tbl_ServicesPartner> _servicesPartnerRepository;
        private IGenericRepository<tbl_Tags> _tagsRepository;
        private IGenericRepository<tbl_CustomerContact> _customerContactRepository;
        private IGenericRepository<tbl_CustomerVisa> _customerVisaRepository;
        private IGenericRepository<tbl_CustomerContactVisa> _customerContactVisaRepository;
        private IGenericRepository<tbl_Tour> _tourRepository;
        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private IGenericRepository<tbl_DocumentFile> _documentFileRepository;
        private IGenericRepository<tbl_UpdateHistory> _updateHistoryRepository;
        private IGenericRepository<tbl_ContactHistory> _contactHistoryRepository;
        private IGenericRepository<tbl_Evaluation> _evaluationRepository;
        private IGenericRepository<tbl_EvaluationDetail> _evaluationDetailRepository;
        private IGenericRepository<tbl_AppointmentHistory> _appointmentHistoryRepository;
        private DataContext _db;

        public PartnerOtherTabController(
            IGenericRepository<tbl_Partner> partnerRepository,
            IGenericRepository<tbl_Evaluation> evaluationRepository,
            IGenericRepository<tbl_EvaluationDetail> evaluationDetailRepository,
            IGenericRepository<tbl_Contract> contractRepository,
            IGenericRepository<tbl_Staff> staffRepository,
            IGenericRepository<tbl_Customer> customerRepository,
            IGenericRepository<tbl_Tags> tagsRepository,
            IGenericRepository<tbl_CustomerContact> customerContactRepository,
            IGenericRepository<tbl_CustomerVisa> customerVisaRepository,
            IGenericRepository<tbl_Tour> tourRepository,
            IGenericRepository<tbl_Dictionary> dictionaryRepository,
            IGenericRepository<tbl_CustomerContactVisa> customerContactVisaRepository,
            IGenericRepository<tbl_DocumentFile> documentFileRepository,
            IGenericRepository<tbl_UpdateHistory> updateHistoryRepository,
            IGenericRepository<tbl_ContactHistory> contactHistoryRepository,
            IGenericRepository<tbl_AppointmentHistory> appointmentHistoryRepository,
             IGenericRepository<tbl_ServicesPartner> servicesPartnerRepository,
            IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._contractRepository = contractRepository;
            this._evaluationDetailRepository = evaluationDetailRepository;
            this._evaluationRepository = evaluationRepository;
            this._partnerRepository = partnerRepository;
            this._customerRepository = customerRepository;
            this._customerContactRepository = customerContactRepository;
            this._tagsRepository = tagsRepository;
            this._customerVisaRepository = customerVisaRepository;
            this._customerContactVisaRepository = customerContactVisaRepository;
            this._tourRepository = tourRepository;
            this._dictionaryRepository = dictionaryRepository;
            this._documentFileRepository = documentFileRepository;
            this._contactHistoryRepository = contactHistoryRepository;
            this._appointmentHistoryRepository = appointmentHistoryRepository;
            this._servicesPartnerRepository = servicesPartnerRepository;
            this._updateHistoryRepository = updateHistoryRepository;
            this._staffRepository = staffRepository;
            _db = new DataContext();
        }

        #endregion

        #region Permission
        int SDBID = 6;
        int maPB = 0, maNKD = 0, maNV = 0, maCN = 0;
        void Permission(int PermissionsId, int formId)
        {
            var list = _db.tbl_ActionData.Where(p => p.FormId == formId && p.PermissionsId == PermissionsId).Select(p => p.FunctionId).ToList();
            ViewBag.IsAdd = list.Contains(1);
            ViewBag.IsDelete = list.Contains(2);
            ViewBag.IsEdit = list.Contains(3);

            var ltAccess = _db.tbl_AccessData.Where(p => p.PermissionId == PermissionsId && p.FormId == formId).Select(p => p.ShowDataById).FirstOrDefault();
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

        #region Lịch hẹn
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateAppointment(tbl_AppointmentHistory model, FormCollection form)
        {
            try
            {
                Permission(clsPermission.GetUser().PermissionID, 63);

                model.PartnerId = Convert.ToInt32(Session["idPartner"].ToString());
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.StaffId = clsPermission.GetUser().StaffID;
                model.OtherStaff = form["OtherStaff"];
                model.PartnerId = model.PartnerId == 0 ? null : model.PartnerId;

                if (await _appointmentHistoryRepository.Create(model))
                {
                    UpdateHistory.SaveHistory(63, "Thêm mới lịch hẹn đối tác: " + model.Title,
                        model.Id, //appointment
                        model.ContractId, //contract
                        model.CustomerId, //customer
                        model.PartnerId, //partner
                        model.ProgramId, //program
                        model.TaskId, //task
                        model.TourId, //tour
                        null, //quotation
                        null, //document
                        null, //history
                        null // ticket
                        );

                    var list = _appointmentHistoryRepository.GetAllAsQueryable().AsEnumerable()
                                .Where(p => p.PartnerId == model.PartnerId && p.IsDelete == false)
                                .OrderByDescending(p => p.CreatedDate)
                                .Select(p => new tbl_AppointmentHistory
                                {
                                    Id = p.Id,
                                    Title = p.Title,
                                    StatusId = p.StatusId,
                                    Time = p.Time,
                                    tbl_DictionaryStatus = _dictionaryRepository.FindId(p.StatusId),
                                    tbl_Staff = _staffRepository.FindId(p.StaffId),
                                    Note = p.Note,
                                    OtherStaff = p.OtherStaff,
                                    TourId = p.TourId,
                                    tbl_Tour = _tourRepository.FindId(p.TourId)
                                }).ToList();
                    return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml", list);
                }
                else
                {
                    return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml");
            }
        }

        public JsonResult LoadPartner(int id)
        {
            var model = new SelectList(_partnerRepository.GetAllAsQueryable().Where(p => p.DictionaryId == id && p.IsDelete == false).ToList(), "Id", "Name");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //[ChildActionOnly]
        //public ActionResult _Partial_EditAppointmentHistory()
        //{
        //    return PartialView("_Partial_EditAppointmentHistory", new tbl_AppointmentHistory());
        //}

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> EditAppointment(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 63);
            var model = await _appointmentHistoryRepository.GetById(id);
            return PartialView("_Partial_EditAppointmentHistory", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> UpdateAppointment(tbl_AppointmentHistory model, FormCollection form)
        {
            try
            {
                Permission(clsPermission.GetUser().PermissionID, 63);
                model.ModifiedDate = DateTime.Now;
                model.OtherStaff = form["OtherStaff"];
                model.PartnerId = model.PartnerId == 0 ? null : model.PartnerId;

                if (await _appointmentHistoryRepository.Update(model))
                {
                    UpdateHistory.SaveHistory(63, "Cập nhật lịch hẹn: " + model.Title,
                        model.Id, //appointment
                        model.ContractId, //contract
                        model.CustomerId, //customer
                        model.PartnerId, //partner
                        model.ProgramId, //program
                        model.TaskId, //task
                        model.TourId, //tour
                        null, //quotation
                        null, //document
                        null, //history
                        null // ticket
                        );
                    var list = _appointmentHistoryRepository.GetAllAsQueryable().AsEnumerable()
                            .Where(p => p.PartnerId == model.PartnerId && p.IsDelete == false)
                            .OrderByDescending(p => p.CreatedDate)
                            .Select(p => new tbl_AppointmentHistory
                            {
                                Id = p.Id,
                                StatusId = p.StatusId,
                                Title = p.Title,
                                Time = p.Time,
                                tbl_DictionaryStatus = _dictionaryRepository.FindId(p.StatusId),
                                tbl_Staff = _staffRepository.FindId(p.StaffId),
                                Note = p.Note,
                                OtherStaff = p.OtherStaff,
                                TourId = p.TourId,
                                tbl_Tour = _tourRepository.FindId(p.TourId)
                            }).ToList();
                    return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml", list);
                }
                else
                {
                    return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 63);
            int parId = _appointmentHistoryRepository.FindId(id).PartnerId ?? 0;
            try
            {
                var listId = id.ToString().Split(',').ToArray();
                if (await _appointmentHistoryRepository.DeleteMany(listId, false))
                {
                    //
                    var item = _contactHistoryRepository.FindId(id);
                    UpdateHistory.SaveHistory(63, "Xóa lịch sử liên hệ: " + item.Request,
                        null, //appointment
                        item.ContractId, //contract
                        item.CustomerId, //customer
                        item.PartnerId, //partner
                        item.ProgramId, //program
                        null, //task
                        item.TourId, //tour
                        null, //quotation
                        null, //document
                        item.Id, //history
                        null // ticket
                        );
                    //
                    var list = _appointmentHistoryRepository.GetAllAsQueryable().AsEnumerable()
                            .Where(p => p.PartnerId == parId && p.IsDelete == false)
                            .OrderByDescending(p => p.CreatedDate)
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
                                tbl_Tour = _tourRepository.FindId(p.TourId)
                            }).ToList();
                    return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml", list);
                }
                else
                {
                    return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/PartnerTabInfo/_LichHen.cshtml");
            }
        }
        #endregion

        #region Lịch sử liên hệ
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateContactHistory(tbl_ContactHistory model, FormCollection form)
        {
            Permission(clsPermission.GetUser().PermissionID, 64);
            try
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.PartnerId = Int32.Parse(Session["idPartner"].ToString());
                model.StaffId = clsPermission.GetUser().StaffID;
                model.OtherStaffId = model.StaffId;
                if (await _contactHistoryRepository.Create(model))
                {
                    UpdateHistory.SaveHistory(64, "Thêm mới lịch sử liên hệ đối tác: " + model.Request,
                        null, //appointment
                        model.ContractId, //contract
                        model.CustomerId, //customer
                        model.PartnerId, //partner
                        model.ProgramId, //program
                        null, //task
                        model.TourId, //tour
                        null, //quotation
                        null, //document
                        model.Id, //history
                        null // ticket
                        );

                    var list = _db.tbl_ContactHistory.AsEnumerable().Where(p => p.PartnerId == model.PartnerId).Where(p => p.IsDelete == false)
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
                    return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml", list);
                }
                else
                {
                    return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml");
            }
        }

        //[ChildActionOnly]
        //public ActionResult _Partial_EditContactHistory()
        //{
        //    return PartialView("_Partial_EditContactHistory", new tbl_ContactHistory());
        //}

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> EditContactHistory(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 64);
            return PartialView("_Partial_EditContactHistory", await _contactHistoryRepository.GetById(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> UpdateContactHistory(tbl_ContactHistory model, FormCollection form)
        {
            Permission(clsPermission.GetUser().PermissionID, 64);
            try
            {
                model.ModifiedDate = DateTime.Now;
                model.OtherStaffId = model.StaffId;
                if (await _contactHistoryRepository.Update(model))
                {
                    UpdateHistory.SaveHistory(64, "Cập nhật lịch sử liên hệ: " + model.Request,
                        null, //appointment
                        model.ContractId, //contract
                        model.CustomerId, //customer
                        model.PartnerId, //partner
                        model.ProgramId, //program
                        null, //task
                        model.TourId, //tour
                        null, //quotation
                        null, //document
                        model.Id, //history
                        null // ticket
                        );
                    var list = _db.tbl_ContactHistory.AsEnumerable().Where(p => p.PartnerId == model.PartnerId).Where(p => p.IsDelete == false)
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
                    return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml", list);
                }
                else
                {
                    return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteContactHistory(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 64);
            try
            {
                int parId = _contactHistoryRepository.FindId(id).PartnerId ?? 0;
                var listId = id.ToString().Split(',').ToArray();
                if (await _contactHistoryRepository.DeleteMany(listId, false))
                {
                    //
                    var item = _contactHistoryRepository.FindId(id);
                    UpdateHistory.SaveHistory(64, "Xóa lịch sử liên hệ: " + item.Request,
                        null, //appointment
                        item.ContractId, //contract
                        item.CustomerId, //customer
                        item.PartnerId, //partner
                        item.ProgramId, //program
                        null, //task
                        item.TourId, //tour
                        null, //quotation
                        null, //document
                        item.Id, //history
                        null // ticket
                        );
                    //
                    var list = _db.tbl_ContactHistory.AsEnumerable().Where(p => p.PartnerId == parId).Where(p => p.IsDelete == false)
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
                    return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml", list);
                }
                else
                {
                    return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/PartnerTabInfo/_LichSuLienHe.cshtml");
            }
        }
        #endregion

        #region Hợp đồng

        [HttpPost]
        public ActionResult UploadFileContract(HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                Session["UploadFileContract"] = fileUpload;
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateContractPartner(tbl_Contract model, FormCollection form)
        {
            Permission(clsPermission.GetUser().PermissionID, 1140);
            try
            {
                var checkCode = _contractRepository.GetAllAsQueryable().FirstOrDefault(p => p.Code == model.Code && p.IsDelete == false);
                if (checkCode == null)
                {
                    model.PartnerId = Convert.ToInt32(Session["idPartner"]);
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                    model.Permission = clsPermission.GetUser().StaffID.ToString();
                    model.DictionaryId = 28;
                    model.StaffId = clsPermission.GetUser().StaffID;
                    if (model.TongDuKien != null)
                    {
                        model.LoiNhuanDuKien = model.TotalPrice - model.TongDuKien;
                        model.CurrencyLNDK = model.CurrencyTDK;
                    }
                    if (form["TagsId"] != null && form["TagsId"] != "")
                    {
                        model.TagsId = form["TagsId"].ToString();
                    }
                    if (Session["UploadFileContract"] != null)
                    {
                        var fileUpload = Session["UploadFileContract"] as HttpPostedFileBase;
                        string FileSize = Common.ConvertFileSize(fileUpload.ContentLength);
                        String newName = fileUpload.FileName.Insert(fileUpload.FileName.LastIndexOf('.'), String.Format("{0:_ddMMyyyy}", DateTime.Now));
                        String path = Server.MapPath("~/Upload/file/" + newName);
                        fileUpload.SaveAs(path);
                        model.FileName = newName;
                        Session["UploadFileContract"] = null;
                    }
                    if (await _contractRepository.Create(model))
                    {
                        UpdateHistory.SaveHistory(1140, "Thêm mới hợp đồng: " + model.Code + " - " + model.Name,
                            null, //appointment
                            model.Id, //contract
                            model.CustomerId, //customer
                            model.PartnerId, //partner
                            null, //program
                            null, //task
                            model.TourId, //tour
                            null, //quotation
                            null, //document
                            null, //history
                            null // ticket
                            );
                    }
                }
            }
            catch { }

            var list = _db.tbl_Contract.AsEnumerable()
                .Where(p => p.PartnerId == model.PartnerId && p.IsDelete == false)
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
            return PartialView("~/Views/PartnerTabInfo/_HopDong.cshtml", list);
        }

        [HttpPost]
        public async Task<ActionResult> EditContract(int id)
        {
            var model = await _contractRepository.GetById(id);
            return PartialView("_Partial_EditContract", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> UpdateContractPartner(tbl_Contract model, FormCollection form)
        {
            Permission(clsPermission.GetUser().PermissionID, 1140);
            try
            {
                model.ModifiedDate = DateTime.Now;
                model.Permission = clsPermission.GetUser().StaffID.ToString();
                model.DictionaryId = 28;
                model.StaffId = clsPermission.GetUser().StaffID;
                if (model.TongDuKien != null)
                {
                    model.LoiNhuanDuKien = model.TotalPrice - model.TongDuKien;
                    model.CurrencyLNDK = model.CurrencyTDK;
                }
                if (form["TagsId"] != null && form["TagsId"] != "")
                {
                    model.TagsId = form["TagsId"].ToString();
                }
                if (Session["UploadFileContract"] != null)
                {
                    var fileUpload = Session["UploadFileContract"] as HttpPostedFileBase;
                    string FileSize = Common.ConvertFileSize(fileUpload.ContentLength);
                    String newName = fileUpload.FileName.Insert(fileUpload.FileName.LastIndexOf('.'), String.Format("{0:_ddMMyyyy}", DateTime.Now));
                    String path = Server.MapPath("~/Upload/file/" + newName);
                    fileUpload.SaveAs(path);
                    model.FileName = newName;
                    Session["UploadFileContract"] = null;
                }
                if (await _contractRepository.Update(model))
                {
                    UpdateHistory.SaveHistory(1140, "Cập nhật hợp đồng: " + model.Code + " - " + model.Name,
                        null, //appointment
                        model.Id, //contract
                        model.CustomerId, //customer
                        model.PartnerId, //partner
                        null, //program
                        null, //task
                        model.TourId, //tour
                        null, //quotation
                        null, //document
                        null, //history
                        null // ticket
                        );
                }
            }
            catch { }

            var list = _db.tbl_Contract.AsEnumerable()
                .Where(p => p.PartnerId == model.PartnerId && p.IsDelete == false)
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
            return PartialView("~/Views/PartnerTabInfo/_HopDong.cshtml", list);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteContractPartner(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 1140);
            var contract = _contractRepository.FindId(id);
            int partnerId = contract.PartnerId ?? 0;
            try
            {
                var listId = id.ToString().Split(',').ToArray();
                if (await _contractRepository.DeleteMany(listId, false))
                {
                    //
                    var item = _contactHistoryRepository.FindId(id);
                    UpdateHistory.SaveHistory(1140, "Xóa hợp đồng: " + contract.Code + " - " + contract.Name,
                            null, //appointment
                            contract.Id, //contract
                            contract.CustomerId, //customer
                            contract.PartnerId, //partner
                            null, //program
                            null, //task
                            contract.TourId, //tour
                            null, //quotation
                            null, //document
                            null, //history
                            null // ticket
                            );
                }
            }
            catch { }
            var list = _db.tbl_Contract.AsEnumerable()
                .Where(p => p.PartnerId == partnerId && p.IsDelete == false)
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
            return PartialView("~/Views/PartnerTabInfo/_HopDong.cshtml", list);
        }
        #endregion

        #region Dịch vụ cung cấp

        [HttpPost]
        public ActionResult InsertService(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 17);
            ViewBag.parentId = id;
            return PartialView("_Partial_InsertService");
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateServicePartner(tbl_ServicesPartner model)
        {
            Permission(clsPermission.GetUser().PermissionID, 17);
            model.IsDelete = false;
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            await _servicesPartnerRepository.Create(model);
            //
            var list = await _db.tbl_ServicesPartner.Where(p => p.PartnerId == model.PartnerId
                            && p.IsDelete == false).OrderByDescending(p => p.CreatedDate).ToListAsync();
            return PartialView("~/Views/PartnerTabInfo/_DichVuCungCap.cshtml", list);
        }

        [HttpPost]
        [ValidateInput(false)]

        public async Task<ActionResult> EditService(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 17);
            var model = await _servicesPartnerRepository.GetById(id);
            return PartialView("_Partial_EditService", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> UpdateServicePartner(tbl_ServicesPartner model)
        {
            Permission(clsPermission.GetUser().PermissionID, 17);
            model.ModifiedDate = DateTime.Now;
            await _servicesPartnerRepository.Update(model);
            //
            var list = await _db.tbl_ServicesPartner.Where(p => p.PartnerId == model.PartnerId
                            && p.IsDelete == false).OrderByDescending(p => p.CreatedDate).ToListAsync();
            return PartialView("~/Views/PartnerTabInfo/_DichVuCungCap.cshtml", list);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteServicePartner(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 17);
            int partner = _servicesPartnerRepository.FindId(id).PartnerId;
            await _servicesPartnerRepository.DeleteMany(id.ToString().Split(',').ToArray(), false);
            //
            var list = await _db.tbl_ServicesPartner.Where(p => p.PartnerId == partner
                            && p.IsDelete == false).OrderByDescending(p => p.CreatedDate).ToListAsync();
            return PartialView("~/Views/PartnerTabInfo/_DichVuCungCap.cshtml", list);
        }

        #endregion

        #region Đánh giá

        [HttpPost]
        public ActionResult InsertEvaluation(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 1149);
            ViewBag.parentId = id;
            return PartialView("_Partial_InsertEvaluationPartner");
        }

        public async Task<ActionResult> CreateEvaluation(tbl_Evaluation model, FormCollection fc)
        {
            Permission(clsPermission.GetUser().PermissionID, 1149);
            decimal? total = 0;
            model.CreateStaffId = clsPermission.GetUser().StaffID;
            model.StaffId = clsPermission.GetUser().StaffID;
            model.CreatedDate = DateTime.Now;
            model.IsDelete = false;
            model.Type = 1;
            if (await _evaluationRepository.Create(model))
            {
                if (!string.IsNullOrEmpty(fc["NumberEvaluation"]))
                {
                    for (int i = 1; i <= Convert.ToInt32(fc["NumberEvaluation"]); i++)
                    {
                        var detail = new tbl_EvaluationDetail()
                        {
                            EvaluationCriteriaId = Convert.ToInt32(fc["EvaluationCriteriaId" + i]),
                            EvaluationId = model.Id,
                            Note = fc["EvaluationNote" + i],
                            Point = !string.IsNullOrEmpty(fc["EvaluationPoint" + i]) ? Convert.ToInt32(fc["EvaluationPoint" + i]) : 0,
                        };
                        await _evaluationDetailRepository.Create(detail);
                        total += detail.Point;
                    }
                    // update tổng tiền
                    UpdateDatabase.UpdateEvaluation(model.Id, total ?? 0);
                }
            }

            var list = _db.tbl_Evaluation.AsEnumerable()
                          .Where(p => p.IsDelete == false && p.PartnerId == model.PartnerId
                            && (p.CreateStaffId == clsPermission.GetUser().StaffID || p.CreateStaffId == 2))
                          .OrderByDescending(p => p.CreatedDate)
                          .Select(p => new tbl_Evaluation()
                          {
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
            return PartialView("~/Views/PartnerTabInfo/_DanhGia.cshtml", list);
        }

        public async Task<ActionResult> EditEvaluation(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 1149);
            ViewBag.DetailList = _evaluationDetailRepository.GetAllAsQueryable().Where(p => p.EvaluationId == id).ToList();
            return PartialView("_Partial_EditEvaluationPartner", await _evaluationRepository.GetById(id));
        }

        public async Task<ActionResult> UpdateEvaluation(tbl_Evaluation model, FormCollection fc)
        {
            Permission(clsPermission.GetUser().PermissionID, 1149);
            decimal? total = 0;
            await _evaluationRepository.Update(model);
            // xóa hết điểm đã có 
            foreach (var item in _evaluationDetailRepository.GetAllAsQueryable().Where(p => p.EvaluationId == model.Id).ToList())
            {
                await _evaluationDetailRepository.DeleteMany(item.Id.ToString().Split(',').ToArray(), true);
            }
            for (int i = 1; i <= Convert.ToInt32(fc["NumberEvaluationEdit"]); i++)
            {
                var detail = new tbl_EvaluationDetail()
                {
                    EvaluationCriteriaId = Convert.ToInt32(fc["EditEvaluationCriteriaId" + i]),
                    EvaluationId = model.Id,
                    Note = fc["EditEvaluationNote" + i],
                    Point = !string.IsNullOrEmpty(fc["EditEvaluationPoint" + i]) ? Convert.ToInt32(fc["EditEvaluationPoint" + i]) : 0,
                };
                await _evaluationDetailRepository.Create(detail);
                total += detail.Point;
            }
            // update tổng tiền
            UpdateDatabase.UpdateEvaluation(model.Id, total ?? 0);

            var list = _db.tbl_Evaluation.AsEnumerable()
                          .Where(p => p.IsDelete == false && p.PartnerId == model.PartnerId
                            && (p.CreateStaffId == clsPermission.GetUser().StaffID || p.CreateStaffId == 2))
                          .OrderByDescending(p => p.CreatedDate)
                          .Select(p => new tbl_Evaluation()
                          {
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
            return PartialView("~/Views/PartnerTabInfo/_DanhGia.cshtml", list);
        }

        public async Task<ActionResult> DeleteEvaluation(int id)
        {
            var model = _evaluationRepository.FindId(id);
            var details = _evaluationDetailRepository.GetAllAsQueryable().Where(p => p.EvaluationId == id).ToList();
            foreach (var item in details)
            {
                await _evaluationDetailRepository.DeleteMany(item.Id.ToString().Split(',').ToArray(), true);
            }
            await _evaluationRepository.DeleteMany(id.ToString().Split(',').ToArray(), true);

            var list = _db.tbl_Evaluation.AsEnumerable()
                          .Where(p => p.IsDelete == false && p.PartnerId == model.PartnerId
                            && (p.CreateStaffId == clsPermission.GetUser().StaffID || p.CreateStaffId == 2))
                          .OrderByDescending(p => p.CreatedDate)
                          .Select(p => new tbl_Evaluation()
                          {
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
            return PartialView("~/Views/PartnerTabInfo/_DanhGia.cshtml", list);
        }
        #endregion
    }
}