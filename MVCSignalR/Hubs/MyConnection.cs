using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MVCSignalR.Hubs
{
    public class MyConnection : PersistentConnection
    {
        /// <summary>
        ///         connection.start();
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, "连接已经建立");
        }
        /// <summary>
        /// connection.received()
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var arr = data.Split('|');
            var nick = arr.Length > 0 ? arr[0] : "";
            var msg = arr.Length > 1 ? arr[1] : "";
            return Connection.Broadcast(new { time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg = msg, nick = nick, headFileName = arr.Length == 3 ? arr[2] : new Random().Next(1, 7).ToString() });
        }
        /// <summary>
        /// connection.disconnected()
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            return Connection.Broadcast(connectionId, "糟糕！服务器挂了...");
        }
    }
}