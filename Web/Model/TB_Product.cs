using System;
namespace XCLShouCang.Model
{
    /// <summary>
    /// TB_Product:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_Product
    {
        public TB_Product()
        { }
        #region Model
        private long _productid;
        private long _fk_searchkeyid;
        private long _id;
        private string _producturl;
        private int _sort;
        private string _imgurl;
        private decimal _price;
        private string _title;
        private int _monthdealcount;
        private int _appraisecount;
        private string _shopname;
        private long _shopid;
        private string _shopurl;
        private string _shopprovince;
        private string _shopcity;
        private string _productprovince;
        private string _productcity;
        private decimal _rate = 0M;
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public long ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 关键字ID
        /// </summary>
        public long FK_SearchKeyID
        {
            set { _fk_searchkeyid = value; }
            get { return _fk_searchkeyid; }
        }
        /// <summary>
        /// 淘宝商品ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 商品链接
        /// </summary>
        public string ProductURL
        {
            set { _producturl = value; }
            get { return _producturl; }
        }
        /// <summary>
        /// 搜索排名
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 商品图片链接
        /// </summary>
        public string ImgURL
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 商品名
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 月成交
        /// </summary>
        public int MonthDealCount
        {
            set { _monthdealcount = value; }
            get { return _monthdealcount; }
        }
        /// <summary>
        /// 评价数
        /// </summary>
        public int AppraiseCount
        {
            set { _appraisecount = value; }
            get { return _appraisecount; }
        }
        /// <summary>
        /// 所属店铺名
        /// </summary>
        public string ShopName
        {
            set { _shopname = value; }
            get { return _shopname; }
        }
        /// <summary>
        /// 所属店铺ID
        /// </summary>
        public long ShopID
        {
            set { _shopid = value; }
            get { return _shopid; }
        }
        /// <summary>
        /// 所属店铺URL
        /// </summary>
        public string ShopURL
        {
            set { _shopurl = value; }
            get { return _shopurl; }
        }
        /// <summary>
        /// 店铺所在省
        /// </summary>
        public string ShopProvince
        {
            set { _shopprovince = value; }
            get { return _shopprovince; }
        }
        /// <summary>
        /// 店铺所在城市
        /// </summary>
        public string ShopCity
        {
            set { _shopcity = value; }
            get { return _shopcity; }
        }
        /// <summary>
        /// 商品所在省
        /// </summary>
        public string ProductProvince
        {
            set { _productprovince = value; }
            get { return _productprovince; }
        }
        /// <summary>
        /// 商品所在城市
        /// </summary>
        public string ProductCity
        {
            set { _productcity = value; }
            get { return _productcity; }
        }
        /// <summary>
        /// 评分
        /// </summary>
        public decimal Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}

