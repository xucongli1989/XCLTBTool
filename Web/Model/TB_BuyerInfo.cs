using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCLShouCang.Model
{
    public class TB_BuyerInfo
    {
        /// <summary>
        /// 账号名
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// gb2312编码的账号
        /// </summary>
        public string EncodeNickName
        {
            get
            {
                return System.Web.HttpUtility.UrlEncode(this.NickName, System.Text.Encoding.GetEncoding("GB2312"));
            }
        }

        /// <summary>
        /// uid guid string 
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserTypeName
        {
            get
            {
                return this.IsSeller ? "卖家" : "买家";
            }
        }

        /// <summary>
        /// 是否为卖家
        /// </summary>
        public bool IsSeller { get; set; }

        /// <summary>
        /// 实名认证类型
        /// </summary>
        public string CertificateType { get; set; }

        /// <summary>
        /// 认证链接图片
        /// </summary>
        public string CertificateLinkImg { get; set; }

        /// <summary>
        /// 店铺名
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 店铺链接
        /// </summary>
        public string ShopURL { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        public long ShopID { get; set; }

        /// <summary>
        /// 卖家信用值
        /// </summary>
        public int SellerCreditValue { get; set; }

        /// <summary>
        /// 卖家信用图片
        /// </summary>
        public string SellerCreditImg { get; set; }

        /// <summary>
        /// 买家信用值
        /// </summary>
        public int BuyerCreditValue { get; set; }

        /// <summary>
        /// 买家信用图片
        /// </summary>
        public string BuyerCreditImg { get; set; }

        /// <summary>
        /// 所在地
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegTime { get; set; }

        /// <summary>
        /// 创店时间
        /// </summary>
        public DateTime? ShopCreateTime { get; set; }

        /// <summary>
        /// 当前主营
        /// </summary>
        public string ShopMainType { get; set; }

        /// <summary>
        /// 店铺服务
        /// </summary>
        public string ShopServices { get; set; }

        /// <summary>
        /// 保证金
        /// </summary>
        public decimal ShopCashFund { get; set; }

        /// <summary>
        /// 好评率
        /// </summary>
        public decimal GoodAppraiseRate { get; set; }

        /// <summary>
        /// 买家评价数列表
        /// </summary>
        public List<TB_AppraiseCount> BuyerAppraiseCountList { get; set; }

        /// <summary>
        /// 卖家评价数列表
        /// </summary>
        public List<TB_AppraiseCount> SellerAppraiseCountList { get; set; }
    }
}
