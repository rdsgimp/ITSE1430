/* Christopher Hurley
 * ITSE 1430
 * Lab 5
 * 4 May, 2018
 */
using System.Web.Mvc;

namespace ChristopherHurley.MovieLib.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}