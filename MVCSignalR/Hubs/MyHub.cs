using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MVCSignalR.Hubs
{
    public class MyHub : Hub
    {
        public void Send(string nick, string message, string img)
        {
            Clients.All.sendMessage(nick, string.IsNullOrEmpty(img) ? new Random().Next(1, 7).ToString() : img, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
        }
    }
}