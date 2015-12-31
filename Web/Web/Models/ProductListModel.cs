using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ProductListModel
    {
        public XCLShouCang.Model.TB_SearchKey SearchKeyModel { get; set; }

        public List<XCLShouCang.Model.TB_Product> ProductList { get; set; }
    }
}