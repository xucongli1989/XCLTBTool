using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace XCLShouCang.DAL
{
	/// <summary>
	/// 数据访问类:TB_Product
	/// </summary>
	public partial class TB_Product
	{
		public TB_Product()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ProductID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TB_Product");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.BigInt)
			};
            parameters[0].Value = ProductID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(XCLShouCang.Model.TB_Product model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TB_Product(");
            strSql.Append("FK_SearchKeyID,ID,ProductURL,Sort,ImgURL,Price,Title,MonthDealCount,AppraiseCount,ShopName,ShopID,ShopURL,ShopProvince,ShopCity,ProductProvince,ProductCity,Rate,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@FK_SearchKeyID,@ID,@ProductURL,@Sort,@ImgURL,@Price,@Title,@MonthDealCount,@AppraiseCount,@ShopName,@ShopID,@ShopURL,@ShopProvince,@ShopCity,@ProductProvince,@ProductCity,@Rate,@CreateTime)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@FK_SearchKeyID", SqlDbType.BigInt,8),
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductURL", SqlDbType.VarChar,100),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ImgURL", SqlDbType.VarChar,1000),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Title", SqlDbType.VarChar,200),
					new SqlParameter("@MonthDealCount", SqlDbType.Int,4),
					new SqlParameter("@AppraiseCount", SqlDbType.Int,4),
					new SqlParameter("@ShopName", SqlDbType.VarChar,50),
					new SqlParameter("@ShopID", SqlDbType.BigInt,8),
					new SqlParameter("@ShopURL", SqlDbType.VarChar,100),
					new SqlParameter("@ShopProvince", SqlDbType.VarChar,50),
					new SqlParameter("@ShopCity", SqlDbType.VarChar,50),
					new SqlParameter("@ProductProvince", SqlDbType.VarChar,50),
					new SqlParameter("@ProductCity", SqlDbType.VarChar,50),
					new SqlParameter("@Rate", SqlDbType.Decimal,9),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.FK_SearchKeyID;
            parameters[1].Value = model.ID;
            parameters[2].Value = model.ProductURL;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.ImgURL;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.Title;
            parameters[7].Value = model.MonthDealCount;
            parameters[8].Value = model.AppraiseCount;
            parameters[9].Value = model.ShopName;
            parameters[10].Value = model.ShopID;
            parameters[11].Value = model.ShopURL;
            parameters[12].Value = model.ShopProvince;
            parameters[13].Value = model.ShopCity;
            parameters[14].Value = model.ProductProvince;
            parameters[15].Value = model.ProductCity;
            parameters[16].Value = model.Rate;
            parameters[17].Value = model.CreateTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(XCLShouCang.Model.TB_Product model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_Product set ");
            strSql.Append("FK_SearchKeyID=@FK_SearchKeyID,");
            strSql.Append("ID=@ID,");
            strSql.Append("ProductURL=@ProductURL,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("ImgURL=@ImgURL,");
            strSql.Append("Price=@Price,");
            strSql.Append("Title=@Title,");
            strSql.Append("MonthDealCount=@MonthDealCount,");
            strSql.Append("AppraiseCount=@AppraiseCount,");
            strSql.Append("ShopName=@ShopName,");
            strSql.Append("ShopID=@ShopID,");
            strSql.Append("ShopURL=@ShopURL,");
            strSql.Append("ShopProvince=@ShopProvince,");
            strSql.Append("ShopCity=@ShopCity,");
            strSql.Append("ProductProvince=@ProductProvince,");
            strSql.Append("ProductCity=@ProductCity,");
            strSql.Append("Rate=@Rate,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@FK_SearchKeyID", SqlDbType.BigInt,8),
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductURL", SqlDbType.VarChar,100),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ImgURL", SqlDbType.VarChar,1000),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Title", SqlDbType.VarChar,200),
					new SqlParameter("@MonthDealCount", SqlDbType.Int,4),
					new SqlParameter("@AppraiseCount", SqlDbType.Int,4),
					new SqlParameter("@ShopName", SqlDbType.VarChar,50),
					new SqlParameter("@ShopID", SqlDbType.BigInt,8),
					new SqlParameter("@ShopURL", SqlDbType.VarChar,100),
					new SqlParameter("@ShopProvince", SqlDbType.VarChar,50),
					new SqlParameter("@ShopCity", SqlDbType.VarChar,50),
					new SqlParameter("@ProductProvince", SqlDbType.VarChar,50),
					new SqlParameter("@ProductCity", SqlDbType.VarChar,50),
					new SqlParameter("@Rate", SqlDbType.Decimal,9),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ProductID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.FK_SearchKeyID;
            parameters[1].Value = model.ID;
            parameters[2].Value = model.ProductURL;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.ImgURL;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.Title;
            parameters[7].Value = model.MonthDealCount;
            parameters[8].Value = model.AppraiseCount;
            parameters[9].Value = model.ShopName;
            parameters[10].Value = model.ShopID;
            parameters[11].Value = model.ShopURL;
            parameters[12].Value = model.ShopProvince;
            parameters[13].Value = model.ShopCity;
            parameters[14].Value = model.ProductProvince;
            parameters[15].Value = model.ProductCity;
            parameters[16].Value = model.Rate;
            parameters[17].Value = model.CreateTime;
            parameters[18].Value = model.ProductID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ProductID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_Product ");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.BigInt)
			};
            parameters[0].Value = ProductID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string ProductIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_Product ");
            strSql.Append(" where ProductID in (" + ProductIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XCLShouCang.Model.TB_Product GetModel(long ProductID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProductID,FK_SearchKeyID,ID,ProductURL,Sort,ImgURL,Price,Title,MonthDealCount,AppraiseCount,ShopName,ShopID,ShopURL,ShopProvince,ShopCity,ProductProvince,ProductCity,Rate,CreateTime from TB_Product ");
            strSql.Append(" where ProductID=@ProductID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.BigInt)
			};
            parameters[0].Value = ProductID;

            XCLShouCang.Model.TB_Product model = new XCLShouCang.Model.TB_Product();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XCLShouCang.Model.TB_Product DataRowToModel(DataRow row)
        {
            XCLShouCang.Model.TB_Product model = new XCLShouCang.Model.TB_Product();
            if (row != null)
            {
                if (row["ProductID"] != null && row["ProductID"].ToString() != "")
                {
                    model.ProductID = long.Parse(row["ProductID"].ToString());
                }
                if (row["FK_SearchKeyID"] != null && row["FK_SearchKeyID"].ToString() != "")
                {
                    model.FK_SearchKeyID = long.Parse(row["FK_SearchKeyID"].ToString());
                }
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = long.Parse(row["ID"].ToString());
                }
                if (row["ProductURL"] != null)
                {
                    model.ProductURL = row["ProductURL"].ToString();
                }
                if (row["Sort"] != null && row["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(row["Sort"].ToString());
                }
                if (row["ImgURL"] != null)
                {
                    model.ImgURL = row["ImgURL"].ToString();
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["MonthDealCount"] != null && row["MonthDealCount"].ToString() != "")
                {
                    model.MonthDealCount = int.Parse(row["MonthDealCount"].ToString());
                }
                if (row["AppraiseCount"] != null && row["AppraiseCount"].ToString() != "")
                {
                    model.AppraiseCount = int.Parse(row["AppraiseCount"].ToString());
                }
                if (row["ShopName"] != null)
                {
                    model.ShopName = row["ShopName"].ToString();
                }
                if (row["ShopID"] != null && row["ShopID"].ToString() != "")
                {
                    model.ShopID = long.Parse(row["ShopID"].ToString());
                }
                if (row["ShopURL"] != null)
                {
                    model.ShopURL = row["ShopURL"].ToString();
                }
                if (row["ShopProvince"] != null)
                {
                    model.ShopProvince = row["ShopProvince"].ToString();
                }
                if (row["ShopCity"] != null)
                {
                    model.ShopCity = row["ShopCity"].ToString();
                }
                if (row["ProductProvince"] != null)
                {
                    model.ProductProvince = row["ProductProvince"].ToString();
                }
                if (row["ProductCity"] != null)
                {
                    model.ProductCity = row["ProductCity"].ToString();
                }
                if (row["Rate"] != null && row["Rate"].ToString() != "")
                {
                    model.Rate = decimal.Parse(row["Rate"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProductID,FK_SearchKeyID,ID,ProductURL,Sort,ImgURL,Price,Title,MonthDealCount,AppraiseCount,ShopName,ShopID,ShopURL,ShopProvince,ShopCity,ProductProvince,ProductCity,Rate,CreateTime ");
            strSql.Append(" FROM TB_Product ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ProductID,FK_SearchKeyID,ID,ProductURL,Sort,ImgURL,Price,Title,MonthDealCount,AppraiseCount,ShopName,ShopID,ShopURL,ShopProvince,ShopCity,ProductProvince,ProductCity,Rate,CreateTime ");
            strSql.Append(" FROM TB_Product ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM TB_Product ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ProductID desc");
            }
            strSql.Append(")AS Row, T.*  from TB_Product T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "TB_Product";
            parameters[1].Value = "ProductID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 根据商家，统计商品数
        /// </summary>
        public DataTable GetTopProductCountReport(long searchKeyID)
        {
            string strSql = string.Format("select * from TB_fun_GetTopProductCount({0})",searchKeyID);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable dt = null;
            if (null != ds && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据省份，统计商品数
        /// </summary>
        public DataTable GetProductCountGroupProvince(long searchKeyID)
        {
            string strSql = string.Format("select * from TB_fun_GetProductCountGroupProvince({0})", searchKeyID);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable dt = null;
            if (null != ds && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据keyid,获取商品列表，便于导出
        /// </summary>
        public DataSet GetProductDataTable(long searchKeyID)
        {
            string strSql = string.Format(@"SELECT
                                                            Title AS 名称,
                                                            ProductURL AS 宝贝链接,
                                                            ShopName AS 店铺,
                                                            ShopURL AS 店铺链接,
                                                            ProductProvince AS 所在省,
                                                            ProductCity AS 所在市,
                                                            Price AS 价格,
                                                            MonthDealCount AS 月成交数,
                                                            AppraiseCount AS 累计评价数,
                                                            Rate AS 评分
                                                            FROM TB_Product
                                                            WHERE FK_SearchKeyID={0}", searchKeyID);
            return DbHelperSQL.Query(strSql.ToString());
        }
		#endregion  ExtensionMethod
	}
}

