using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductReportController : BaseController
    {
        //
        // GET: /ProductReport/

        public ActionResult Index(long id)
        {
            Web.Models.ProductReportModel viewModel = new Models.ProductReportModel();
            XCLShouCang.BLL.TB_SearchKey searchKeyBLL = new XCLShouCang.BLL.TB_SearchKey();
            viewModel.SearchKeyModel = searchKeyBLL.GetModel(id);
            return View("~/Views/TMSearch/ProductReport.cshtml", viewModel);
        }

        public ActionResult GetTopProductCount()
        {
            XCLShouCang.BLL.TB_Product productBLL = new XCLShouCang.BLL.TB_Product();
            long searchKeyID = XCLNetTools.StringHander.FormHelper.GetLong("searchKeyID");
            List<XCLShouCang.Model.TB_ProductCountByShop> lst= productBLL.GetTopProductCountReport(searchKeyID);
            return Json(lst,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductCountGroupProvince()
        {
            XCLShouCang.BLL.TB_Product productBLL = new XCLShouCang.BLL.TB_Product();
            long searchKeyID = XCLNetTools.StringHander.FormHelper.GetLong("searchKeyID");
            List<XCLShouCang.Model.TB_ProductCountGroupProvince> lst = productBLL.GetProductCountGroupProvince(searchKeyID);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }


    }
}
