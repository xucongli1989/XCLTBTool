using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCLShouCang.Model
{
    public class TB_AppraiseCount
    {
        /// <summary>
        /// 时间类型
        /// </summary>
        public string AppraiseTimeType { get; set; }

        /// <summary>
        /// 好评数
        /// </summary>
        public int GoodCount { get; set; }

        /// <summary>
        /// 好评的评价链接地址
        /// </summary>
        public string GoodLink { get; set; }

        /// <summary>
        /// 中评数
        /// </summary>
        public int MiddleCount { get; set; }

        /// <summary>
        /// 中评的评价链接地址
        /// </summary>
        public string MiddleLink { get; set; }

        /// <summary>
        /// 差评数
        /// </summary>
        public int BadCount { get; set; }

        /// <summary>
        /// 差评的评价链接地址
        /// </summary>
        public string BadLink { get; set; }
    }




    /// <summary>
    /// 评价统计中的每个评价统计的model
    /// [[5,{'rater':'1','direction':'0','result':'1','timeLine':'-7'}],[0,{'rater':'1','direction':'0','result':'0','timeLine':'-7'}],[0,{'rater':'1','direction':'0','result':'-1','timeLine':'-7'}]]
    /// </summary>
    public class AppraiseItemCount
    {
        public int rater { get; set; }

        public int direction { get; set; }

        public int result { get; set; }

        public int timeLine { get; set; }
    }
}
