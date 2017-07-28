﻿using CRM.Core;
using CRM.Infrastructure;
using System;
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
    public class CardLevelManageController : BaseController
    {
        // GET: CardLevelManage
       #region Init

        private IGenericRepository<tbl_Dictionary> _dictionaryRepository;
        private DataContext _db;

        public CardLevelManageController(IGenericRepository<tbl_Dictionary> dictionaryRepository, IBaseRepository baseRepository)
            : base(baseRepository)
        {
            this._dictionaryRepository = dictionaryRepository;
            _db = new DataContext();
        }

        #endregion


        //[CheckSessionTimeOut]
        public ActionResult Index()
        {
            int perId = clsPermission.GetUser().PermissionID;
            var list = _db.tbl_ActionData.Where(p => p.FormId == 1151 && p.PermissionsId == perId).Select(p => p.FunctionId).ToList();
            ViewBag.IsAdd = list.Contains(1);
            ViewBag.IsDelete = list.Contains(2);
            ViewBag.IsEdit = list.Contains(3);
            var dictionary = _dictionaryRepository.GetAllAsQueryable().Where(p => p.DictionaryCategoryId == 35 && p.IsDelete == false).Select(p => new DictionaryViewModel
            {
                Id = p.Id,
                Name = p.Name
            });

            return View(dictionary.ToList());
        }

        [HttpPost]
        public ActionResult SaveData(int id, string name)
        {
            try
            {
                if (id == 0) // insert
                {
                    tbl_Dictionary dictionary = new tbl_Dictionary()
                    {
                        Name = name,
                        DictionaryCategoryId = 35
                    };

                    _db.tbl_Dictionary.Add(dictionary);
                }
                else // update
                {
                    var item = _db.tbl_Dictionary.Find(id);
                    item.Name = name;
                }

                _db.SaveChanges();
            }
            catch
            {
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(FormCollection fc)
        {
            if (fc["listItemId"] != null && fc["listItemId"] != "")
            {
                var listIds = fc["listItemId"].Split(',');
                listIds = listIds.Take(listIds.Count() - 1).ToArray();
                if (listIds.Count() > 0)
                {
                    if (await _dictionaryRepository.DeleteMany(listIds, false))
                    {
                        return Json(new ActionModel() { Succeed = true, Code = "350", View = "", Message = "Xóa dữ liệu thành công !", IsPartialView = false, RedirectTo = Url.Action("Index", "CardLevelManage") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new ActionModel() { Succeed = false, Code = "350", View = "", Message = "Xóa dữ liệu thất bại !" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new ActionModel() { Succeed = false, Code = "350", View = "", Message = "Vui lòng chọn những mục cần xóa !" }, JsonRequestBehavior.AllowGet);
        }
    }
}