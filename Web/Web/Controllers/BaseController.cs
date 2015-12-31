using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public static JsonResult XCLJsonResult(object data)
        {
            return new Web.Common.XCLJsonResult { Data = data};
        }
    }
}
