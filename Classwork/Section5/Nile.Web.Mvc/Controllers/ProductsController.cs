using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nile.Web.Mvc.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            //return View();
            // JsonRequestBehavior//

             return Json(new { Name = "Index" });

            //return Content("Index");
        }
    }
}