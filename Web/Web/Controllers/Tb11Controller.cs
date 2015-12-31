using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class Tb11Controller : BaseController
    {
        //
        // GET: /D11/

        public ActionResult Index(string typeName)
        {
            typeName = string.IsNullOrEmpty(typeName) ? "家居文化" : Microsoft.JScript.GlobalObject.unescape(typeName);

            XCLShouCang.BLL.TB_11 bll = new XCLShouCang.BLL.TB_11();
            Web.Models.Tb11Model viewModel = new Models.Tb11Model();

            viewModel.CurrentTypeName = typeName;
            viewModel.DataList = bll.GetListByTypeName(viewModel.CurrentTypeName);
            viewModel.TypeNameList = bll.GetTypeNameList();

            return View("~/Views/Tb11/Index.cshtml",viewModel);
        }

    }
}
