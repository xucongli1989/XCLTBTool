using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ExpressController : Controller
    {
        //
        // GET: /Express/

        public ActionResult Index()
        {
            return View("~/Views/Express/ExpressSearch.cshtml");
        }

    }
}
