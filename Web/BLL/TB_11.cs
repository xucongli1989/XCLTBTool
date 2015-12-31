using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using XCLShouCang.Model;
namespace XCLShouCang.BLL
{
    /// <summary>
    /// TB_11
    /// </summary>
    public partial class TB_11
    {
        private readonly XCLShouCang.DAL.TB_11 dal = new XCLShouCang.DAL.TB_11();
        public TB_11()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(XCLShouCang.Model.TB_11 model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(XCLShouCang.Model.TB_11 model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XCLShouCang.Model.TB_11 GetModel(long ID)
        {

            return dal.GetModel(ID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<XCLShouCang.Model.TB_11> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<XCLShouCang.Model.TB_11> DataTableToList(DataTable dt)
        {
            List<XCLShouCang.Model.TB_11> modelList = new List<XCLShouCang.Model.TB_11>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                XCLShouCang.Model.TB_11 model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        public List<XCLShouCang.Model.TB_11> GetListByTypeName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return this.GetModelList("");
            }


            List<XCLShouCang.Model.TB_11> lst = null;
            DataTable dt = dal.GetListByTypeName(typeName);
            if (null != dt && dt.Rows.Count > 0)
            {
                lst = DataTableToList(dt);
            }
            return lst;
        }
        public List<string> GetTypeNameList()
        {
            return dal.GetTypeNameList();
        }
        #endregion  ExtensionMethod
    }
}

