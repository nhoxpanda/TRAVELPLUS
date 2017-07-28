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

namespace TRAVELPLUS.Controllers.Ticket
{
    [Authorize]
    public class TicketManageController : BaseController
    {
        // GET: TicketManage

        #region Init

        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private IGenericRepository<tbl_Tags> _tagsRepository;
        private IGenericRepository<tbl_Customer> _customerRepository;
        private IGenericRepository<tbl_CustomerLeague> _customerLeagueRepository;
        private IGenericRepository<tbl_Staff> _staffRepository;
        private IGenericRepository<tbl_Tour> _tourRepository;
        private IGenericRepository<tbl_Contract> _contractRepository;
        private IGenericRepository<tbl_Ticket> _ticketRepository;
        private IGenericRepository<tbl_Airline> _airlineRepository;
        private DataContext _db;

        public TicketManageController(IGenericRepository<tbl_Dictionary> dictionaryRepository,
            IGenericRepository<tbl_Tags> tagsRepository,
            IGenericRepository<tbl_Customer> customerRepository,
            IGenericRepository<tbl_CustomerLeague> customerLeagueRepository,
            IGenericRepository<tbl_Staff> staffRepository,
            IGenericRepository<tbl_Tour> tourRepository,
            IGenericRepository<tbl_Contract> contractRepository,
            IGenericRepository<tbl_Ticket> ticketRepository,
            IGenericRepository<tbl_Airline> airlineRepository,
            IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._tagsRepository = tagsRepository;
            this._dictionaryRepository = dictionaryRepository;
            this._customerRepository = customerRepository;
            this._customerLeagueRepository = customerLeagueRepository;
            this._staffRepository = staffRepository;
            this._tourRepository = tourRepository;
            this._contractRepository = contractRepository;
            this._ticketRepository = ticketRepository;
            this._airlineRepository = airlineRepository;
            _db = new DataContext();
        }

        #endregion

        #region List

