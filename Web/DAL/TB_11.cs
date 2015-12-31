using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace XCLShouCang.DAL
{
    /// <summary>
    /// 数据访问类:TB_11
    /// </summary>
    public partial class TB_11
    {
        public TB_11()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TB_11");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(XCLShouCang.Model.TB_11 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TB_11(");
            strSql.Append("分类,品类,商品名称,ImgUrl,专柜价,市场价,双11价,双11T4会员价,折扣力度,是否有赠,赠品价值,商家名称,商品详情链接,推荐理由)");
            strSql.Append(" values (");
            strSql.Append("@分类,@品类,@商品名称,@ImgUrl,@专柜价,@市场价,@双11价,@双11T4会员价,@折扣力度,@是否有赠,@赠品价值,@商家名称,@商品详情链接,@推荐理由)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@分类", SqlDbType.NVarChar,50),
					new SqlParameter("@品类", SqlDbType.NVarChar,50),
					new SqlParameter("@商品名称", SqlDbType.NVarChar,500),
					new SqlParameter("@ImgUrl", SqlDbType.VarChar,500),
					new SqlParameter("@专柜价", SqlDbType.VarChar,50),
					new SqlParameter("@市场价", SqlDbType.VarChar,50),
					new SqlParameter("@双11价", SqlDbType.VarChar,50),
					new SqlParameter("@双11T4会员价", SqlDbType.VarChar,50),
					new SqlParameter("@折扣力度", SqlDbType.VarChar,50),
					new SqlParameter("@是否有赠", SqlDbType.NVarChar,50),
					new SqlParameter("@赠品价值", SqlDbType.VarChar,50),
					new SqlParameter("@商家名称", SqlDbType.NVarChar,50),
					new SqlParameter("@商品详情链接", SqlDbType.VarChar,500),
					new SqlParameter("@推荐理由", SqlDbType.NVarChar,1000)};
            parameters[0].Value = model.分类;
            parameters[1].Value = model.品类;
            parameters[2].Value = model.商品名称;
            parameters[3].Value = model.ImgUrl;
            parameters[4].Value = model.专柜价;
            parameters[5].Value = model.市场价;
            parameters[6].Value = model.双11价;
            parameters[7].Value = model.双11T4会员价;
            parameters[8].Value = model.折扣力度;
            parameters[9].Value = model.是否有赠;
            parameters[10].Value = model.赠品价值;
            parameters[11].Value = model.商家名称;
            parameters[12].Value = model.商品详情链接;
            parameters[13].Value = model.推荐理由;

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
        public bool Update(XCLShouCang.Model.TB_11 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_11 set ");
            strSql.Append("分类=@分类,");
            strSql.Append("品类=@品类,");
            strSql.Append("商品名称=@商品名称,");
            strSql.Append("ImgUrl=@ImgUrl,");
            strSql.Append("专柜价=@专柜价,");
            strSql.Append("市场价=@市场价,");
            strSql.Append("双11价=@双11价,");
            strSql.Append("双11T4会员价=@双11T4会员价,");
            strSql.Append("折扣力度=@折扣力度,");
            strSql.Append("是否有赠=@是否有赠,");
            strSql.Append("赠品价值=@赠品价值,");
            strSql.Append("商家名称=@商家名称,");
            strSql.Append("商品详情链接=@商品详情链接,");
            strSql.Append("推荐理由=@推荐理由");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@分类", SqlDbType.NVarChar,50),
					new SqlParameter("@品类", SqlDbType.NVarChar,50),
					new SqlParameter("@商品名称", SqlDbType.NVarChar,500),
					new SqlParameter("@ImgUrl", SqlDbType.VarChar,500),
					new SqlParameter("@专柜价", SqlDbType.VarChar,50),
					new SqlParameter("@市场价", SqlDbType.VarChar,50),
					new SqlParameter("@双11价", SqlDbType.VarChar,50),
					new SqlParameter("@双11T4会员价", SqlDbType.VarChar,50),
					new SqlParameter("@折扣力度", SqlDbType.VarChar,50),
					new SqlParameter("@是否有赠", SqlDbType.NVarChar,50),
					new SqlParameter("@赠品价值", SqlDbType.VarChar,50),
					new SqlParameter("@商家名称", SqlDbType.NVarChar,50),
					new SqlParameter("@商品详情链接", SqlDbType.VarChar,500),
					new SqlParameter("@推荐理由", SqlDbType.NVarChar,1000),
					new SqlParameter("@ID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.分类;
            parameters[1].Value = model.品类;
            parameters[2].Value = model.商品名称;
            parameters[3].Value = model.ImgUrl;
            parameters[4].Value = model.专柜价;
            parameters[5].Value = model.市场价;
            parameters[6].Value = model.双11价;
            parameters[7].Value = model.双11T4会员价;
            parameters[8].Value = model.折扣力度;
            parameters[9].Value = model.是否有赠;
            parameters[10].Value = model.赠品价值;
            parameters[11].Value = model.商家名称;
            parameters[12].Value = model.商品详情链接;
            parameters[13].Value = model.推荐理由;
            parameters[14].Value = model.ID;

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
        public bool Delete(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_11 ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_11 ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public XCLShouCang.Model.TB_11 GetModel(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,分类,品类,商品名称,ImgUrl,专柜价,市场价,双11价,双11T4会员价,折扣力度,是否有赠,赠品价值,商家名称,商品详情链接,推荐理由 from TB_11 ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
			};
            parameters[0].Value = ID;

            XCLShouCang.Model.TB_11 model = new XCLShouCang.Model.TB_11();
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
        public XCLShouCang.Model.TB_11 DataRowToModel(DataRow row)
        {
            XCLShouCang.Model.TB_11 model = new XCLShouCang.Model.TB_11();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = long.Parse(row["ID"].ToString());
                }
                if (row["分类"] != null)
                {
                    model.分类 = row["分类"].ToString();
                }
                if (row["品类"] != null)
                {
                    model.品类 = row["品类"].ToString();
                }
                if (row["商品名称"] != null)
                {
                    model.商品名称 = row["商品名称"].ToString();
                }
                if (row["ImgUrl"] != null)
                {
                    model.ImgUrl = row["ImgUrl"].ToString();
                }
                if (row["专柜价"] != null)
                {
                    model.专柜价 = row["专柜价"].ToString();
                }
                if (row["市场价"] != null)
                {
                    model.市场价 = row["市场价"].ToString();
                }
                if (row["双11价"] != null)
                {
                    model.双11价 = row["双11价"].ToString();
                }
                if (row["双11T4会员价"] != null)
                {
                    model.双11T4会员价 = row["双11T4会员价"].ToString();
                }
                if (row["折扣力度"] != null)
                {
                    model.折扣力度 = row["折扣力度"].ToString();
                }
                if (row["是否有赠"] != null)
                {
                    model.是否有赠 = row["是否有赠"].ToString();
                }
                if (row["赠品价值"] != null)
                {
                    model.赠品价值 = row["赠品价值"].ToString();
                }
                if (row["商家名称"] != null)
                {
                    model.商家名称 = row["商家名称"].ToString();
                }
                if (row["商品详情链接"] != null)
                {
                    model.商品详情链接 = row["商品详情链接"].ToString();
                }
                if (row["推荐理由"] != null)
                {
                    model.推荐理由 = row["推荐理由"].ToString();
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
            strSql.Append("select ID,分类,品类,商品名称,ImgUrl,专柜价,市场价,双11价,双11T4会员价,折扣力度,是否有赠,赠品价值,商家名称,商品详情链接,推荐理由 ");
            strSql.Append(" FROM TB_11 ");
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
            strSql.Append(" ID,分类,品类,商品名称,ImgUrl,专柜价,市场价,双11价,双11T4会员价,折扣力度,是否有赠,赠品价值,商家名称,商品详情链接,推荐理由 ");
            strSql.Append(" FROM TB_11 ");
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
            strSql.Append("select count(1) FROM TB_11 ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from TB_11 T ");
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
            parameters[0].Value = "TB_11";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataTable GetListByTypeName(string typeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TB_11 where 分类=@分类");
            SqlParameter[] parameters = {
					new SqlParameter("@分类", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = typeName;

            DataSet ds= DbHelperSQL.Query(strSql.ToString(), parameters);
            DataTable dt = null;
            if (null != ds && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;

        }

        public List<string> GetTypeNameList()
        {
            List<string> lst = null;
            DataSet ds = DbHelperSQL.Query("SELECT DISTINCT 分类 from dbo.TB_11");
            if (null != ds && ds.Tables[0].Rows.Count > 0)
            {
                lst = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lst.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            return lst;
        }
        #endregion  ExtensionMethod
    }
}

