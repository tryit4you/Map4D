using Map4D.Data.BO;
using Map4D.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Map4D.Controllers
{
    public class DrawPolygonController : Controller
    {
        private DrawPolygonBo drawPolygonBo = new DrawPolygonBo();
        /// <summary>
        /// GET : DrawPolygon Index
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="districtId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
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
            //if (code!=null)
            //{
            //    var shapes = drawPolygonBo.GetObjectDataByCode(code);
            //}

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
        /// <summary>
        /// Get All City in VietNam
        /// </summary>
        /// <returns></returns>
        public JsonResult ListCity()
        {
            var listCity = drawPolygonBo.GetAllCity();
            return Json(new
            {
                data = listCity
            },JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get All District by cityId
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public JsonResult ListDictrict(string cityId)
        {
            var listDistrict = drawPolygonBo.GetAllDistrictByCity(int.Parse(cityId));
            return Json(new
            {
                data = listDistrict
            },JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get All Ward by districtId
        /// </summary>
        /// <param name="dictrictId"></param>
        /// <returns></returns>
        public JsonResult ListWard(string dictrictId)
        {
            
            var listWard = drawPolygonBo.GetAllWardByDistrict(int.Parse(dictrictId));
          
            return Json(new
            {
                data = listWard
            },JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get ObjectData and PointCenter by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult GetShapes(string code)
        {
            var shapes = drawPolygonBo.GetObjectDataByCode(code);
            PointViewModel pointCenter = drawPolygonBo.GetPointCenterByCode(code);
            return Json(new
            {
                shapes,
                pointCenter
            }, JsonRequestBehavior.AllowGet);
        }
    }
}