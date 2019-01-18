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
        public ActionResult Index(string cityId,string districtId,string code)
        {
            string currentCityId=string.Empty;
            string currentDistrictId=string.Empty;

            IEnumerable<CountriesViewModel> listDistricts=null;
            IEnumerable<CountriesViewModel> listWards=null;
            if (cityId!=null)
            {
                currentCityId = cityId;
                listDistricts = drawPolygonBo.GetAllWardByDistrict(int.Parse(cityId));
            }
            if (districtId != null)
            {
                currentDistrictId = districtId;
                listWards = drawPolygonBo.GetAllWardByDistrict(int.Parse(districtId));
            }
            if (code!=null)
            {
                var shapes = drawPolygonBo.getDuLieuDoiTuongByCode(code);
            }

            var listCity = drawPolygonBo.GetAllCity();
            var DrawPolygonViewModel = new DrawPolygonViewModels
            {
                CurrentCityId=currentCityId,
                CurrentDistrictId=currentDistrictId,
                Cities = listCity,
                Districts = listDistricts,
                Wards = listWards,
            };
            return View(DrawPolygonViewModel);
        }
        public ActionResult GetShapesByCode(string code)
        {
            var shapes = drawPolygonBo.getDuLieuDoiTuongByCode(code);
            return Json(new
            {
                data=shapes
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListCity()
        {
            var listCity = drawPolygonBo.GetAllCity();
            return Json(new
            {
                data = listCity
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListDictrict(string cityId)
        {
            var listDistrict = drawPolygonBo.GetAllDistrictByCity(int.Parse(cityId));
            return Json(new
            {
                data = listDistrict
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListWard(string dictrictId)
        {
            
            var listWard = drawPolygonBo.GetAllWardByDistrict(int.Parse(dictrictId));
          
            return Json(new
            {
                data = listWard
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult getWard(string code)
        {
            var shapes = drawPolygonBo.getDuLieuDoiTuongByCode(code);
            
            return Json(new
            {
                shapes
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetShapes(string code)
        {
            var shapes = drawPolygonBo.getDuLieuDoiTuongByCode(code);
            PointViewModel pointCenter = drawPolygonBo.GetPointCenterByCode(code);
            return Json(new
            {
                shapes = shapes, pointCenter = pointCenter
            }, JsonRequestBehavior.AllowGet);
        }
    }
}