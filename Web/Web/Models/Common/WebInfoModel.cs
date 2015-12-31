using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Common
{
    public class WebInfoModel
    {
        public List<string> WebURL { get; set; }

        public string WebMainURL { get; set; }

        public string WebName { get; set; }

        public string Title { get; set; }

        public string KeyWords { get; set; }

        public string Description { get; set; }

        public string SimpleDescription { get; set; }

        public string AdminEmail { get; set; }

        public string CopyRight { get; set; }



        public string TB_ProductSearchListURL { get; set; }
        public string TB_ProductURL { get; set; }
        public string TB_ProductDetailURL { get; set; }
        public string TB_ShopURL { get; set; }
        public string TB_ProductRateURL { get; set; }
        public string TB_UserProfileURL { get; set; }
        public string TB_UserRateHomeURL { get; set; }
        public int TB_SearchIntervalSecond { get; set; }
        public List<string> TB_Province { get; set; }
        public List<string> TB_ProvinceSingle { get; set; }
    }
}