        int SDBID = 6;
        int maPB = 0, maNKD = 0, maNV = 0, maCN = 0;
        void Permission(int PermissionsId, int formId)
        {
            var list = _db.tbl_ActionData.Where(p => p.FormId == formId && p.PermissionsId == PermissionsId).Select(p => p.FunctionId).ToList();
            ViewBag.IsAdd = list.Contains(1);
            ViewBag.IsDelete = list.Contains(2);
            ViewBag.IsEdit = list.Contains(3);

            //cập nhật trạng thái
            var listUS = _db.tbl_ActionData.Where(p => p.FormId == 1098 & p.PermissionsId == PermissionsId).Select(p => p.FunctionId).ToList();
            ViewBag.IsUpdateStatus = list.Contains(1);

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


        //[CheckSessionTimeOut]
        public ActionResult Index()
        {
            Permission(clsPermission.GetUser().PermissionID, 1098);

            if (SDBID == 6)
                return View(new List<tbl_Ticket>());

            var model = _ticketRepository.GetAllAsQueryable().Where(p => (p.Staff == maNV | maNV == 0)
                    & (p.tbl_Staff.DepartmentId == maPB | maPB == 0)
                    & (p.tbl_Staff.StaffGroupId == maNKD | maNKD == 0)
                    & (p.tbl_Staff.HeadquarterId == maCN | maCN == 0) & (p.IsDelete == false))
                    .OrderByDescending(p => p.CreateDate)
                    .ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetIdTicket(int id)
        {
            Session["idTicket"] = id;
            return Json(JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(tbl_Ticket model)
        {
            try
            {
                Permission(clsPermission.GetUser().PermissionID, 1098);
                var checkcode = _ticketRepository.GetAllAsQueryable().FirstOrDefault(p => p.Code == model.Code && p.IsDelete == false);
                if (checkcode == null)
                {
                    model.CreateDate = DateTime.Now;
                    model.ModifyDate = DateTime.Now;
                    model.IsDelete = false;
                    model.Staff = clsPermission.GetUser().StaffID;
                    model.FarePrice = model.FarePrice != null ? Convert.ToDecimal(model.FarePrice.ToString().Replace(".", "")) : 0;
                    model.TaxPrice = model.TaxPrice != null ? Convert.ToDecimal(model.TaxPrice.ToString().Replace(".", "")) : 0;
                    model.ServicePrice = model.ServicePrice != null ? Convert.ToDecimal(model.ServicePrice.ToString().Replace(".", "")) : 0;
                    model.SalePrice = model.SalePrice != null ? Convert.ToDecimal(model.SalePrice.ToString().Replace(".", "")) : 0;
                    model.Price = model.Price != null ? Convert.ToDecimal(model.Price.ToString().Replace(".", "")) : 0;
                    model.ComPrice = model.ComPrice != null ? Convert.ToDecimal(model.ComPrice.ToString().Replace(".", "")) : 0;
                    model.RosePrice = model.RosePrice != null ? Convert.ToDecimal(model.RosePrice.ToString().Replace(".", "")) : 0;
                    if (await _ticketRepository.Create(model))
                    {
                        UpdateHistory.SaveHistory(1098, "Thêm mới vé, code: " + model.Code + " - " + model.Name,
                            null, //appointment
                            model.ContractId, //contract
                            model.CustomerId, //customer
                            null, //partner
                            model.ProgramId, //program
                            null, //task
                            model.TourId, //tour
                            null, //quotation
                            null, //document
                            null, //history
                            model.Id // ticket
                        );
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Update

        public async Task<ActionResult> EditInfoTicket(int id)
        {
            var model = await _ticketRepository.GetById(id);
            var airline = _airlineRepository.FindId(model.AirlineId);

            ViewBag.TicketViewModel = new TicketViewModel()
            {
                Percent = airline != null ? airline.Percent : 0,
                DepartureId1 = model.FirstDepartureId != null ? _tagsRepository.FindId(model.tbl_TagsFirstDeparture.ParentId).ParentId : 0,
                DepartureId2 = model.SecondDepartureId != null ? _tagsRepository.FindId(model.tbl_TagsSecondDeparture.ParentId).ParentId : 0,
                Destination1 = model.FirstDestinationId != null ? _tagsRepository.FindId(model.tbl_TagsFirstDestination.ParentId).ParentId : 0,
                Destination2 = model.SecondDestinationId != null ? _tagsRepository.FindId(model.tbl_TagsSecondDestination.ParentId).ParentId : 0,
            };
            return PartialView("_Partial_EditTicket", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Update(tbl_Ticket model)
        {
            try
            {
                Permission(clsPermission.GetUser().PermissionID, 1098);
                model.ModifyDate = DateTime.Now;
                model.Staff = clsPermission.GetUser().StaffID;
                model.FarePrice = model.FarePrice != null ? Convert.ToDecimal(model.FarePrice.ToString().Replace(".", "")) : 0;
                model.TaxPrice = model.TaxPrice != null ? Convert.ToDecimal(model.TaxPrice.ToString().Replace(".", "")) : 0;
                model.ServicePrice = model.ServicePrice != null ? Convert.ToDecimal(model.ServicePrice.ToString().Replace(".", "")) : 0;
                model.SalePrice = model.SalePrice != null ? Convert.ToDecimal(model.SalePrice.ToString().Replace(".", "")) : 0;
                model.Price = model.Price != null ? Convert.ToDecimal(model.Price.ToString().Replace(".", "")) : 0;
                model.ComPrice = model.ComPrice != null ? Convert.ToDecimal(model.ComPrice.ToString().Replace(".", "")) : 0;
                model.RosePrice = model.RosePrice != null ? Convert.ToDecimal(model.RosePrice.ToString().Replace(".", "")) : 0;
                if (await _ticketRepository.Update(model))
                {
                    UpdateHistory.SaveHistory(1098, "Cập nhật vé: " + model.Code,
                        null, //appointment
                            model.ContractId, //contract
                            model.CustomerId, //customer
                            null, //partner
                            model.ProgramId, //program
                            null, //task
                            model.TourId, //tour
                            null, //quotation
                            null, //document
                            null, //history
                            model.Id // ticket
                            );
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(FormCollection fc)
        {
            Permission(clsPermission.GetUser().PermissionID, 1098);
            try
            {
                if (fc["listItemId"] != null && fc["listItemId"] != "")
                {
                    var listIds = fc["listItemId"].Split(',');
                    listIds = listIds.Take(listIds.Count() - 1).ToArray();
                    if (listIds.Count() > 0)
                    {
                        //
                        foreach (var i in listIds)
                        {
                            var item = _ticketRepository.FindId(Convert.ToInt32(i));
                            UpdateHistory.SaveHistory(1098, "Xóa Vé máy bay: " + item.Code + " - " + item.Name,
                                null, //appointment
                                item.ContractId, //contract
                                item.CustomerId, //customer
                                null, //partner
                                item.ProgramId, //program
                                null, //task
                                item.TourId, //tour
                                null, //quotation
                                null, //document
                                null, //history
                                item.Id // ticket
                                );
                        }
                        //
                        if (await _ticketRepository.DeleteMany(listIds, false))
                        {
                            return Json(1, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(0, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Other Tab
        public ActionResult _Partial_TabInfoTicket()
        {
            return PartialView("_Partial_TabInfoTicket");
        }

        public ActionResult InfoNote(int id)
        {
            var model = _ticketRepository.FindId(id);
            return PartialView("_Partial_TabInfoTicket", model);
        }
        #endregion

        #region LoadCustomer
        public async Task<ActionResult> LoadCustomer(int id)
        {
            var model = await _customerRepository.GetById(id);
            return Json(new { name = model.FullName, phone = model.MobilePhone }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAirlineLeague(int id)
        {
            var items = _customerLeagueRepository.GetAllAsQueryable()
                .Where(p => p.CustomerId == id).Select(p => p.tbl_DictionaryAirlineLeague).ToList();
            return Json(new SelectList(items, "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetCardLevelNumber(int league, int customer)
        {
            var model = await _customerLeagueRepository.GetAllAsQueryable()
                .FirstOrDefaultAsync(p => p.AirlineLeagueId == league && p.CustomerId == customer);
            return Json(new { idcard = model.CardLevelId, card = model.tbl_DictionaryCardLevel.Name, number = model.Code }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GetPercentForRose

        public ActionResult GetPercentForRose(int id)
        {
            var model = _airlineRepository.FindId(id);
            return Json(model.Percent, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}