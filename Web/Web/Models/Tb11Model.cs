using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Tb11Model
    {
        public string CurrentTypeName { get; set; }

        public List<string> TypeNameList { get; set; }

        public List<XCLShouCang.Model.TB_11> DataList { get; set; }
    }
}