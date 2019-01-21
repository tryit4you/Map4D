using Map4D.Data.BO;
using System.Web.Mvc;

namespace Map4D.Controllers
{
    public class PolygonDetailController : Controller
    {
        private PolygonDetailBo polygonDetail = new PolygonDetailBo();

        /// <summary>
        /// GET: PolygonDetail Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get InfoPoint by LatLng
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public JsonResult GetDetailByLatLng(string lat, string lng)
        {
            var details = polygonDetail.GetInfoPointByLatLng(lat, lng);
            return Json(new
            {
                details
            },JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get Html Info Polygon by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult GetDetailObject(string code)
        {
            string html = polygonDetail.GetPopupHtmlByCode(code);
            return Json(new
            {
                htmlCode = html
            },JsonRequestBehavior.AllowGet);
        }
    }
}