using Map4D.Data.BO;
using Map4D.Models;
using Map4D.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Map4D.Controllers
{
    public class DrawPolygonController : Controller
    {
        private DrawPolygonBo drawPolygonBo = new DrawPolygonBo();
        // GET: DrawPolygon
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllData()
        {
            var allData = drawPolygonBo.GetAllData();
            var jsonResult = Json(new
            {
                data = allData
            },JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult GetAllDataPartial()
        {
            var listCity = drawPolygonBo.GetAllCity();
            var listDistrict = drawPolygonBo.GetAllDistrict(2);
            var listWard = drawPolygonBo.GetAllWard(3);
            var drawPolygonViewModel = new DrawPolygonViewModels
            {
                Cities = listCity,
                Districts = listDistrict,
                Wards = listWard
            };
            return View(drawPolygonViewModel);
        }
        public ActionResult GetShapesByCode(string code)
        {
            var shapes = drawPolygonBo.GetDuLieuDoiTuongByCode(code);
            return Json(new
            {
                data = shapes
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListCity()
        {
            var listCity = drawPolygonBo.GetAllCity();
            return Json(new
            {
                data = listCity
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListDictrict(string cityId)
        {
            var listDistrict = drawPolygonBo.GetAllDistrictByCity(int.Parse(cityId));
            return Json(new
            {
                data = listDistrict
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListWard(string dictrictId)
        {

            var listWard = drawPolygonBo.GetAllWardByDistrict(int.Parse(dictrictId));

            return Json(new
            {
                data = listWard
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getWard(string code)
        {
            var shapes = drawPolygonBo.GetDuLieuDoiTuongByCode(code);

            return Json(new
            {
                shapes
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetShapes(string code)
        {
            var shapes = drawPolygonBo.GetDuLieuDoiTuongByCode(code);
            PointViewModel pointCenter = drawPolygonBo.GetPointCenterByCode(code);
            return Json(new
            {
                shapes,
                pointCenter
            }, JsonRequestBehavior.AllowGet);
        }
    }
}