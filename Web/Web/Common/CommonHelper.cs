using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Web.Common
{
    public class CommonHelper
    {

        /// <summary>
        /// 网站根路径
        /// </summary>
        public static readonly string RootURL=XCLNetTools.StringHander.Common.RootURL;

        /// <summary>
        /// 网站基本信息
        /// </summary>
        public static readonly Web.Models.Common.WebInfoModel WebInfo = new Models.Common.WebInfoModel()
        {
                Description = XCLNetTools.XML.ConfigClass.GetConfigString("Description"),
                SimpleDescription = XCLNetTools.XML.ConfigClass.GetConfigString("SimpleDescription"),
                KeyWords = XCLNetTools.XML.ConfigClass.GetConfigString("KeyWords"),
                Title = XCLNetTools.XML.ConfigClass.GetConfigString("Title"),
                WebName = XCLNetTools.XML.ConfigClass.GetConfigString("WebName"),
                WebURL = XCLNetTools.XML.ConfigClass.GetConfigString("WebURL").Split(',').ToList(),
                AdminEmail = XCLNetTools.XML.ConfigClass.GetConfigString("AdminEmail"),
                CopyRight = XCLNetTools.XML.ConfigClass.GetConfigString("CopyRight").Replace("{Year}",DateTime.Now.Year.ToString()),
                WebMainURL = XCLNetTools.XML.ConfigClass.GetConfigString("WebMainURL"),

                TB_ProductSearchListURL = XCLNetTools.XML.ConfigClass.GetConfigString("TB_ProductSearchListURL"),
                TB_ProductURL = XCLNetTools.XML.ConfigClass.GetConfigString("TB_ProductURL"),
                TB_ProductDetailURL=XCLNetTools.XML.ConfigClass.GetConfigString("TB_ProductDetailURL"),
                TB_Province = XCLNetTools.XML.ConfigClass.GetConfigString("TB_Province").Split(',').ToList(),
                TB_ProvinceSingle = XCLNetTools.XML.ConfigClass.GetConfigString("TB_ProvinceSingle").Split(',').ToList(),
                TB_SearchIntervalSecond = XCLNetTools.XML.ConfigClass.GetConfigInt("TB_SearchIntervalSecond"),
                TB_ShopURL = XCLNetTools.XML.ConfigClass.GetConfigString("TB_ShopURL"),
                TB_ProductRateURL = XCLNetTools.XML.ConfigClass.GetConfigString("TB_ProductRateURL"),
                TB_UserProfileURL=XCLNetTools.XML.ConfigClass.GetConfigString("TB_UserProfileURL"),
                TB_UserRateHomeURL=XCLNetTools.XML.ConfigClass.GetConfigString("TB_UserRateHomeURL")
        };

        /// <summary>
        /// 搜索类型list
        /// </summary>
        public static readonly List<XCLNetTools.Enum.EnumFieldModel> SearchTypeEnumList=XCLNetTools.Enum.EnumHelper.GetEnumFieldModelList(typeof(Web.Common.CommonHelper.SearchTypeEnum));
        public enum SearchTypeEnum
        {
            [Description("指定淘宝账号，如：dadianzhu")]
            淘宝账号 = 2,
            [Description("指定关键字，如：手机")]
            天猫搜索关键字 = 0,
            [Description("指定链接，如：http://list.tmall.com/search_product.htm?q=iphone")]
            天猫搜索列表链接 = 1
        }

        /// <summary>
        /// 评价时间类型
        /// </summary>
        public enum AppraiseTimeTypeEnum
        { 
            最近一周,
            最近一月,
            最近半年,
            半年以前
        }

        /// <summary>
        /// 下载url页面
        /// </summary>
        public static string DownLoadHTMLString(string url)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Headers.Add(System.Net.HttpRequestHeader.Referer,string.Format("http://www.{0}.com",XCLNetTools.StringHander.RandomHelper.GenerateStringId()));
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0");
            wc.Headers.Add("Cookie", "cna=yng1Dan4xioCAT2qk5SqPnmX; isg=4F9ED6A44E7141E8B086B9CF7922DEB3");
            string html=wc.DownloadString(url);
            html = new Regex(@">\s+<").Replace(html, "><");
            return html;
        }
    }
}

