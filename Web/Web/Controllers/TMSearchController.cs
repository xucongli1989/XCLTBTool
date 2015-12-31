using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TMSearchController : BaseController
    {
        /// <summary>
        /// 商品信息列表
        /// </summary>
        public ActionResult ProductList(long id)
        {
            Web.Models.ProductListModel viewModel = new Models.ProductListModel();
            XCLShouCang.BLL.TB_SearchKey searchKeyBLL = new XCLShouCang.BLL.TB_SearchKey();
            XCLShouCang.BLL.TB_Product productBLL = new XCLShouCang.BLL.TB_Product();
            viewModel.SearchKeyModel=searchKeyBLL.GetModel(id);
            if (null != viewModel.SearchKeyModel)
            {
                viewModel.ProductList = productBLL.GetModelList(viewModel.SearchKeyModel.SearchKeyID);
            }
            ViewBag.Title = viewModel.SearchKeyModel.KeyName;
            return View("~/Views/TMSearch/ProductList.cshtml",viewModel);
        }

        /// <summary>
        /// 根据关键字，导出商品信息
        /// </summary>
        public void ProductOutPut()
        {
            long searchKeyID = XCLNetTools.StringHander.FormHelper.GetLong("searchKeyID");
            XCLShouCang.BLL.TB_SearchKey searchKeyBLL = new XCLShouCang.BLL.TB_SearchKey();
            XCLShouCang.BLL.TB_Product productBLL = new XCLShouCang.BLL.TB_Product();

            XCLShouCang.Model.TB_SearchKey keyModel=searchKeyBLL.GetModel(searchKeyID);
            if(null!=keyModel)
            {
                DataSet ds = productBLL.GetProductDataTable(keyModel.SearchKeyID);
                XCLNetTools.DataHandler.DataToExcel.OutPutExcel(new XCLNetTools.DataHandler.OutPutParamClass()
                {
                    ds = ds,
                    AutoDownLoad=true,
                    conTitle=new string[]{ string.Format("天猫关键字【{0}】的查询结果！",keyModel.KeyName)},
                    fileTitle=Web.Common.CommonHelper.WebInfo.WebName
                });
            }
        }

        [HttpPost]
        public ActionResult GoSearch()
        {
            XCLNetTools.Message.MessageModel msgModel = new XCLNetTools.Message.MessageModel();
            DateTime dtNow=DateTime.Now;

            #region 查询时间检测
            string lastSearchTimeSession = "lastSearchTimeSession";
            DateTime? lastSearchTime = Session[lastSearchTimeSession] as DateTime?;//最近一次查询时间
            if (null != lastSearchTime)
            {
                if (DateTime.Now.Subtract(Convert.ToDateTime(lastSearchTime)).TotalSeconds <= Web.Common.CommonHelper.WebInfo.TB_SearchIntervalSecond)
                {
                    msgModel.Message = string.Format("查询过于频繁，请{0}秒后再查询！", Web.Common.CommonHelper.WebInfo.TB_SearchIntervalSecond);
                    return Json(msgModel);
                }
            }
            #endregion

            #region 参数验证
            XCLShouCang.Model.TB_SearchKey keyModel = new XCLShouCang.Model.TB_SearchKey()
            {
                KeyType=XCLNetTools.StringHander.FormHelper.GetInt("hdSearchTypeValue"),
                KeyName = XCLNetTools.StringHander.FormHelper.GetString("kw"),
                CreateTime = dtNow
            };

            if (keyModel.KeyType == (int)Common.CommonHelper.SearchTypeEnum.天猫搜索列表链接)
            {
                if (keyModel.KeyName.IndexOf("http://", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    keyModel.KeyName = string.Format("http://{0}", keyModel.KeyName);
                }

                if (!XCLNetTools.StringHander.PageValid.IsURL(keyModel.KeyName))
                {
                    msgModel.Message = "您指定的链接无效！";
                    return Json(msgModel);
                }
            }
            #endregion

            #region 执行进度监听
            string connectionID=XCLNetTools.StringHander.FormHelper.GetString("connectionID");
            Action<string> action_ShowProcess = new Action<string>((msg) =>
            {
                var currentClient = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Web.Common.SignalR.MyChatHub>().Clients.Client(connectionID);
                currentClient.showProductListProcess(new Web.Models.Common.GetProductProcessModel()
                { 
                    Message=msg
                });
            });
            #endregion

            var currentType=(Web.Common.CommonHelper.SearchTypeEnum)keyModel.KeyType;
            switch (currentType)
            { 
                case Common.CommonHelper.SearchTypeEnum.天猫搜索关键字:
                case Common.CommonHelper.SearchTypeEnum.天猫搜索列表链接:
                    this.GrabeProductFromPage(keyModel, action_ShowProcess, msgModel);
                    break;
                case Common.CommonHelper.SearchTypeEnum.淘宝账号:
                    this.GrabeBuyerInfo(keyModel, action_ShowProcess, msgModel);
                    break;
            }
            
            Session[lastSearchTimeSession] = DateTime.Now;

            if (currentType == Common.CommonHelper.SearchTypeEnum.淘宝账号 && null != msgModel.CustomObject)
            {
                return PartialView("~/Views/UserControl/BuyerSearch/BuyerInfo.cshtml", msgModel.CustomObject);
            }
            else
            {
                return Web.Controllers.BaseController.XCLJsonResult(msgModel);
            }
        }

        /// <summary>
        /// 抓取取宝贝信息
        /// </summary>
        private void GrabeProductFromPage(XCLShouCang.Model.TB_SearchKey keyModel, Action<string> action_ShowProcess, XCLNetTools.Message.MessageModel msgModel)
        {
            DateTime dtNow = DateTime.Now;
            List<XCLShouCang.Model.TB_Product> productModelList = new List<XCLShouCang.Model.TB_Product>();
            XCLShouCang.Model.TB_Product productModel = null;

            //抓取url数据
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            action_ShowProcess("准备获取宝贝信息");

            #region 获取列表页数据
            string searchUrl = string.Empty;
            switch (keyModel.KeyType)
            {
                case (int)Common.CommonHelper.SearchTypeEnum.天猫搜索列表链接:
                    searchUrl = keyModel.KeyName;
                    break;
                default:
                    searchUrl = Web.Common.CommonHelper.WebInfo.TB_ProductSearchListURL.Replace("{kw}", System.Web.HttpUtility.UrlEncode(keyModel.KeyName, System.Text.Encoding.GetEncoding("GB2312")));
                    break;
            }
            string productListHTML = Web.Common.CommonHelper.DownLoadHTMLString(searchUrl);

            doc.LoadHtml(productListHTML);

            Regex numberRegex = new Regex(@"\d+");
            HtmlAgilityPack.HtmlNodeCollection productNodes = doc.DocumentNode.SelectNodes("//div[@id='J_ItemList']/div[@class='product'][@data-id]");
            if (null != productNodes && productNodes.Count > 0)
            {
                for (int i = 0; i < productNodes.Count; i++)
                {
                    if (i == 0 || (i + 1) % 5 == 0)
                    {
                        action_ShowProcess(string.Format("正在从搜索列表中提取第【{0}】个宝贝基本信息...", i + 1));
                    }

                    var productNode = productNodes[i];
                    var productNodeWrap = productNode.SelectSingleNode("div[@class='product-iWrap']");
                    productModel = new XCLShouCang.Model.TB_Product();
                    productModel.CreateTime = dtNow;
                    productModel.Sort = i + 1;
                    productModel.ID = XCLNetTools.StringHander.Common.GetLong(null != productNode.Attributes["data-id"] ? productNode.Attributes["data-id"].Value : "");

                    //图片
                    var imgNode = productNodeWrap.SelectSingleNode("child::div[@class='productImg-wrap']/a[@class='productImg']/img");
                    if (null != imgNode)
                    {
                        if (null != imgNode.Attributes["data-ks-lazyload"])
                        {
                            productModel.ImgURL = imgNode.Attributes["data-ks-lazyload"].Value;
                        }
                        else if (null != imgNode.Attributes["src"])
                        {
                            productModel.ImgURL = imgNode.Attributes["src"].Value;
                        }
                    }
                    //价格
                    var priceNode = productNodeWrap.SelectSingleNode("child::p[@class='productPrice']/em");
                    productModel.Price = XCLNetTools.StringHander.Common.GetDecimal(null != priceNode.Attributes["title"] ? priceNode.Attributes["title"].Value : "");
                    //标题
                    var titleNode = productNodeWrap.SelectSingleNode("child::p[@class='productTitle']/a");
                    if (null == titleNode)
                    {
                        titleNode = productNodeWrap.SelectSingleNode("child::div[@class='productTitle productTitle-spu']/a");//电器城
                    }
                    productModel.Title = null != titleNode ? titleNode.InnerText : string.Empty;
                    //店铺
                    var shopNode = productNodeWrap.SelectSingleNode("child::div[@class='productShop']/a");
                    productModel.ShopName = shopNode.InnerText;
                    productModel.ShopID = XCLNetTools.StringHander.Common.GetLong(new Regex(@"(user_number_id=)(\d+)").Match(null != shopNode.Attributes["href"] ? shopNode.Attributes["href"].Value : "").Groups[2].Value);
                    
                    productModel.ProductURL = Web.Common.CommonHelper.WebInfo.TB_ProductURL.Replace("{ID}", productModel.ID.ToString());
                    productModel.ShopURL = Web.Common.CommonHelper.WebInfo.TB_ShopURL.Replace("{ShopID}", productModel.ShopID.ToString());
                    productModelList.Add(productModel);
                }

            }
            #endregion

            #region 获取单个宝贝详细数据
            Regex regRate = new Regex(@"(""gradeAvg"":)([1-9]\d*\.\d*|0\.\d*[1-9]\d*)");
            Regex regAllRateCount = new Regex(@"(""rateTotal"":)([1-9]\d*|0)");
            Regex regAreaFilter = new Regex(@"省|市|(\s+)");
            Regex regProductArea = new Regex(@"""deliveryAddress"":""(.*)"",""deliverySkuMap""");

            if (null != productModelList && productModelList.Count > 0)
            {
                for (int i = 0; i < productModelList.Count; i++)
                {
                    if (i == 0 || (i + 1) % 5 == 0)
                    {
                        action_ShowProcess(string.Format("正在提取第【{0}】个宝贝详情...", i + 1));
                    }

                    try
                    {
                        var m = productModelList[i];
                        //doc.LoadHtml(Web.Common.CommonHelper.DownLoadHTMLString(m.ProductURL));
                        //var currentDocNode = doc.DocumentNode;
                        ////商家所在地
                        //var sellNode = currentDocNode.SelectSingleNode("//li[@class='locus']");
                        //var sellNodeAreaDiv = null != sellNode ? sellNode.SelectSingleNode("div") : null;
                        //string sellAreaText = null != sellNodeAreaDiv ? sellNodeAreaDiv.InnerText : string.Empty;
                        //if (!string.IsNullOrEmpty(sellAreaText))
                        //{
                        //    string[] sellAreaArry = regAreaFilter.Replace(sellAreaText, "").Split(',');
                        //    if (sellAreaArry.Length > 1)
                        //    {
                        //        m.ShopProvince = sellAreaArry[0];
                        //        m.ShopCity = sellAreaArry[1];
                        //    }
                        //    else
                        //    {
                        //        m.ShopProvince = sellAreaArry[0];
                        //        m.ShopCity = sellAreaArry[0];
                        //    }
                        //}
                        //宝贝评分
                        //XCLTBTool({"dsr":{"gradeAvg":4.8,"rateTotal":7202}})
                        string rateHtml = Web.Common.CommonHelper.DownLoadHTMLString(Web.Common.CommonHelper.WebInfo.TB_ProductRateURL.Replace("{ID}", m.ID.ToString()));
                        m.AppraiseCount = XCLNetTools.StringHander.Common.GetInt(regAllRateCount.Match(rateHtml).Groups[2].Value);
                        m.Rate = XCLNetTools.StringHander.Common.GetDecimal(regRate.Match(rateHtml).Groups[2].Value);
                        //宝贝销量
                        string productDetailsHTML= Web.Common.CommonHelper.DownLoadHTMLString(Web.Common.CommonHelper.WebInfo.TB_ProductDetailURL.Replace("{ID}",m.ID.ToString()));
                        if (productDetailsHTML.Contains(@"""sellCount"":"))
                        {
                            m.MonthDealCount = XCLNetTools.StringHander.Common.GetInt(new Regex(@"(""sellCount"":)(\d+)").Match(productDetailsHTML).Groups[2].Value);
                        }
                        //宝贝所在地
                        if (regProductArea.Match(productDetailsHTML).Success)
                        {
                            string areaText = regProductArea.Match(productDetailsHTML).Groups[1].Value;
                            if (Web.Common.CommonHelper.WebInfo.TB_ProvinceSingle.Exists(k => string.Equals(k, areaText)))
                            {
                                m.ProductProvince = areaText;
                                m.ProductCity = areaText;
                            }
                            else
                            {
                                m.ProductProvince = Web.Common.CommonHelper.WebInfo.TB_Province.Where(k => areaText.Contains(k)).First();
                                m.ProductCity = areaText.Substring(m.ProductProvince.Length);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }

            #endregion

            action_ShowProcess("宝贝信息已获取完成");

            #region 向数据库中保存数据
            XCLShouCang.BLL.TB_SearchKey keyBLL = new XCLShouCang.BLL.TB_SearchKey();
            long keyID = keyBLL.Add(keyModel, productModelList);
            if (keyID > 0)
            {
                msgModel.IsSuccess = true;
                msgModel.CustomObject = new { KeyID = keyID, RecCount = null == productModelList ? 0 : productModelList.Count, UseSecond = (int)(DateTime.Now.Subtract(dtNow).TotalSeconds) };
                msgModel.Message = "查询成功！";
            }
            else
            {
                msgModel.IsSuccess = false;
                msgModel.Message = "查询失败，请重试！";
            }
            #endregion

        }

        /// <summary>
        /// 抓取用户信息
        /// </summary>
        /// <remarks>
        /// 步骤：
        /// 1、加载用户名片地址，获取用户名片中的加userid密文(b89034eecee41f9b87b1eac2a0faac43)（如果获取不到，也就是它有可能跳转到用户店铺首页，此时获取另一种密文UvGx0OmvyvGIu）
        /// 2、根据第1步中的密文，转到用户个人信誉页面提取数据
        /// </remarks>
        private void GrabeBuyerInfo(XCLShouCang.Model.TB_SearchKey keyModel, Action<string> action_ShowProcess, XCLNetTools.Message.MessageModel msgModel)
        {
            action_ShowProcess("正准备查询账号信息...");

            string searchUrl = string.Empty;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNode tempHTMLNode = null;
            HtmlAgilityPack.HtmlNodeCollection tempHTMLNodeCon = null;
            string pageHtml = string.Empty;
            XCLShouCang.Model.TB_BuyerInfo buyerModel = new XCLShouCang.Model.TB_BuyerInfo();
            Regex regNumber = new Regex(@"[-\+]?\d+(\.\d+)?");

            #region 基本信息
            action_ShowProcess("正在查询基本信息...");

            Regex regUserID = new Regex("(user_id=)(.*)");
            Regex regArea = new Regex("(：)(.*)");
            Regex regReg=new Regex(@"(\d{4}.\d{2}.\d{2})");
            

           searchUrl = Web.Common.CommonHelper.WebInfo.TB_UserProfileURL.Replace("{UID}", System.Web.HttpUtility.UrlEncode(keyModel.KeyName, System.Text.Encoding.GetEncoding("GB2312")));

            pageHtml =Web.Common.CommonHelper.DownLoadHTMLString(searchUrl);
            doc.LoadHtml(pageHtml);

            buyerModel.NickName = (keyModel.KeyName??"").Trim();

            //名片
            var nameCardNode = doc.DocumentNode.SelectSingleNode("//div[@id='NameCard']");
            if (null != nameCardNode)
            {
                //店铺
                var shopNode = nameCardNode.SelectSingleNode("//li[contains(text(),'淘宝店铺')]/a");
                if (null != shopNode)
                {
                    buyerModel.ShopName = (shopNode.InnerText??"").Trim();
                    buyerModel.ShopURL = shopNode.GetAttributeValue("href", string.Empty);
                }
                //UID
                buyerModel.UserID = regUserID.Match(buyerModel.ShopURL).Groups[2].Value;
                //注册时间
                tempHTMLNode = nameCardNode.SelectSingleNode("//li[contains(text(),'注册时间')]");
                if (null != tempHTMLNode)
                {
                    buyerModel.RegTime = XCLNetTools.StringHander.Common.GetDateTimeNullable(regReg.Match(tempHTMLNode.InnerText.Trim()).Groups[1].Value);
                }

                //认证
                tempHTMLNode = nameCardNode.SelectSingleNode("//li[contains(text(),'认证情况')]/a/img");
                if (null != tempHTMLNode)
                {
                    buyerModel.CertificateLinkImg = tempHTMLNode.GetAttributeValue("src", string.Empty);
                    buyerModel.CertificateType = (tempHTMLNode.GetAttributeValue("title", string.Empty)??"").Trim();
                }

                //所在地
                tempHTMLNode = nameCardNode.SelectSingleNode("//li[contains(text(),'所 在 地')]");
                if (null != tempHTMLNode)
                {
                    buyerModel.Area = (regArea.Match(tempHTMLNode.InnerText.Trim()).Groups[2].Value??"").Trim();
                }
            }
            else
            {
                //此时可能被跳转到店铺首页去了
                Match userIDMT = new Regex(@"user-rate-([A-Za-z0-9]+)\.htm").Match(pageHtml);//UvGx0OmvyvGIu
                if (userIDMT.Success)
                {
                    buyerModel.UserID = (userIDMT.Groups[1].Value??"").Trim();
                    buyerModel.ShopName = new Regex("首页|-|淘宝网").Replace(doc.DocumentNode.SelectSingleNode("//title").InnerText, "").Trim();
                }
            }

            if (string.IsNullOrEmpty(buyerModel.UserID))
            { 
                msgModel.Message = "未找到该用户信息！";
                msgModel.IsSuccess = false;
                return;
            }

            #endregion

            #region 信用首页获取其它信息

            action_ShowProcess("正在查询信用信息...");

            pageHtml = Web.Common.CommonHelper.DownLoadHTMLString(Web.Common.CommonHelper.WebInfo.TB_UserRateHomeURL.Replace("{UID}", buyerModel.UserID.ToString()));
            doc.LoadHtml(pageHtml);
            var docNode = doc.DocumentNode;

            //买家 http://rate.taobao.com/user-rate-49095164e83c97b0a713aa536ea1cbc5.htm
            //卖家 http://rate.taobao.com/user-rate-b89034eecee41f9b87b1eac2a0faac43.htm
            var personNode = docNode.SelectSingleNode("//div[contains(@class,'personal-info-box')]");

            buyerModel.ShopCreateTime = XCLNetTools.StringHander.Common.GetDateTimeNullable(docNode.SelectSingleNode("//input[@id='J_showShopStartDate']").GetAttributeValue("value", string.Empty));
            buyerModel.IsSeller = null != buyerModel.ShopCreateTime;

            if (buyerModel.IsSeller)
            {
                tempHTMLNode = docNode.SelectSingleNode("//li[contains(text(),'当前主营')]");
                if (null != tempHTMLNode)
                {
                    buyerModel.ShopMainType = (tempHTMLNode.SelectSingleNode("a").InnerText??"").Trim();
                }
                tempHTMLNode=docNode.SelectSingleNode("//input[@id='J_ShopIdHidden']");
                if (null != tempHTMLNode)
                {
                    buyerModel.ShopID = XCLNetTools.StringHander.Common.GetLong(tempHTMLNode.GetAttributeValue("value", string.Empty));
                }
                tempHTMLNode=docNode.SelectSingleNode(string.Format("//a[contains(text(),'{0}')]",buyerModel.NickName));
                if(null!=tempHTMLNode)
                {
                    buyerModel.ShopURL = (tempHTMLNode.GetAttributeValue("href", string.Empty)??"").Trim();
                }

                tempHTMLNode = docNode.SelectSingleNode("//li[contains(text(),'卖家信用')]");
                if (null != tempHTMLNode)
                {
                    buyerModel.SellerCreditValue = XCLNetTools.StringHander.Common.GetInt(regNumber.Match(tempHTMLNode.InnerText).Value);
                    if (buyerModel.SellerCreditValue > 0)
                    {
                        var sellerCreditImgNode = tempHTMLNode.SelectSingleNode("child::a/img");
                        if (null != sellerCreditImgNode)
                        {
                            buyerModel.SellerCreditImg = (sellerCreditImgNode.GetAttributeValue("src", string.Empty)??"").Trim();
                        }
                    }
                }

                var buyerCreditNode = docNode.SelectSingleNode("//li[contains(text(),'买家信用')]");
                buyerModel.BuyerCreditValue = XCLNetTools.StringHander.Common.GetInt(regNumber.Match(buyerCreditNode.InnerText).Value);
                if (buyerModel.BuyerCreditValue > 0)
                {
                    tempHTMLNode=buyerCreditNode.SelectSingleNode("child::a/img");
                    if(null!=tempHTMLNode)
                    {
                        buyerModel.BuyerCreditImg = (tempHTMLNode.GetAttributeValue("src", string.Empty)??"").Trim();
                    }
                }
            }
            else
            {
                var buyerCreditNode = docNode.SelectSingleNode("//li[contains(text(),'买家信用')]");
                buyerModel.BuyerCreditValue = XCLNetTools.StringHander.Common.GetInt(buyerCreditNode.SelectSingleNode("child::a[@id='J_BuyerRate']").InnerText.Trim());
                if (buyerModel.BuyerCreditValue > 0)
                {
                    buyerModel.BuyerCreditImg = (buyerCreditNode.SelectSingleNode(".//img").GetAttributeValue("src", string.Empty)??"").Trim();
                }
            }


            //认证信息
            if (string.IsNullOrEmpty(buyerModel.CertificateType))
            {
                tempHTMLNodeCon = docNode.SelectNodes("//li[contains(text(),'认证信息')]/a/img | //dt[contains(text(),'认证信息')]");
                if (null != tempHTMLNodeCon && tempHTMLNodeCon.Count>0)
                {
                    foreach (var m in tempHTMLNodeCon)
                    {
                        buyerModel.CertificateLinkImg = (m.GetAttributeValue("src", string.Empty)??"").Trim();
                        buyerModel.CertificateType = (m.GetAttributeValue("title", string.Empty)??"").Trim();
                        if (string.IsNullOrEmpty(buyerModel.CertificateType) && string.Equals(m.Name, "dt", StringComparison.OrdinalIgnoreCase))
                        {
                            var cerImgNode=m.SelectSingleNode("following-sibling::dd/a/img");
                            if(null!=cerImgNode)
                            {
                                buyerModel.CertificateLinkImg = (cerImgNode.GetAttributeValue("src", string.Empty)??"").Trim();
                                buyerModel.CertificateType = (cerImgNode.GetAttributeValue("title", string.Empty)??"").Trim();
                            }
                        }
                        if (!string.IsNullOrEmpty(buyerModel.CertificateType))
                        {
                            break;
                        }
                    }
                }
            }
            //所在地
            if (string.IsNullOrEmpty(buyerModel.Area))
            {
                tempHTMLNodeCon = docNode.SelectNodes("//li[contains(text(),'所在地区')] | //dt[contains(text(),'所在地区')]");
                if (null != tempHTMLNodeCon && tempHTMLNodeCon.Count > 0)
                {
                    foreach (var m in tempHTMLNodeCon)
                    {
                        buyerModel.Area = (new Regex(@"所在地区：|\s+").Replace(m.InnerText, "")??"").Trim();
                        if (string.IsNullOrEmpty(buyerModel.Area) && string.Equals(m.Name,"dt",StringComparison.OrdinalIgnoreCase))
                        {
                            buyerModel.Area = m.SelectSingleNode("following-sibling::dd").InnerText.Trim();
                        }
                        if (!string.IsNullOrEmpty(buyerModel.Area))
                        {
                            break;
                        }
                    }
                }
            }

            //评价信息节点
            var appraiseNode = docNode.SelectSingleNode("//div[contains(@class,'personal-rating')]//div[contains(@class,'rate-box')]");
            //好评率
            var goodRateNode = appraiseNode.SelectSingleNode("//em[contains(text(),'好评率')]");
            if (null != goodRateNode)
            {
                buyerModel.GoodAppraiseRate = XCLNetTools.StringHander.Common.GetDecimal(regNumber.Match(goodRateNode.InnerText).Value);
            }

            //评价统计节点
            var appraiseInfoNode = appraiseNode.SelectNodes("//div[@id='J_menu_list']//ul//li");
            if (null != appraiseInfoNode && appraiseInfoNode.Count > 0)
            {
                var appraiseCountList = new List<XCLShouCang.Model.TB_AppraiseCount>();
                var regGetCount = new Regex(@"\[(\d+),");
                foreach (var m in appraiseInfoNode)
                {
                    //[[5,{'rater':'1','direction':'0','result':'1','timeLine':'-7'}],[0,{'rater':'1','direction':'0','result':'0','timeLine':'-7'}],[0,{'rater':'1','direction':'0','result':'-1','timeLine':'-7'}]]
                    string relValue = m.GetAttributeValue("rel", string.Empty);
                    string mText = m.InnerText.Trim();
                    var matchs = regGetCount.Matches(relValue);
                    if (mText.Contains("最近一周"))
                    {
                        appraiseCountList.Add(new XCLShouCang.Model.TB_AppraiseCount()
                        {
                            AppraiseTimeType = Web.Common.CommonHelper.AppraiseTimeTypeEnum.最近一周.ToString(),
                            GoodCount = XCLNetTools.StringHander.Common.GetInt(matchs[0].Groups[1].Value),
                            MiddleCount = XCLNetTools.StringHander.Common.GetInt(matchs[1].Groups[1].Value),
                            BadCount = XCLNetTools.StringHander.Common.GetInt(matchs[2].Groups[1].Value)
                        });
                    }
                    else if (mText.Contains("最近一月"))
                    {
                        appraiseCountList.Add(new XCLShouCang.Model.TB_AppraiseCount()
                        {
                            AppraiseTimeType = Web.Common.CommonHelper.AppraiseTimeTypeEnum.最近一月.ToString(),
                            GoodCount = XCLNetTools.StringHander.Common.GetInt(matchs[0].Groups[1].Value),
                            MiddleCount = XCLNetTools.StringHander.Common.GetInt(matchs[1].Groups[1].Value),
                            BadCount = XCLNetTools.StringHander.Common.GetInt(matchs[2].Groups[1].Value)
                        });
                    }
                    else if (mText.Contains("最近半年"))
                    {
                        appraiseCountList.Add(new XCLShouCang.Model.TB_AppraiseCount()
                        {
                            AppraiseTimeType = Web.Common.CommonHelper.AppraiseTimeTypeEnum.最近半年.ToString(),
                            GoodCount = XCLNetTools.StringHander.Common.GetInt(matchs[0].Groups[1].Value),
                            MiddleCount = XCLNetTools.StringHander.Common.GetInt(matchs[1].Groups[1].Value),
                            BadCount = XCLNetTools.StringHander.Common.GetInt(matchs[2].Groups[1].Value)
                        });
                    }
                    else if (mText.Contains("半年以前"))
                    {
                        if (buyerModel.IsSeller)
                        {
                            var tmpRate = m.SelectNodes("//a[@data-point-val][contains(@href,'timeLine|-211')]");
                            if (null != tmpRate && tmpRate.Count ==3)
                            {
                                appraiseCountList.Add(new XCLShouCang.Model.TB_AppraiseCount()
                                {
                                    AppraiseTimeType = Web.Common.CommonHelper.AppraiseTimeTypeEnum.半年以前.ToString(),
                                    GoodCount = XCLNetTools.StringHander.Common.GetInt(tmpRate[0].InnerText.Trim()),
                                    MiddleCount = XCLNetTools.StringHander.Common.GetInt(tmpRate[1].InnerText.Trim()),
                                    BadCount = XCLNetTools.StringHander.Common.GetInt(tmpRate[2].InnerText.Trim())
                                });            
                            }
                        }
                        else
                        {
                            //[[260,'http://ratehis.taobao.com/user-rate-UvFkbMmQyMGHL--isarchive|true--detailed|1--goodNeutralOrBad|1--timeLine|-211--receivedOrPosted|0--buyerOrSeller|1.htm#RateType'],[0,'http://ratehis.taobao.com/user-rate-UvFkbMmQyMGHL--isarchive|true--detailed|1--goodNeutralOrBad|0--timeLine|-211--receivedOrPosted|0--buyerOrSeller|1.htm#RateType'],[0,'http://ratehis.taobao.com/user-rate-UvFkbMmQyMGHL--isarchive|true--detailed|1--goodNeutralOrBad|-1--timeLine|-211--receivedOrPosted|0--buyerOrSeller|1.htm#RateType']]
                            appraiseCountList.Add(new XCLShouCang.Model.TB_AppraiseCount()
                            {
                                AppraiseTimeType = Web.Common.CommonHelper.AppraiseTimeTypeEnum.半年以前.ToString(),
                                GoodCount = XCLNetTools.StringHander.Common.GetInt(matchs[0].Groups[1].Value),
                                MiddleCount = XCLNetTools.StringHander.Common.GetInt(matchs[1].Groups[1].Value),
                                BadCount = XCLNetTools.StringHander.Common.GetInt(matchs[2].Groups[1].Value)
                            });                            
                        }
                    }
                }
                if (buyerModel.IsSeller)
                {
                    buyerModel.SellerAppraiseCountList = appraiseCountList;
                }
                else
                {
                    buyerModel.BuyerAppraiseCountList = appraiseCountList;
                }
            }




            #endregion

            #region 向数据库中保存数据
            XCLShouCang.BLL.TB_SearchKey keyBLL = new XCLShouCang.BLL.TB_SearchKey();
            keyBLL.Add(keyModel);
            #endregion

            msgModel.CustomObject = buyerModel;
            msgModel.IsSuccess = true;
            return;

        }
    }
}
