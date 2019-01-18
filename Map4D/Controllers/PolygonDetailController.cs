using Map4D.Data.BO;
using Map4D.Models;
using Map4D.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Map4D.Controllers
{
    public class PolygonDetailController : Controller
    {
        private PolygonDetailBo polygonDetail = new PolygonDetailBo();

        // GET: PolygonDetail
        public ActionResult Index()
        {
            return View();
        }
    }
}