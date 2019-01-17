using Map4D.Data.BO;
using Map4D.Models;
using Map4D.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Map4D.Controllers
{
    public class PolygonDetailController : Controller
    {
        private PolygonDetailBo polygonDetailBo = new PolygonDetailBo();
        // GET: PolygonDetail
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDetailByLatLng(string lat,string lng)
        {
            return Json(polygonDetail.GetDetailByLatLng(lat, lng), JsonRequestBehavior.AllowGet);
        }
        public ActionResult TestTonTai(string lng, string lat)
        {
            if(string.IsNullOrEmpty(lng) || string.IsNullOrEmpty(lat))
            {
                return View();
            }
            List<PolygonDetailViewModel> listPolygonDetail = polygonDetail.GetDetailByLatLng(lat, lng);
            List<TestTonTaiViewModel> listTestTonTai = new List<TestTonTaiViewModel>();
            PointViewModel pointKiemTra = new PointViewModel() {Lng = double.Parse(lng),Lat = double.Parse(lat) };
            foreach(PolygonDetailViewModel polygon in listPolygonDetail)
            {
                bool result = polygonDetail.KiemTraTonTai(polygon, pointKiemTra);
                listTestTonTai.Add(new TestTonTaiViewModel() { DuLieuDoiTuong = polygon.DuLieuDoiTuong,TonTai = result});
            }
            ViewBag.listTestTonTai = listTestTonTai;
            return View();
        }
    }
}