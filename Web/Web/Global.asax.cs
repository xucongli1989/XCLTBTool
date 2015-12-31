using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(Server.MapPath("~/Config/Log4NetConfig.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
            XCLNetTools.Message.Log.LogInfo = log4net.LogManager.GetLogger("LoggerConfig");
            XCLNetTools.Message.Log.JsonMessageName = "XCL20140800";

            //删除临时目录
            //XCLNetTools.FileHandler.FileDirectory.ClearDirectory(Server.MapPath("~/Upload/Temp/"));
        }
    }
}