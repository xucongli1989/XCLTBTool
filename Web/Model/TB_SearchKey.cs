using System;
namespace XCLShouCang.Model
{
    /// <summary>
    /// TB_SearchKey:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_SearchKey
    {
        public TB_SearchKey()
        { }
        #region Model
        private long _searchkeyid;
        private string _keyname;
        private int _keytype = 0;
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public long SearchKeyID
        {
            set { _searchkeyid = value; }
            get { return _searchkeyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KeyName
        {
            set { _keyname = value; }
            get { return _keyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int KeyType
        {
            set { _keytype = value; }
            get { return _keytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}

