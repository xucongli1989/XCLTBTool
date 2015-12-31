using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using XCLShouCang.Model;
namespace XCLShouCang.BLL
{
	/// <summary>
	/// TB_SearchKey
	/// </summary>
	public partial class TB_SearchKey
	{
		private readonly XCLShouCang.DAL.TB_SearchKey dal=new XCLShouCang.DAL.TB_SearchKey();
		public TB_SearchKey()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SearchKeyID)
		{
			return dal.Exists(SearchKeyID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(XCLShouCang.Model.TB_SearchKey model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(XCLShouCang.Model.TB_SearchKey model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long SearchKeyID)
		{
			
			return dal.Delete(SearchKeyID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string SearchKeyIDlist )
		{
			return dal.DeleteList(SearchKeyIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XCLShouCang.Model.TB_SearchKey GetModel(long SearchKeyID)
		{
			
			return dal.GetModel(SearchKeyID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public XCLShouCang.Model.TB_SearchKey GetModelByCache(long SearchKeyID)
		{
			
			string CacheKey = "TB_SearchKeyModel-" + SearchKeyID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SearchKeyID);
					if (objModel != null)
					{
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (XCLShouCang.Model.TB_SearchKey)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<XCLShouCang.Model.TB_SearchKey> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<XCLShouCang.Model.TB_SearchKey> DataTableToList(DataTable dt)
		{
			List<XCLShouCang.Model.TB_SearchKey> modelList = new List<XCLShouCang.Model.TB_SearchKey>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				XCLShouCang.Model.TB_SearchKey model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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
        public long Add(XCLShouCang.Model.TB_SearchKey keyModel, List<XCLShouCang.Model.TB_Product> productList)
        {
            return dal.Add(keyModel, productList);
        }
		#endregion  ExtensionMethod
	}
}

