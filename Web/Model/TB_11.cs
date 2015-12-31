using System;
namespace XCLShouCang.Model
{
    /// <summary>
    /// TB_11:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_11
    {
        public TB_11()
        { }
        #region Model
        private long _id;
        private string _分类;
        private string _品类;
        private string _商品名称;
        private string _imgurl;
        private string _专柜价;
        private string _市场价;
        private string _双11价;
        private string _双11t4会员价;
        private string _折扣力度;
        private string _是否有赠;
        private string _赠品价值;
        private string _商家名称;
        private string _商品详情链接;
        private string _推荐理由;
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 分类
        {
            set { _分类 = value; }
            get { return _分类; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 品类
        {
            set { _品类 = value; }
            get { return _品类; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 商品名称
        {
            set { _商品名称 = value; }
            get { return _商品名称; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 专柜价
        {
            set { _专柜价 = value; }
            get { return _专柜价; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 市场价
        {
            set { _市场价 = value; }
            get { return _市场价; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 双11价
        {
            set { _双11价 = value; }
            get { return _双11价; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 双11T4会员价
        {
            set { _双11t4会员价 = value; }
            get { return _双11t4会员价; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 折扣力度
        {
            set { _折扣力度 = value; }
            get { return _折扣力度; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 是否有赠
        {
            set { _是否有赠 = value; }
            get { return _是否有赠; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 赠品价值
        {
            set { _赠品价值 = value; }
            get { return _赠品价值; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 商家名称
        {
            set { _商家名称 = value; }
            get { return _商家名称; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 商品详情链接
        {
            set { _商品详情链接 = value; }
            get { return _商品详情链接; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 推荐理由
        {
            set { _推荐理由 = value; }
            get { return _推荐理由; }
        }
        #endregion Model

    }
}

