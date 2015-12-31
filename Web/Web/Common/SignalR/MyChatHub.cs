using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
namespace Web.Common.SignalR
{
    [HubName("XCLHub")]
    public class MyChatHub : Hub
    {
        /// <summary>
        /// 商品采集进度提示
        /// </summary>
        public void SendProductListProcess(Web.Models.Common.GetProductProcessModel model)
        {
            Clients.Caller.showProductListProcess(model);
        }
    }
}