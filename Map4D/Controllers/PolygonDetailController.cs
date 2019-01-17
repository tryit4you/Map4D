using Map4D.Data.BO;
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
        public ActionResult GetDetail(string lat,string lng)
        {
            var data = polygonDetailBo.GetDetailByLatLng(lat, lng);
            return Json(new
            {
                data
            });
        }
    }
}