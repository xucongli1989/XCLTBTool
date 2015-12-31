using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using XCLShouCang.Model;
namespace XCLShouCang.BLL
{
	/// <summary>
	/// TB_Product
	/// </summary>
	public partial class TB_Product
	{
		private readonly XCLShouCang.DAL.TB_Product dal=new XCLShouCang.DAL.TB_Product();
		public TB_Product()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ProductID)
		{
			return dal.Exists(ProductID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(XCLShouCang.Model.TB_Product model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(XCLShouCang.Model.TB_Product model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long ProductID)
		{
			
			return dal.Delete(ProductID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ProductIDlist )
		{
			return dal.DeleteList(ProductIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XCLShouCang.Model.TB_Product GetModel(long ProductID)
		{
			
			return dal.GetModel(ProductID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public XCLShouCang.Model.TB_Product GetModelByCache(long ProductID)
		{
			
			string CacheKey = "TB_ProductModel-" + ProductID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ProductID);
					if (objModel != null)
					{
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (XCLShouCang.Model.TB_Product)objModel;
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
		public List<XCLShouCang.Model.TB_Product> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<XCLShouCang.Model.TB_Product> DataTableToList(DataTable dt)
		{
			List<XCLShouCang.Model.TB_Product> modelList = new List<XCLShouCang.Model.TB_Product>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				XCLShouCang.Model.TB_Product model;
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
        public List<XCLShouCang.Model.TB_Product> GetModelList(long searchKeyID)
        {
            return this.GetModelList(string.Format("FK_SearchKeyID={0}",searchKeyID));
        }

         /// <summary>
        /// 根据商家，统计商品数
        /// </summary>
        public List<XCLShouCang.Model.TB_ProductCountByShop> GetTopProductCountReport(long searchKeyID)
        {
            List<XCLShouCang.Model.TB_ProductCountByShop> lst = null;
            DataTable dt= dal.GetTopProductCountReport(searchKeyID);
            if (null != dt && dt.Rows.Count > 0)
            {
                lst = new List<TB_ProductCountByShop>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst.Add(new XCLShouCang.Model.TB_ProductCountByShop() {
                        Rep_ProductCount =XCLNetTools.StringHander.Common.GetInt(dt.Rows[i]["Rep_ProductCount"]),
                        ShopID =XCLNetTools.StringHander.Common.GetLong(dt.Rows[i]["ShopID"]),
                        ShopName = dt.Rows[i]["ShopName"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据省份，统计商品数
        /// </summary>
        public List<XCLShouCang.Model.TB_ProductCountGroupProvince> GetProductCountGroupProvince(long searchKeyID)
        {
            List<XCLShouCang.Model.TB_ProductCountGroupProvince> lst = null;
            DataTable dt = dal.GetProductCountGroupProvince(searchKeyID);
            if (null != dt && dt.Rows.Count > 0)
            {
                lst = new List<TB_ProductCountGroupProvince>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst.Add(new XCLShouCang.Model.TB_ProductCountGroupProvince()
                    {
                        Rep_ProductCount = XCLNetTools.StringHander.Common.GetInt(dt.Rows[i]["Rep_ProductCount"]),
                        ProductProvince = dt.Rows[i]["ProductProvince"].ToString()
                    });
                }
            }
            return lst;
        }

         /// <summary>
        /// 根据keyid,获取商品列表，便于导出
        /// </summary>
        public DataSet GetProductDataTable(long searchKeyID)
        {
            return dal.GetProductDataTable(searchKeyID);
        }
		#endregion  ExtensionMethod
	}
}

