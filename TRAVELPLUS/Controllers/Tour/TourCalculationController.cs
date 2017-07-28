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

namespace TRAVELPLUS.Controllers.Tour
{
    public class TourCalculationController : BaseController
    {
        private IGenericRepository<tbl_TourCalculation> _tourCalculationRepository;
        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private IGenericRepository<tbl_Tour> _tourRepository;
        private DataContext _db;
        public TourCalculationController(
            IGenericRepository<tbl_TourCalculation> tourCalculationRepository,
            IGenericRepository<tbl_Tour> tourRepository,
            IGenericRepository<tbl_Dictionary> dictionaryRepository,
        IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._tourCalculationRepository = tourCalculationRepository;
            this._tourRepository = tourRepository;
            this._dictionaryRepository = dictionaryRepository;
            _db = new DataContext();
        }

        // GET: TourCalculation
        public ActionResult Index(int? id)
        {
            return View();
        }
        public async Task<ActionResult> Create(TourCalModel model)
        {
            decimal exchange = Convert.ToDecimal(_dictionaryRepository.FindId(model.CurrencyId).Note);
            tbl_TourCalculation tourCal = new tbl_TourCalculation()
            {
                CreateDate = DateTime.Now,
                Date = DateTime.Parse(model.DateSelect),
                Note = model.Note,
                PartnerId = model.PartnerID,
                Price = model.PriceTourCal != null ? decimal.Parse((model.PriceTourCal * exchange).ToString().Replace(",", "")) : 0,
                StaffId = clsPermission.GetUser().StaffID,
                ServiceId = model.ServiceID,
                TourId = model.TourID,
                CurrencyId = model.CurrencyId
            };
            await _tourCalculationRepository.Create(tourCal);
            return RedirectToAction("Index"); ;
        }

        public JsonResult getInfoFromTour(int? id)
        {
            TourCalModel tourCal = new TourCalModel();
            var _tour = _tourRepository.GetAllAsQueryable().FirstOrDefault(x => x.Id == id && x.IsDelete == false);
            var listTourCal = _tourCalculationRepository.GetAllAsQueryable().AsEnumerable().Where(x => x.TourId == id).Select(x => new tbl_TourCalculation()
            {
                TourId = x.TourId,
                CreateDate = x.CreateDate,
                Date = x.Date,
                Id = x.Id,
                Note = x.Note,
                PartnerId = x.PartnerId,
                Price = x.Price,
                ServiceId = x.ServiceId,
                StaffId = x.StaffId,
                CurrencyId = x.CurrencyId
            }).ToList();
            tourCal.listTourCal = listTourCal;
            tourCal.FromDate = _tour.StartDate.Value.ToShortDateString();
            tourCal.ToDate = _tour.EndDate.Value.ToShortDateString();
            tourCal.SoNgay = _tour.NumberDay ?? 0;
            return Json(new { tourCal = tourCal }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> getListTourCalByTourID(int? id)
        {
            var model = await _tourCalculationRepository.GetAllAsQueryable().Where(p => p.TourId == id).ToListAsync();
            return PartialView("_Partial_Detail", model);
        }

        public ActionResult getInfoAddTourCal(int? id)
        {
            var _tour = _tourRepository.GetAllAsQueryable().FirstOrDefault(x => x.Id == id && x.IsDelete == false);
            TourCalModel _tourCal = new TourCalModel();
            _tourCal.TourName = _tour.Name;

            for (int i = 0; i < _tour.NumberDay; i++)
            {
                _tourCal.listDate.Add(new SelectListItem()
                {
                    Text = (_tour.StartDate ?? DateTime.Now).AddDays(i).ToShortDateString(),
                    Value = (_tour.StartDate ?? DateTime.Now).AddDays(i).ToShortDateString()
                });
            }
            return PartialView("_Partial_TourCal_Insert", _tourCal);
        }

        public JsonResult getPartnerListByServiceID(int? id)
        {
            List<tbl_Partner> partnerList = new List<tbl_Partner>();
            if (id != 0)
            {
                partnerList = _db.tbl_Partner.AsEnumerable().Where(p => p.IsDelete == false && p.DictionaryId == id)
                    .Select(p => new tbl_Partner
                    {
                        Id = p.Id,
                        Name = "[" + p.Code + "] " + p.Name,
                        Code = p.Code
                    }).ToList();
            }
            else
            {
                partnerList = _db.tbl_Partner.AsEnumerable().Where(p => p.IsDelete == false)
                    .Select(p => new tbl_Partner
                    {
                        Id = p.Id,
                        Name = "[" + p.Code + "] " + p.Name,
                        Code = p.Code
                    }).ToList();
            }
            return Json(new { listPartner = partnerList }, JsonRequestBehavior.AllowGet);
        }
    }
}