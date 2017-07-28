using CRM.Core;
using CRM.Enum;
using CRM.Infrastructure;
using TRAVELPLUS.Models;
using TRAVELPLUS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TRAVELPLUS.Controllers.Ticket
{
    [Authorize]
    public class AirportManageController : BaseController
    {
        // GET: AirportManage

        #region Init

        private IGenericRepository<tbl_DocumentFile> _documentFileRepository;
        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private IGenericRepository<tbl_Tags> _tagsRepository;
        private IGenericRepository<tbl_ActionData> _actionDataRepository;
        private IGenericRepository<tbl_Staff> _staffRepository;
        private IGenericRepository<tbl_Tour> _tourRepository;
        private IGenericRepository<tbl_CustomerContact> _customerContactRepository;
        private IGenericRepository<tbl_CustomerContactVisa> _customerContactVisaRepository;
        private IGenericRepository<tbl_CustomerVisa> _customerVisaRepository;
        private IGenericRepository<tbl_StaffVisa> _staffVisaRepository;
        private IGenericRepository<tbl_AppointmentHistory> _appointmentHistoryRepository;
        private IGenericRepository<tbl_TourGuide> _tourGuideRepository;
        private IGenericRepository<tbl_TourSchedule> _tourScheduleRepository;
        private IGenericRepository<tbl_Task> _taskRepository;
        private IGenericRepository<tbl_Airport> _airportRepository;
        private DataContext _db;

        public AirportManageController(IGenericRepository<tbl_DocumentFile> documentFileRepository,
            IGenericRepository<tbl_Dictionary> dictionaryRepository,
            IGenericRepository<tbl_Tags> tagsRepository,
            IGenericRepository<tbl_ActionData> actionDataRepository,
            IGenericRepository<tbl_Staff> staffRepository,
            IGenericRepository<tbl_Tour> tourRepository,
            IGenericRepository<tbl_CustomerContact> customerContactRepository,
            IGenericRepository<tbl_CustomerContactVisa> customerContactVisaRepository,
            IGenericRepository<tbl_CustomerVisa> customerVisaRepository,
            IGenericRepository<tbl_StaffVisa> staffVisaRepository,
            IGenericRepository<tbl_AppointmentHistory> appointmentHistoryRepository,
            IGenericRepository<tbl_TourGuide> tourGuideRepository,
            IGenericRepository<tbl_TourSchedule> tourScheduleRepository,
            IGenericRepository<tbl_Task> taskRepository,
            IGenericRepository<tbl_Airport> airportRepository,
            IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._airportRepository = airportRepository;
            this._documentFileRepository = documentFileRepository;
            this._dictionaryRepository = dictionaryRepository;
            this._tagsRepository = tagsRepository;
            this._actionDataRepository = actionDataRepository;
            this._staffRepository = staffRepository;
            this._tourRepository = tourRepository;
            this._customerContactRepository = customerContactRepository;
            this._customerContactVisaRepository = customerContactVisaRepository;
            this._customerVisaRepository = customerVisaRepository;
            this._staffVisaRepository = staffVisaRepository;
            this._appointmentHistoryRepository = appointmentHistoryRepository;
            this._tourGuideRepository = tourGuideRepository;
            this._tourScheduleRepository = tourScheduleRepository;
            this._taskRepository = taskRepository;
            _db = new DataContext();
        }

        #endregion

        #region Index + permission

        void Permission(int PermissionsId, int formId)
        {
            var list = _actionDataRepository.GetAllAsQueryable().AsEnumerable()
                .Where(p => p.FormId == formId && p.PermissionsId == PermissionsId).Select(p => p.FunctionId).ToList();
            ViewBag.IsAdd = list.Contains(1);
            ViewBag.IsDelete = list.Contains(2);
            ViewBag.IsEdit = list.Contains(3);
        }

        public ActionResult Index()
        {
            Permission(clsPermission.GetUser().PermissionID, 1152);
            var model = _airportRepository.GetAllAsQueryable().Where(p => p.IsDelete == false).ToList();
            return View(model);
        }

        #endregion

        #region Create

        public async Task<ActionResult> Create(tbl_Airport model)
        {
            Permission(clsPermission.GetUser().PermissionID, 1152);
            await _airportRepository.Create(model);
            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        public async Task<ActionResult> Edit(int id)
        {
            Permission(clsPermission.GetUser().PermissionID, 1152);
            var model = await _airportRepository.GetById(id);
            return PartialView("_Partial_EditAirport", model);
        }

        public async Task<ActionResult> Update(tbl_Airport model)
        {
            Permission(clsPermission.GetUser().PermissionID, 1152);
            await _airportRepository.Update(model);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(FormCollection fc)
        {
            Permission(clsPermission.GetUser().PermissionID, 1152);
            if (fc["listItemId"] != null && fc["listItemId"] != "")
            {
                var listIds = fc["listItemId"].Split(',');
                listIds = listIds.Take(listIds.Count() - 1).ToArray();
                if (listIds.Count() > 0)
                {
                    if (await _airportRepository.DeleteMany(listIds, false))
                    {
                        return Json(new ActionModel() { Succeed = true, Code = "200", View = "", Message = "Xóa dữ liệu thành công !", IsPartialView = false, RedirectTo = Url.Action("Index", "AirportManage") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new ActionModel() { Succeed = false, Code = "200", View = "", Message = "Xóa dữ liệu thất bại !" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new ActionModel() { Succeed = false, Code = "200", View = "", Message = "Vui lòng chọn những mục cần xóa !" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}